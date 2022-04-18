using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class QuantityCannotBeLessThanOneException:Exception
    {
        public QuantityCannotBeLessThanOneException(string message="Quantity cannot be less than one"):base(message)
        {

        }
    }
}
