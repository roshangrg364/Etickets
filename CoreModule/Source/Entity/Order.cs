using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class Order
    {
        public const string StatusCompleted = "Completed";
        protected Order()
        {

        }
        public Order(ApplicationUser user,ShippingAddress address)
        {
            Status = StatusCompleted;
            CreatedOn = DateTime.Now;
            User = user;
            ShippingAddress = address;

        }
        public int Id { get; protected set; }
        public string   Status { get; protected set; }
        public string   UserId { get; protected set; }
        public virtual ApplicationUser   User { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public  int ShippingAddressId { get; protected set; }
        public virtual ShippingAddress ShippingAddress { get; protected set; }
        public decimal  Amount { get; protected set; }
        public virtual ICollection<OrderItem> OrderItems { get; protected set; } = new List<OrderItem>();

        public void AddOrderItem(Movie movie,int quantity)
        {
            OrderItems.Add(new OrderItem(this, movie, quantity));
            CalculateAmount();
        }
        public void CalculateAmount()
        {
            Amount = OrderItems.Sum(a => a.Amount);
        }

    }
}
