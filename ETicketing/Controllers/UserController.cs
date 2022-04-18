using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.UOW;
using ETicketing.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace ETicketing.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _notify;
        public UserController(ILogger<UserController> logger,
            UnitOfWorkInterface unitOfWork,
            UserManager<ApplicationUser> userManager,
            IToastNotification notify)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _notify = notify;
        }
      
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userIndexViewModel = new List<UserIndexViewModel>();
            foreach (var user in users)
            {
               
                var userModel =  new UserIndexViewModel
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Email = user.Email,
                    Username = user.UserName,
                    Status = await _userManager.IsInRoleAsync(user, ApplicationUser.RoleAdmin) ? ApplicationUser.RoleAdmin : ApplicationUser.RoleUser
                };
                userIndexViewModel.Add(userModel);
            }
            return View(userIndexViewModel);
            

        }

        public IActionResult ChangePassword(string userId)
        {
            var changePasswordViewModel = new ChangePasswordVieModel() { 
            UserId = userId
            };

            return View(changePasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVieModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId) ?? throw new UserNotFoundException();
                var isPasswordChanged = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if(isPasswordChanged.Succeeded)
                {
                    _notify.AddSuccessToastMessage("Password Changed Successfully");
                    return RedirectToAction(nameof(Index));
                }
                foreach(var error in isPasswordChanged.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
    }
}
