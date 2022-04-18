using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class StartDateCannotBeMoreThanEndDateException:Exception
    {
        public StartDateCannotBeMoreThanEndDateException(string message ="Start date cannot be more than end date"):base(message)
        {

        }
    }
}
