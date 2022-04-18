using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class MovieAlreadyExpiredException:Exception
    {
        public MovieAlreadyExpiredException(string message = "Movie already expired") : base(message)
        {

        }
    }
}
