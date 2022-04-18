using CoreModule.Source.Dto.Actor;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.Extensions;
using ETicketing.ViewModels.Actor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ETicketing.Controllers
{
 [Authorize(Roles =ApplicationUser.RoleAdmin)]
    public class ActorController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<ActorController> _logger;
        private readonly IToastNotification _notify;
        private readonly ActorServiceInterface _actorService;
        private readonly ImageUploaderInterface _imageUploader;
        public ActorController(UnitOfWorkInterface unitOfWork,
            ILogger<ActorController> logger,
            IToastNotification notify,
            ActorServiceInterface actorService,
            ImageUploaderInterface imageUploader)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notify = notify;
            _actorService = actorService;
            _imageUploader = imageUploader;
        }
        public async Task<IActionResult> Index()
        {
          var user=  await this.GetCurrentUser();
            var actors = await _unitOfWork.Actors.GetAllAsync();
            var actorIndexViewModel = actors.Select(a => new ActorIndexViewModel
            {
                Id = a.Id,
                Name = a.FullName,
                Description = a.Description,
                Image = a.Image ?? ""
            });
            return View(actorIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var createDto = new ActorCreateDto(model.Name, model.Description, imagePath);
                    await _actorService.Create(createDto);
                    _notify.AddSuccessToastMessage("Actor Added Successfully");
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
                var actor = await _unitOfWork.Actors.GetByIdAsync(id) ?? throw new ActorNotFoundException();
                var updateViewModel = new ActorUpdateViewModel
                {
                    Id = actor.Id,
                    Name = actor.FullName,
                    Description = actor.Description,
                    ImageSource = actor.Image
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
        public async Task<IActionResult> Update(ActorUpdateViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var updateDto = new ActorUpdateDto(model.Id, model.Name, model.Description, imagePath);
                    await _actorService.Update(updateDto);
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
                var actor = await _unitOfWork.Actors.GetByIdAsync(id) ?? throw new ActorNotFoundException();
                ActorDetailsViewModel actorDetailsViewModel = ConfigureActorDetails(actor);
                return View(actorDetailsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

      [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           
            await _actorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> ValidateAndUploadImage(IFormFile file)
        {
            string? imagePath = null;
            if (file != null &&file.Length > 0)
            {
                imagePath = await _imageUploader.UploadToTemporary(file);
            }

            return imagePath;
        }
        private static ActorDetailsViewModel ConfigureActorDetails(CoreModule.Source.Entity.Actor actor)
        {
            return new ActorDetailsViewModel
            {
                id = actor.Id,
                Name = actor.FullName,
                Description = actor.Description,
                Image = actor.Image ?? ""
            };
        }
    }
}
