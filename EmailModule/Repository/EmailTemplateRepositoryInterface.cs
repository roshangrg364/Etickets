using EmailModule.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailModule.Repository
{
    public interface EmailTemplateRepositoryInterface
    {

     
        Task<EmailTemplate?> GetByType(string name);
        Task<EmailTemplate?> GetByIdAsync(int id);
        Task<IEnumerable<EmailTemplate>> GetAllAsync();
        IQueryable<EmailTemplate> GetQueryable();
        Task AddAsync(EmailTemplate entity);
        Task AddRangeAsync(IEnumerable<EmailTemplate> entities);
        Task Update(EmailTemplate entity);
        Task Remove(EmailTemplate entity);
        Task RemoveRange(IEnumerable<EmailTemplate> entities);
    }
}
