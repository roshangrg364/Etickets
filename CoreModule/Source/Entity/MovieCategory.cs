using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class MovieCategory
    {
        protected MovieCategory()
        {

        }
        public MovieCategory(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name=name;
        }
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public virtual IList<Movie> Movies { get; protected set; } = new List<Movie>();
    }
}
