using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.Inputs
{
    public class CreateOrderInput
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderProductInput> Products { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
    
    public class CreateOrderProductInput
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
