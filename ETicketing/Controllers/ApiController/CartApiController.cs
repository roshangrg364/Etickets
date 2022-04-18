using CoreModule.Source.Dto.CartItem;
using CoreModule.Source.Service;
using CoreModule.UOW;
using ETicketing.ApiModel;
using ETicketing.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers.ApiController
{
    [Route("api/carts")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly CartServiceInterface _cartService;
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CartApiController> _logger;
        public CartApiController(CartServiceInterface cartService,
            ILogger<CartApiController> logger,
            UnitOfWorkInterface unitOfWork)
        {
            _cartService = cartService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemCreateApiModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state");
                var cartItemCreateDto = new CartItemCreateDto(this.GetCurrentUserId(), model.MovieId);
                var cartItem = await _cartService.Add(cartItemCreateDto);
                var cartResponseData = new CartItemResponseModel
                {
                    MovieName = cartItem.Movie.Name,
                    Cinema = cartItem.Movie.CinemaHall.Name,
                    Quantity = cartItem.Quantity,
                    Rate = cartItem.Rate
                };
                return new JsonResult(cartResponseData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartListByUser()
        {
            try
            {
                var userId = this.GetCurrentUserId();
                var cartItemList = await _unitOfWork.CartItems.GetByUserId(userId);
                var cartListResponseData = cartItemList.Select(a => new CartItemResponseModel
                {
                    Id = a.Id,
                    MovieName = a.Movie.Name,
                    Cinema = a.Movie.CinemaHall.Name,
                    Quantity = a.Quantity,
                    Rate = a.Rate,
                    Total = a.TotalAmount

                }).ToList();
                return new JsonResult(cartListResponseData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{cartItemId}/increaseQuantity")]
        public async Task<IActionResult> AddQuantity(int cartItemId)
        {
            try
            {
                await _cartService.IncreaseQuantity(cartItemId);
                return new JsonResult("item increased successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{cartItemId}/decreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.DecreaseQuantity(cartItemId);
                return new JsonResult("item decreased successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItem(cartItemId);
                  return new JsonResult("item removed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }  
        
        [HttpPut("{cartItemId}/updateQuantity")]
        public async Task<IActionResult> Update(int cartItemId, CartItemUpdateModel model)
        {
            try
            {
                await _cartService.BulkUpdate(cartItemId,model.Quantity);
                return new JsonResult("item updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
