using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailModule.Service
{
    public interface EmailTemplateServiceInterface
    {
        Task Create(string type, string template);
        Task Update(int id,string type, string template);
    }
}
