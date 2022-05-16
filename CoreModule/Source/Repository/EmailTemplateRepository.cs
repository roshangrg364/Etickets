
using CoreModule.Base;
using CoreModule.DbContextConfig;
using EmailModule.Entity;
using EmailModule.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Repository
{
    public class EmailTemplateRepository : BaseRepository<EmailTemplate>, EmailTemplateRepositoryInterface
    {
        public EmailTemplateRepository(MyDbContext context):base(context)
        {

        }
        public async Task<EmailTemplate?> GetByType(string name)
        {
            return await GetQueryable().Where(a => a.Type == name).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
