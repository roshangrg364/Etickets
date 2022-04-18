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
    public class CartRepository:BaseRepository<CartItem>,CartRepositoryInterface
    {
        public CartRepository(MyDbContext context):base(context)
        {

        }

        public async Task<CartItem?> GetByMovieAndUserId(int movieId, string userId)
        {
            return await GetQueryable().Where(a => a.MovieId == movieId && a.UserId == userId).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IList<CartItem>> GetByUserId(string userId)
        {
            return await GetQueryable().Where(a=>a.UserId == userId).ToListAsync().ConfigureAwait(false);
        }
    }
}
