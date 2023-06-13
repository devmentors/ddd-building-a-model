using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order
{
    public sealed class Order
    {
        private List<OrderLine> _lines;
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<OrderLine> Lines { get => _lines; }
        public Shipment Shipment { get; private set; }
        public Payment Payment { get; private set; }
        public DateTime PlaceDateUtc { get; }
        public OrderStatus Status { get; private set; }
        public bool IsCompleted => Status is OrderStatus.Canceled || Status is OrderStatus.Completed;

        private Order()
        {
        }
        
        private Order(
            Guid customerId,
            IReadOnlyCollection<OrderLine> lines,
            Shipment shipment,
            Payment payment,
            DateTime placeDateUtc,
            Guid? id = null)
        {
            Status = OrderStatus.Placed;

            CustomerId = customerId;
            //Also some checks, ie. if at least one product etc...
            _lines = lines.ToList();
            Shipment = shipment ?? throw new ArgumentNullException(nameof(shipment));
            Payment = payment ?? throw new ArgumentNullException(nameof(payment));
            PlaceDateUtc = placeDateUtc;

            if (id is not null)
            {
                Id = id.Value;
            }
        }

        internal static Order CreateFromCheckout(CheckoutCart checkoutCart)
        {
            var orderLines = checkoutCart.Products
                .Select((x, i) => new OrderLine(i, x.Product.SKU, x.Product.Name, x.Product.Price, x.Quantity))
                .ToList();

            var shipmentOrderLineNumber = orderLines.Max(x => x.OrderLineNumber) + 1;
            var shipmentLine = new OrderLine(shipmentOrderLineNumber, default,"Shipment", 10, 1);
            
            orderLines.Add(shipmentLine);
            
            return new Order(checkoutCart.CustomerId, orderLines, checkoutCart.Shipment, checkoutCart.Payment, DateTime.UtcNow);
        }

        internal void Complete()
        {
            if (Status is OrderStatus.Canceled)
            {
                throw new InvalidOrderStatusChangeException();
            }

            Status = OrderStatus.Completed;
        }

        internal void Cancel()
        {
            if (Status is OrderStatus.Completed)
            {
                throw new InvalidOrderStatusChangeException();
            }

            Status = OrderStatus.Canceled;
        }
    }

    public enum OrderStatus
    {
        Placed = 1,
        Completed = 2,
        Canceled = 3
    }
}
