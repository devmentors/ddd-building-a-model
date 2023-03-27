using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Exceptions;

namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Order
{
    public sealed record OrderLine
    {
        public int OrderLineNumber { get; }
        public string SKU { get; }

        public string Name { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }
        
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

            OrderLineNumber = orderLineNumber;
            SKU = sku;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
