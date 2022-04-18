using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class PaidAmountCannotBeLessThanTotalAmountException:Exception
    {
        public PaidAmountCannotBeLessThanTotalAmountException(string message="Paid Amount Cannot be less than total amount"):base(message)
        {

        }
    }
}
