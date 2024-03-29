﻿using CoreModule.Source.Dto.MovieCategory;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.UOW;
using ETicketing.ViewModels.MovieCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ETicketing.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class MovieCategoryController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<MovieCategoryController> _logger;
        private readonly MovieCategoryServiceInterface _movieCategoryService;
        private readonly IToastNotification _notify;
        public MovieCategoryController(UnitOfWorkInterface unitOfWork,
            MovieCategoryServiceInterface movieCategoryService,
            ILogger<MovieCategoryController> logger,
            IToastNotification notify)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _movieCategoryService = movieCategoryService;
            _notify = notify;
        }
        public async Task<IActionResult> Index()
        {
            var movieCategoryList = await _unitOfWork.Categories.GetAllAsync();
            var movieCategoryIndexViewModel = movieCategoryList.Select(a => new MovieCategoryIndexViewModel
            {
                Id = a.Id,
                Name = a.Name
            });
            return View(movieCategoryIndexViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

   

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id) ?? throw new MovieCategoryNotFoundException();
                var categoryUpdateViewModel = new MovieCategoryUpdateViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return View(categoryUpdateViewModel);
            }
            catch (Exception ex)
            {
                _notify.AddErrorToastMessage(ex.Message);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    
    }
}
