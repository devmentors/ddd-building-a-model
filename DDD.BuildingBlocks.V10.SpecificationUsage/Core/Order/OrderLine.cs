using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order
{
    public sealed record OrderLine
    {
        public Guid Id { get; }
        public int OrderLineNumber { get; }
        public string SKU { get; }

        public string Name { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }

        private OrderLine()
        {
        }
        
        public OrderLine(int orderLineNumber, string sku, string name,  decimal unitPrice, int quantity)
        {
            if (quantity < 1)
            {
                throw new InvalidOrderLineDataException();
            }
            if (unitPrice < 0)
            {
                throw new InvalidOrderLineDataException();
            }

            Id = Guid.NewGuid();
            OrderLineNumber = orderLineNumber;
            SKU = sku;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
