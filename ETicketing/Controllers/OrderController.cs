using CoreModule.Source.Entity;
using CoreModule.UOW;
using ETicketing.Extensions;
using ETicketing.ViewModels.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace ETicketing.Controllers
{
    public class OrderController : Controller
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<OrderController> _logger;
        private readonly IToastNotification _notify;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(UnitOfWorkInterface unitOfWork,
            ILogger<OrderController> logger,
            IToastNotification notify,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notify = notify;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.Users.GetByIdString(this.GetCurrentUserId());
            var orderQueryable = _unitOfWork.Orders.GetQueryable();
            if(await _userManager.IsInRoleAsync(user,ApplicationUser.RoleUser))
            {
                orderQueryable = orderQueryable.Where(a => a.UserId == this.GetCurrentUserId());
            }
            var orderIndexViewModel = await orderQueryable.Select(a => new OrderIndexViewModel
            {
                Id = a.Id,
                User = a.User.FullName,
                Status = a.Status,
                CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd")
            }).ToListAsync();
            return View(orderIndexViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(id) ?? throw new Exception("Order Not Found");
                var orderDetailViewModel = new OrderDetailViewModel() {
                    Id = order.Id,
                    User = order.User.FullName,
                    Status = order.Status,
                    CreatedOn = order.CreatedOn.ToString("yyyy-MM-dd"),
                    OrderItems = order.OrderItems.Select(a => new OrderItemViewModel
                    {
                        Movie = a.Movie.Name,
                        Cinema = a.Movie.CinemaHall.Name,
                        Quantity = a.Quantity,
                        TicketPrice = a.Rate,
                        Amount = a.Amount
                    }).ToList(),
                    ShippingAddress= new ShippingAddressViewModel
                    {
                        Name = order.ShippingAddress.FullName,
                        Address = order.ShippingAddress.Address,
                        ZipCode = order.ShippingAddress.ZipCode,
                        PhoneNumber = order.ShippingAddress.PhoneNumber,
                    }

                };
                return View(orderDetailViewModel);

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
