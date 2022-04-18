using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class ActorMovie
    {
        protected ActorMovie()
        {

        }
        public ActorMovie(Actor actor, Movie movie)
        {
            Actor = actor;
            Movie = movie;
        }
        public int ActorId { get; protected set; }
        public virtual Actor Actor { get; protected set; }
        public virtual Movie Movie { get; protected set; }
        public int MovieId { get; protected set; }
    }
}
