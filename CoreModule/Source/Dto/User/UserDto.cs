using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Dto.User
{
    public class UserDto
    {
        public UserDto(string fullName,string userName,string email,string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            UserName = userName;
        }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
      
    }
}
