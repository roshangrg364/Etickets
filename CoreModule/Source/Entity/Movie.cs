using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class Movie
    {
        public const string Available = "Available";
        public const string Expired = "Closed";
        protected Movie()
        {

        }
        public Movie(string name, string description, decimal ticketPrice,string image,DateTime startDate, DateTime endDate,
            MovieCategory category, CinemaHall cinemaHall,Producer producer)
        {
            Name = name;
            Description = description;
            if (ticketPrice < 0) throw new Exception("Ticket Price cannot be less than zero");
            TicketPrice = ticketPrice;
            Image = image;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
            CinemaHall = cinemaHall;
            Producer = producer;
        }

        public void Update(string name, string description, decimal ticketPrice,  DateTime startDate, DateTime endDate,
            MovieCategory category, CinemaHall cinemaHall, Producer producer)
        {
            Name = name;
            Description = description;
            TicketPrice = ticketPrice;   
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
            CinemaHall = cinemaHall;
            Producer = producer;
        }

        public void SetImage(string image)
        {
            Image = image;
        }

        public bool IsAvailable() => EndDate > DateTime.Now; 
        public int Id { get;private set; }
        public string Name { get;protected set; }
        public string Description { get;protected set; }
        public decimal TicketPrice { get;protected set; }
        public string Image { get;protected set; }
        public DateTime StartDate { get;protected set; }
        public DateTime EndDate { get;protected set; }

        public int MovieCategoryId { get;protected set; }
        public virtual MovieCategory Category { get;protected set; }

        public int CinemaHallId { get;protected set; }
        public virtual CinemaHall CinemaHall { get;protected set; }
         public int ProducerId { get;protected set; }
        public virtual Producer Producer { get;protected set; }



        public virtual ICollection<ActorMovie> ActorMovies { get; protected set; } = new Collection<ActorMovie>();

       public void AddActor(Actor actor)
        {         
            if(!ActorMovies.Any(a=>a.Actor.Id == actor.Id))
            {
                ActorMovies.Add(new ActorMovie(actor, this));
            }
        }
        public void ClearActor()
        {
            ActorMovies.Clear();
        }


    }
}
