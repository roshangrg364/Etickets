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
    public class MovieCategoryRepository : BaseRepository<MovieCategory>, MovieCategoryRespositoryInterface
    {
        public MovieCategoryRepository(MyDbContext context):base(context)
        {

        }
        public async Task<MovieCategory?> GetByName(string name)
        {
           return await GetQueryable().Where(a=>a.Name == name).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
