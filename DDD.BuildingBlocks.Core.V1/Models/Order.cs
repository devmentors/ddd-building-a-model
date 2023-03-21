namespace DDD.BuildingBlocks.Core.V1.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Customer Customer { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public List<OrderProduct> Products { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
    
    public enum PaymentMethod
    {
        Cash,
        Cashless,
        Loan
    }

    public enum OrderStatus
    {
        BeforeCheckout = 1,
        Placed = 2,
        Completed = 3,
        Canceled = 4
    }
}
