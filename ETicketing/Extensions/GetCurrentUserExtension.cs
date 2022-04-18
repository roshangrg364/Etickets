using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicketing.Extensions
{
    public static class GetCurrentUserExtension
    {

        public static string GetCurrentUserId(this ControllerBase controller)
        {
            return controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static async Task<ApplicationUser> GetCurrentUser(this ControllerBase controller)
        {
            var userId = controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            using var serviceScope = ServiceActivator.GetScope();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            return await userManager.FindByIdAsync(userId).ConfigureAwait(true) ?? throw new UserNotFoundException();
        }
    }
}
