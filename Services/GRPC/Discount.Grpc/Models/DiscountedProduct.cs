namespace Discount.Grpc.Models
{
    public class DiscountedProduct
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
