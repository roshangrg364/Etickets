using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException(string message ="User not found exception"):base(message)
        {

        }
    }
}
