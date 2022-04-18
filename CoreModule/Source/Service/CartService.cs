using CoreModule.Source.Dto.CartItem;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.UOW;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class CartService : CartServiceInterface
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        
        public CartService(UnitOfWorkInterface unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CartItem> Add(CartItemCreateDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdString(dto.UserId).ConfigureAwait(false) ?? throw new UserNotFoundException();
            var movie = await _unitOfWork.Movies.GetByIdAsync(dto.MovieId).ConfigureAwait(false) ?? throw new MovieNotFoundException();
            CartItem cartItem;
            if (!movie.IsAvailable()) throw new MovieAlreadyExpiredException();
            var existingCartItem = await _unitOfWork.CartItems.GetByMovieAndUserId(dto.MovieId, dto.UserId).ConfigureAwait(false);
            if(existingCartItem is null)
            {
                var newCartItem = new CartItem( user, movie);
                await _unitOfWork.CartItems.AddAsync(newCartItem).ConfigureAwait(false);
                cartItem = newCartItem;
            }
            else
            {
                existingCartItem.IncreaseQuantity();
                cartItem = existingCartItem;
            }
            await _unitOfWork.Complete();
            return cartItem;

        }

        public async Task BulkUpdate(int id,int quantity)
        {
            var cartItem = await _unitOfWork.CartItems.GetByIdAsync(id).ConfigureAwait(false) ?? throw new CartItemNotFoundException();
            cartItem.BulkUpdate(quantity);
            await _unitOfWork.CartItems.Update(cartItem).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }

        public async Task ClearCart(string userId)
        {
            var cartItems = await _unitOfWork.CartItems.GetByUserId(userId).ConfigureAwait(false);
            if(cartItems.Any())
            {
                await _unitOfWork.CartItems.RemoveRange(cartItems).ConfigureAwait(false);
            }
            await _unitOfWork.Complete();
        }

        public async Task DecreaseQuantity(int cartItemId)
        {
           
            var cartItem = await _unitOfWork.CartItems.GetByIdAsync(cartItemId).ConfigureAwait(false) ?? throw new CartItemNotFoundException();
            cartItem.DecreaseQuantity();
            await _unitOfWork.Complete().ConfigureAwait(false);
         
        }

        public async Task IncreaseQuantity(int cartItemId)
        {
            var cartItem = await _unitOfWork.CartItems.GetByIdAsync(cartItemId).ConfigureAwait(false) ?? throw new CartItemNotFoundException();
            cartItem.IncreaseQuantity();
            await _unitOfWork.Complete().ConfigureAwait(false);

        }

        public async Task RemoveCartItem(int cartItemId)
        {
            var cartItem = await _unitOfWork.CartItems.GetByIdAsync(cartItemId).ConfigureAwait(false) ?? throw new CartItemNotFoundException();
            await _unitOfWork.CartItems.Remove(cartItem).ConfigureAwait(false);
            await _unitOfWork.Complete().ConfigureAwait(false);

        }
    }
}
