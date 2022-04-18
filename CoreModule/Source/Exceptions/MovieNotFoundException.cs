using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class MovieNotFoundException:Exception
    {
        public MovieNotFoundException(string message ="Movie Not Found"):base(message)
        {

        }
    }
}
