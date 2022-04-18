using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public const string RoleAdmin = "Admin";
        public const string RoleUser = "User";

        public string FullName { get; set; }
    }
}
