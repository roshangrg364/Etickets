using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Dto.CartItem
{
    public class CartItemCreateDto
    {
        public CartItemCreateDto(string userId, int movieId)
        {
            UserId = userId;
            MovieId = movieId;
           
        }
        public string UserId { get; set; }
        public int MovieId { get; set; }
      
      
    }
}
