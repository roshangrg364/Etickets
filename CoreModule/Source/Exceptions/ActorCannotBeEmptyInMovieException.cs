using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class ActorCannotBeEmptyInMovieException:Exception
    {
        public ActorCannotBeEmptyInMovieException(string message ="Movie must have at least one actor"):base(message)
        {

        }
    }
}
