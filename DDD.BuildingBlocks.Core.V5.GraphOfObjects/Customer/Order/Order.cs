using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Cart;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Exceptions;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Shared;

namespace DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Order
{
    public sealed class Order
    {
        private List<OrderLine> _lines;
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<OrderLine> Lines { get => _lines; }
        public Shipment Shipment { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsCompleted => Status is OrderStatus.Canceled || Status is OrderStatus.Completed;
        public DateTime PlaceDateUtc { get; init; }
        
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
                .Select((x, i) => new OrderLine(i, x.Product.Name, x.Product.Price, x.Quantity))
                .ToList();

            var shipmentOrderLineNumber = orderLines.Max(x => x.OrderLineNumber) + 1;
            var shipmentLine = new OrderLine(shipmentOrderLineNumber, "Shipment", 10, 1);
            
            orderLines.Add(shipmentLine);
            
            return new Order(checkoutCart.CustomerId, orderLines, checkoutCart.Shipment, checkoutCart.Payment, DateTime.UtcNow);
        }

        internal static Order CreateFromRawData(
            Guid customerId,
            IReadOnlyCollection<OrderLine> lines,
            Shipment shipment,
            Payment payment,
            DateTime placeDateUtc,
            Guid? id = null) 
            => new Order(customerId, lines, shipment, payment, placeDateUtc, id);

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
