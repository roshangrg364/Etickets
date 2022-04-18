using CoreModule.Base;
using CoreModule.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Repository
{
    public interface CartRepositoryInterface:BaseRepositoryInterface<CartItem>
    {
        Task<CartItem?> GetByMovieAndUserId(int movieId, string userId);
        Task<IList<CartItem>> GetByUserId(string userId);
    }
}
