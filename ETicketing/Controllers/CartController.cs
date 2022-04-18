using CoreModule.UOW;
using ETicketing.Extensions;
using ETicketing.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers
{
    public class CartController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CartController> _logger;
        public CartController(UnitOfWorkInterface unitOfWork,
            ILogger<CartController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadCartPartialView()
        {
            try
            {
                var userId = this.GetCurrentUserId();
                var cartList = await _unitOfWork.CartItems.GetByUserId(userId);
                var cartListIndexModel = cartList.Select(a => new CartIndexViewModel
                {
                    Id = a.Id,
                    MovieName = a.Movie.Name,
                    Cinema = a.Movie.CinemaHall.Name,
                    Quantity = a.Quantity,
                    Rate = a.Rate,
                    Total = a.TotalAmount

                }).ToList();
                return PartialView("_cartListPartialView", cartListIndexModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
           
        }

    }
}
