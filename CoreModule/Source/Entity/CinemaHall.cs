using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class CinemaHall
    {
        protected CinemaHall()
        {

        }
        public CinemaHall(string name, string description)
        {
            Name = name;
            Description = description;
            
        }
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public void SetImage(string image)
        {
            Image = image;
        }
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string? Image { get; protected set; }

        public virtual IList<Movie> Movies { get; protected set; } = new List<Movie>();
    }
}
