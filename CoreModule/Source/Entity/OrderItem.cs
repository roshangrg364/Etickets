using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class OrderItem
    {
        protected OrderItem() { }
        public OrderItem(Order order, Movie movie, int quantity)
        {
            Order = order;
            Movie = movie;
            Quantity = quantity;
            Rate = movie.TicketPrice;
            Amount = Quantity * Rate;

        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
