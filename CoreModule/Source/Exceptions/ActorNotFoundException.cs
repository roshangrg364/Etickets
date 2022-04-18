using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class ActorNotFoundException:Exception
    {
        public ActorNotFoundException(string message = "Actor Not Found."):base(message)
        {

        }
    }
}
