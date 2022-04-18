using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Dto.Movie
{
    public class MovieCreateDto
    {
        public MovieCreateDto(string name, string description, decimal ticket, string image, DateTime startDate,
            DateTime endDate, int movieCategoryId,int cinemaHallId,int producerId,List<int> actorIds)
        {
            Name = name;
            Description = description;
            TicketPrice = ticket;
            Image = image;
            StartDate = startDate;
            EndDate = endDate;
            MovieCategoryId = movieCategoryId;
            CinemaHallId = cinemaHallId;
            ProducerId = producerId;
            ActorIds = actorIds;

        }
    
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TicketPrice { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int MovieCategoryId { get; set; }
      
        public int CinemaHallId { get; set; }
 
        public int ProducerId { get; set; }
        public List<int> ActorIds { get; set; } = new List<int>();

    }
    public class MovieUpdateDto:MovieCreateDto
    {
        public MovieUpdateDto(int id, string name, string description, decimal ticket, string image, DateTime startDate,
            DateTime EndDate, int movieCategoryId, int cinemaHallId, int producerId, List<int> actorIds):base(name,description,ticket,image,startDate,EndDate,
                movieCategoryId,cinemaHallId,producerId,actorIds)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
