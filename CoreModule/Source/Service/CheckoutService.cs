using CoreModule.Source.Dto.Checkout;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class CheckoutService : CheckoutServiceInterface
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        public CheckoutService(UnitOfWorkInterface unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<int> Checkout(CheckoutDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdString(dto.UserId).ConfigureAwait(false) ?? throw new UserNotFoundException();
            var cartItems = await _unitOfWork.CartItems.GetByUserId(dto.UserId).ConfigureAwait(false);
            var totalAmount = cartItems.Sum(a => a.TotalAmount);
            VerifyPaidAmount(dto, totalAmount);
            var shippingAddress = new ShippingAddress(dto.FullName, dto.Address, dto.ZipCode, dto.PhoneNumber);
            await _unitOfWork.ShippingAddress.AddAsync(shippingAddress).ConfigureAwait(false);

            var Order = new Order(user, shippingAddress);
            foreach(var cartItem in cartItems)
            {
                Order.AddOrderItem(cartItem.Movie, cartItem.Quantity);
            }
            await _unitOfWork.Orders.AddAsync(Order).ConfigureAwait(false);
            await _unitOfWork.CartItems.RemoveRange(cartItems).ConfigureAwait(false);
            await _unitOfWork.Complete();
            return Order.Id;
        }

        private static void VerifyPaidAmount(CheckoutDto dto, decimal totalAmount)
        {
            if (dto.PaidAmount < totalAmount) throw new PaidAmountCannotBeLessThanTotalAmountException();
        }
    }
}
