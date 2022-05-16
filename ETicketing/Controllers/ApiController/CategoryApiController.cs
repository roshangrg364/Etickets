using CoreModule.Source.Dto.MovieCategory;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.UOW;
using ETicketing.ViewModels.MovieCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers.ApiController
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {

        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CategoryApiController> _logger;
        private readonly MovieCategoryServiceInterface _movieCategoryService;
 
        public CategoryApiController(UnitOfWorkInterface unitOfWork,
            MovieCategoryServiceInterface movieCategoryService,
            ILogger<CategoryApiController> logger
         )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _movieCategoryService = movieCategoryService;

        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieCategoryCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid ModelState");
                
                    var movieCategoryCreateDto = new MovieCategoryCreateDto(model.Name);
                    await _movieCategoryService.Create(movieCategoryCreateDto);

                    return new JsonResult("Created Successfully");
                
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return  BadRequest(ex.Message);
            }
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,MovieCategoryUpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("invalid data");
               
                    var updateDto = new MovieCategoryUpdateDto(id, model.Name);
                    await _movieCategoryService.Update(updateDto);

                return new JsonResult("Updated Sucessfully");
            }
            catch (Exception ex)
            {
              
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

    }
}
