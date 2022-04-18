using CoreModule.Base;
using CoreModule.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Repository
{
    public interface MovieRepositoryInterface:BaseRepositoryInterface<Movie>
    {
        Task<IEnumerable<Movie>> GetByName(string name);
        Task<IEnumerable<Movie>> GetAllAvailableMovies();
        Task<Movie?> GetByCinemaHallAndMovie(string movie, int cinemaId);
    }
}
