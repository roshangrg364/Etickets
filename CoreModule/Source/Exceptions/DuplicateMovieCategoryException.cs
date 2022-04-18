using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public    class DuplicateMovieCategoryException:Exception
    {
        public DuplicateMovieCategoryException(string message ="Duplicate Movie Category."):base(message)
        {

        }
    }
}
