using CoreModule.Source.Dto.User;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class UserService : UserServiceInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
      
        public UserService(UserManager<ApplicationUser> userManager
           )
        {
            _userManager = userManager;
            
        }
        public async Task Create(UserDto dto)
        {
            await ValidateUser(dto);
            var user = new ApplicationUser()
            {
                FullName = dto.FullName,
                UserName = dto.UserName,
                Email =dto.Email
               
            };
            var isSuceeded = await _userManager.CreateAsync(user, dto.Password);
            if (!isSuceeded.Succeeded) throw new UserException(isSuceeded.Errors.First().Description);
            await _userManager.AddToRoleAsync(user, ApplicationUser.RoleUser);
        }
        private async Task ValidateUser(UserDto dto)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(dto.Email).ConfigureAwait(false);
            if (userWithSameEmail != null) throw new UserWithSameEmailAlreadyExistException();
            var userWithSameUserName = await _userManager.FindByNameAsync(dto.UserName).ConfigureAwait(false);
            if (userWithSameUserName != null) throw new DuplicateUserNameException();
        }
    }
}
