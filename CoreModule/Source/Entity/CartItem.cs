using CoreModule.Source.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class CartItem
    {
        protected CartItem()
        {

        }
        public CartItem( ApplicationUser user,Movie movie )
        {
            Rate = movie.TicketPrice;
            Quantity = 1;
            User = user;
            Movie = movie;
            AddedOn = DateTime.Now;
            TotalAmount = Quantity * Rate;

        }
        public  void IncreaseQuantity()
        {
            Quantity += 1;
            TotalAmount = (Rate * Quantity);
        }
        public  void DecreaseQuantity()
        {
            if (Quantity == 1) throw new QuantityCannotBeLessThanOneException();
            Quantity -= 1;
            TotalAmount = (Rate * Quantity);
        }
        public void BulkUpdate(int quantity)
        {
            if (quantity < 1) throw new QuantityCannotBeLessThanOneException();
            Quantity = quantity;
        }
        public  int Id { get; protected set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public  decimal Rate { get; protected set; }
        public  int Quantity { get; protected set; }
        public  decimal TotalAmount { get; protected set; }
        public  DateTime AddedOn { get; protected set; }
        public  string UserId { get; protected set; }
        public  virtual ApplicationUser User { get; protected set; }
       
    }
}
