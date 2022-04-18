using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class ProducerNotFoundException:Exception
    {
        public ProducerNotFoundException(string message ="Producer Not Found."):base(message)
        {

        }
    }
}
