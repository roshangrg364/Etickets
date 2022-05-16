using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.Extensions;
using ETicketing.ViewModels.Actor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoreModule.Source.Dto.Actor;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;

namespace ETicketing.Controllers.ApiController
{
    [Route("api/actors")]
    [ApiController]
    public class ActorApiController : ControllerBase
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<ActorApiController> _logger;
      
        private readonly ActorServiceInterface _actorService;
        private readonly ImageUploaderInterface _imageUploader;
        public ActorApiController(UnitOfWorkInterface unitOfWork,
            ILogger<ActorApiController> logger,
            ActorServiceInterface actorService,
            ImageUploaderInterface imageUploader)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _actorService = actorService;
            _imageUploader = imageUploader;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ActorCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid request");
              
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var createDto = new ActorCreateDto(model.Name, model.Description, imagePath);
                    await _actorService.Create(createDto);
                    return new JsonResult("Actor Added Successfully");
                   
                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
               
            }
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]ActorUpdateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid request");
              
                    string? imagePath = await ValidateAndUploadImage(model.Image);
                    var updateDto = new ActorUpdateDto(id, model.Name, model.Description, imagePath);
                    await _actorService.Update(updateDto);
                return new JsonResult("Updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
          
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _actorService.Remove(id);
                return new JsonResult("Deleted Succesfully");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
    
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

    }
}
