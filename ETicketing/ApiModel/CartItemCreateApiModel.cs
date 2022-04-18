namespace ETicketing.ApiModel
{
    public class CartItemCreateApiModel
    {
        public int MovieId { get; set; }

    }
    public class CartItemResponseModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Cinema { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
       
    }

    public class CartItemUpdateModel
    {
        public int Quantity { get; set; }
    }

}
