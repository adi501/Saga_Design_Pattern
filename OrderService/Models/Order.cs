namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
