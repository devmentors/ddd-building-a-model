using DDD.BuildingBlocks.Core.V1.Models;

namespace DDD.BuildingBlocks.Core.V1.Inputs
{
    public class CreateOrderInput
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderProductInput> Products { get; set; }
        public CreateOrderPaymentInput Payment { get; set; }
        public CreateOrderShipmentInput Shipment { get; set; }


    }
    public class CreateOrderProductInput
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderPaymentInput
    {
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class CreateOrderShipmentInput
    {
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
    }
}
