namespace ETicketing.ViewModels.Order
{
    public class OrderIndexViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
    }
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public IList<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
        public ShippingAddressViewModel ShippingAddress { get; set; } 
    }
    public class OrderItemViewModel
    {
        public string Movie { get; set; }
        public string Cinema { get; set; }
        public decimal TicketPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class ShippingAddressViewModel
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }



}
