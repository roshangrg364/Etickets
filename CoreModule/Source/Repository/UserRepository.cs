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
    public class UserRepository : BaseRepository<ApplicationUser>, UserRepositoryInterface
    {
        public UserRepository(MyDbContext context):base(context)
        {

        }
        public async Task<ApplicationUser?> GetByIdString(string id)
        {
            return await GetQueryable().Where(a => a.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
