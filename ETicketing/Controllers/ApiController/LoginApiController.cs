using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using ETicketing.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers.ApiController
{
    [AllowAnonymous]
    [Route("api/logins")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly ILogger<LoginApiController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserServiceInterface _userService;
        public LoginApiController(ILogger<LoginApiController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UserServiceInterface userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Model State");
                var user = await _userManager.FindByNameAsync(model.UserName) ?? throw new IncorrectUsernameOrPasswordException();
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCheck) throw new IncorrectUsernameOrPasswordException();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (!result.Succeeded) throw new UserException("Wrong credentials.Please, try again!");

                return new JsonResult(new { returnUrl = model.ReturnUrl});
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

          
        }

    }
}
