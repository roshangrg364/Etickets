using CoreModule.Source.Dto.CinemalHall;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.ViewModels.CinemalHall;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ETicketing.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class CinemaHallController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CinemaHallController> _logger;
        private readonly IToastNotification _notify;
        private readonly CinemaHallServiceInterface _CinemaHallService;
        private readonly ImageUploaderInterface _imageUploader;
        public CinemaHallController(UnitOfWorkInterface unitOfWork,
            ILogger<CinemaHallController> logger,
            IToastNotification notify,
            CinemaHallServiceInterface CinemaHallService,
            ImageUploaderInterface imageUploader)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notify = notify;
            _CinemaHallService = CinemaHallService;
            _imageUploader = imageUploader;
        }
        public async Task<IActionResult> Index()
        {
            var CinemaHalls = await _unitOfWork.CinemaHalls.GetAllAsync();
            var cinemaHallIndexViewModel = CinemaHalls.Select(a => new CinemaHallIndexViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Image = a.Image ?? ""
            });
            return View(cinemaHallIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaHallCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var createDto = new CinemaHallCreatDto(model.Name, model.Description, imagePath);
                    await _CinemaHallService.Create(createDto);
                    _notify.AddSuccessToastMessage("CinemaHall Added Successfully");
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
                var CinemaHall = await _unitOfWork.CinemaHalls.GetByIdAsync(id) ?? throw new CinemaNotFoundException();
                var updateViewModel = new CinemaHallUpdateViewModel
                {
                    Id = CinemaHall.Id,
                    Name = CinemaHall.Name,
                    Description = CinemaHall.Description,
                    ImageSource = CinemaHall.Image
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
        public async Task<IActionResult> Update(CinemaHallUpdateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var updateDto = new CinemaHallUpdateDto(model.Id, model.Name, model.Description, imagePath);
                    await _CinemaHallService.Update(updateDto);
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
                var CinemaHall = await _unitOfWork.CinemaHalls.GetByIdAsync(id) ?? throw new CinemaNotFoundException();
                CinemaHallDetailsViewModel CinemaHallDetailsViewModel = ConfigureCinemaHallDetails(CinemaHall);
                return View(CinemaHallDetailsViewModel);
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
        private static CinemaHallDetailsViewModel ConfigureCinemaHallDetails(CoreModule.Source.Entity.CinemaHall CinemaHall)
        {
            return new CinemaHallDetailsViewModel
            {
                id = CinemaHall.Id,
                Name = CinemaHall.Name,
                Description = CinemaHall.Description,
                Image = CinemaHall.Image ?? ""
            };
        }
    }
}
