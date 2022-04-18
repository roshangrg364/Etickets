using CoreModule.Source.Dto.Producer;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.ViewModels.MovieCategory;
using ETicketing.ViewModels.Producer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;


namespace ETicketing.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class ProducerController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<ProducerController> _logger;
        private readonly IToastNotification _notify;
        private readonly ProducerServiceInterface _ProducerService;
        private readonly ImageUploaderInterface _imageUploader;
        public ProducerController(UnitOfWorkInterface unitOfWork,
            ILogger<ProducerController> logger,
            IToastNotification notify,
            ProducerServiceInterface ProducerService,
            ImageUploaderInterface imageUploader)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notify = notify;
            _ProducerService = ProducerService;
            _imageUploader = imageUploader;
        }
        public async Task<IActionResult> Index()
        {
            var Producers = await _unitOfWork.Producers.GetAllAsync();
            var producerIndexViewModel = Producers.Select(a => new ProducerIndexViewModel
            {
                Id = a.Id,
                Name = a.FullName,
                Description = a.Description,
                Image = a.Image ?? ""
            });
            return View(producerIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProducerCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var createDto = new ProducerCreateDto(model.Name, model.Description, imagePath);
                    await _ProducerService.Create(createDto);
                    _notify.AddSuccessToastMessage("Producer Added Successfully");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var Producer = await _unitOfWork.Producers.GetByIdAsync(id) ?? throw new ProducerNotFoundException();
                var updateViewModel = new ProducerUpdateViewModel
                {
                    Id = Producer.Id,
                    Name = Producer.FullName,
                    Description = Producer.Description,
                    ImageSource = Producer.Image
                };
                return View(updateViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProducerUpdateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var updateDto = new ProducerUpdateDto(model.Id, model.Name, model.Description, imagePath);
                    await _ProducerService.Update(updateDto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var Producer = await _unitOfWork.Producers.GetByIdAsync(id) ?? throw new ProducerNotFoundException();
                ProducerDetailsViewModel ProducerDetailsViewModel = ConfigureProducerDetails(Producer);
                return View(ProducerDetailsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

      
        private async Task<string> ValidateAndUploadImage(IFormFile file)
        {
            string? imagePath = null;
            if (file != null && file.Length > 0)
            {
                imagePath = await _imageUploader.UploadToTemporary(file);
            }

            return imagePath;
        }
        private static ProducerDetailsViewModel ConfigureProducerDetails(CoreModule.Source.Entity.Producer Producer)
        {
            return new ProducerDetailsViewModel
            {
                id = Producer.Id,
                Name = Producer.FullName,
                Description = Producer.Description,
                Image = Producer.Image ?? ""
            };
        }
    }
}
