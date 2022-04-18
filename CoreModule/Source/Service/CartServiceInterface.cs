using CoreModule.Source.Dto.CartItem;
using CoreModule.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface CartServiceInterface
    {
        Task<CartItem> Add(CartItemCreateDto dto);
      
        Task ClearCart(string userId);
        Task IncreaseQuantity(int cartItemId);
        Task DecreaseQuantity(int cartItemId);
        Task BulkUpdate(int id,int quantity);
        Task RemoveCartItem(int cartItemId);
    }
}
