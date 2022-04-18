using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class UserWithSameEmailAlreadyExistException:Exception
    {
        public UserWithSameEmailAlreadyExistException(string message ="User With Same Email Already Exists."):base(message)
        {

        }
    }
}
