using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EmailModule.Repository;
using EmailModule.Service;
using ETicketing.ViewModels.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;

namespace AuthenticationAndAuthorization.Controllers
{
    public class EmailTemplateController : Controller
    {
        private readonly EmailTemplateRepositoryInterface _templateRepo;
        private readonly ILogger<EmailTemplateController> _logger;
        private readonly EmailTemplateServiceInterface _emailTemplateService;
        private readonly IToastNotification _notification;
       
        public EmailTemplateController(EmailTemplateRepositoryInterface templateRepo,
            ILogger<EmailTemplateController> logger,
             EmailTemplateServiceInterface emailTemplateService,
             IToastNotification notification)
        {
            _templateRepo = templateRepo;
            _logger = logger;
            _emailTemplateService = emailTemplateService;
            _notification = notification;
        }
        public async Task<IActionResult> Index()
        {

            var emailTemplates = await _templateRepo.GetAllAsync().ConfigureAwait(true);
            var emailTemplateIndexModels = emailTemplates.Select(a => new EmailTemplateIndexViewModel
            {
            Id = a.Id,
            Type = a.Type
            });

            return View(emailTemplateIndexModels);
          
        }

        public IActionResult Create()
        {
            var emailTemplateCreateModel = new EmailTemplateCreateViewModel();
            return View(emailTemplateCreateModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmailTemplateCreateViewModel model)
        {
            try
            {
                var template = HttpUtility.HtmlDecode(model.Template);
                await _emailTemplateService.Create(model.Type, template);
                _notification.AddSuccessToastMessage("created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                _notification.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }


    }
}
