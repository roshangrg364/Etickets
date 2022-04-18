using CoreModule.Source.Dto.User;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using ETicketing.Extensions;
using ETicketing.ViewModels.Account;
using ETicketing.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ETicketing.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserServiceInterface _userService;
        private readonly IToastNotification _notify;
        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UserServiceInterface userService,
            IToastNotification notify)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _notify = notify;
        }
        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = "/Home/Index"
            };
            return View(loginViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            try
            {
                if (!ModelState.IsValid) return View(model);
                var user = await _userManager.FindByNameAsync(model.UserName) ?? throw new IncorrectUsernameOrPasswordException();
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCheck) throw new IncorrectUsernameOrPasswordException();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if(!result.Succeeded) throw new UserException("Wrong credentials.Please, try again!");
                _notify.AddSuccessToastMessage("Logged in successsfully");
                return LocalRedirect(model.ReturnUrl);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }

            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            try
            {
                var userDto = new UserDto(registerVM.FullName, registerVM.UserName, registerVM.Email, registerVM.Password);
                await _userService.Create(userDto);
                _notify.AddSuccessToastMessage("User registered Successfully");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(registerVM);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _notify.AddSuccessToastMessage("Logged Out Successfully");
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVieModel model)
        {
            try
            {
                var userId = this.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException();
                var isPasswordChanged = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (isPasswordChanged.Succeeded)
                {
                    _notify.AddSuccessToastMessage("Password Changed Successfully");
                    return RedirectToAction("Index","Home");
                }
                foreach (var error in isPasswordChanged.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
    }
}
