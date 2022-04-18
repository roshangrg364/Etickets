using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class CartItemNotFoundException:Exception
    {
        public CartItemNotFoundException(string message ="Cart item not found"):base(message)
        {

        }
    }
}
