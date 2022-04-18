using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class IncorrectUsernameOrPasswordException:Exception
    {
        public IncorrectUsernameOrPasswordException(string message ="Incorrect username or password"):base(message)
        {

        }
    }
}
