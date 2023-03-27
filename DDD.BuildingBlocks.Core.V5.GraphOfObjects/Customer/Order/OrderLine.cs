using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Exceptions;

namespace DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Order
{
    public sealed record OrderLine
    {
        public int OrderLineNumber { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }
        
        public OrderLine(int orderLineNumber, string name, decimal unitPrice, int quantity)
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
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
