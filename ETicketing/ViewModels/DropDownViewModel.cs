
using Category = CoreModule.Source.Entity.MovieCategory;
using ActorEntity = CoreModule.Source.Entity.Actor;
using ProducerEntity = CoreModule.Source.Entity.Producer;
using Cinema = CoreModule.Source.Entity.CinemaHall;
namespace ETicketing.ViewModels
{
    public class DropDownViewModel
    {
        public IList<Category> Categories { get; set; } = new List<Category>();
        public IList<ActorEntity> Actors { get; set; } = new List<ActorEntity>();
        public IList<ProducerEntity> Producers { get; set; } = new List<ProducerEntity>();
        public IList<Cinema> Cinemas { get; set; } = new List<Cinema>();
    }
}
