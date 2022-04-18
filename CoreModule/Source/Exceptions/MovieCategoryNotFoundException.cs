using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class MovieCategoryNotFoundException:Exception
    {
        public MovieCategoryNotFoundException(string message ="Movie Category Not Found"):base(message)
        {

        }
    }
}
