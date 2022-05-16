using EmailModule.Entity;
using EmailModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailModule.Service
{
    public class EmailTemplateService : EmailTemplateServiceInterface
    {
        private readonly EmailTemplateRepositoryInterface _templateRepo;
        public EmailTemplateService(EmailTemplateRepositoryInterface templateRepo)
        {
            _templateRepo = templateRepo;
        }
        public async Task Create(string type, string template)
        {
            await ValidateTemplateType(type);
            var emailTemplate = new EmailTemplate(type, template);
            await _templateRepo.AddAsync(emailTemplate).ConfigureAwait(false);
        }

        public async Task Update(int id, string type, string template)
        {
            var emailTemplate = await _templateRepo.GetByIdAsync(id).ConfigureAwait(false) ?? throw new Exception("template not found");
            await ValidateTemplateType(type, emailTemplate);
            emailTemplate.Update(type, template);
            await _templateRepo.Update(emailTemplate).ConfigureAwait(false);
        }

        private async Task ValidateTemplateType(string type, EmailTemplate? template=null)
        {
            var templatewithSameType = await _templateRepo.GetByType(type).ConfigureAwait(false);
            if(templatewithSameType!=null && templatewithSameType !=template)
            {
                throw new Exception("Template Type Already Exists Exception");
            }
        }
    }
}
