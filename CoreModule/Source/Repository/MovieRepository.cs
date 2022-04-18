using CoreModule.Base;
using CoreModule.DbContextConfig;
using CoreModule.Source.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Repository
{
    public class MovieRepository:BaseRepository<Movie>,MovieRepositoryInterface
    {
        public MovieRepository(MyDbContext context):base(context)
        {

        }

        public async Task<IEnumerable<Movie>> GetAllAvailableMovies()
        {
            return await GetQueryable().Where(a => a.StartDate > a.EndDate ).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Movie>> GetByName(string name)
        {
            return await GetQueryable().Where(a=>a.Name == name).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Movie?> GetByCinemaHallAndMovie(string movie, int cinemaId)
        {
            return await GetQueryable().Where(a => a.Name == movie && a.CinemaHallId == cinemaId).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
