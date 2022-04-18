using CoreModule.UOW;
using ETicketing.Extensions;
using ETicketing.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<CheckoutController> _logger;
        public CheckoutController(UnitOfWorkInterface unitOfWork,
            ILogger<CheckoutController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
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
            return View(cartListIndexModel);
        }
        public IActionResult Success(int orderId)
        {
            return View(orderId);
        }
    }
}
