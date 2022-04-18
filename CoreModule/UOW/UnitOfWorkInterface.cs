
using CoreModule.Source.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.UOW
{
    public interface UnitOfWorkInterface:IDisposable
    {
        ActorRepositoryInterface Actors { get; }
        ActorMovieRepositoryInterface ActorMovies { get; }
        CinemaHallRepositoryInterface CinemaHalls { get; }
        ProducerRepositoryInterface Producers { get; }
        MovieCategoryRespositoryInterface Categories { get; }
        MovieRepositoryInterface Movies { get; }
        CartRepositoryInterface CartItems { get; }
        UserRepositoryInterface Users { get; }
        ShippingAddressRepositoryInterface ShippingAddress { get; }
        OrderRepositoryInterface Orders { get; }
        Task<int> Complete();
    }
}
