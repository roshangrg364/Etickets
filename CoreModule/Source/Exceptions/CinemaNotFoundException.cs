using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class CinemaNotFoundException:Exception
    {
        public CinemaNotFoundException(string message="Cinema Not Found."):base(message)
        {

        }
    }
}
