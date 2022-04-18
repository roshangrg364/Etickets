using CoreModule.Source.Dto.Checkout;
using CoreModule.Source.Service;
using CoreModule.UOW;
using ETicketing.ApiModel;
using ETicketing.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers.ApiController
{
    [Route("api/checkouts")]
    [ApiController]
    public class CheckoutApiController : ControllerBase
    {
        private readonly CheckoutServiceInterface _checkoutService;
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CheckoutApiController> _logger;
        public CheckoutApiController(CheckoutServiceInterface checkOutService,
            ILogger<CheckoutApiController> logger,
            UnitOfWorkInterface unitOfWork)
        {
            _checkoutService = checkOutService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutApiModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Model state");
                var checkoutDto = new CheckoutDto(this.GetCurrentUserId(), model.PaidAmount, model.Name, model.Address, model.ZipCode, model.Number);
              var orderId =  await _checkoutService.Checkout(checkoutDto);
                return new JsonResult(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
