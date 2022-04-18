namespace ETicketing.ViewModels.Cart
{
    public class CartIndexViewModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Cinema { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
