using DDD.BuildingBlocks.Core.V3.Decomposed.Exceptions;
using DDD.BuildingBlocks.Core.V3.Decomposed.Shared;

namespace DDD.BuildingBlocks.Core.V3.Decomposed.Order
{
    public sealed class Order
    {
        private List<OrderProduct> _products;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Customer Customer { get; private set; }
        public IReadOnlyCollection<OrderProduct> Products { get => _products; }
        public CustomerAddress CustomerAddress { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus Status { get; private set; }

        public bool IsCompleted => Status is OrderStatus.Canceled || Status is OrderStatus.Completed;
        
        internal Order(
            Customer customer,
            IReadOnlyCollection<OrderProduct> products,
            CustomerAddress customerAddress,
            Payment payment,
            Guid? id = null)
        {
            Status = OrderStatus.Placed;

            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            //Also some checks, ie. if at least one product etc...
            _products = products.ToList();
            CustomerAddress = customerAddress ?? throw new ArgumentNullException(nameof(customerAddress));
            Payment = payment ?? throw new ArgumentNullException(nameof(payment));

            if (id is not null)
            {
                Id = id.Value;
            }
        }

        public void Complete()
        {
            if (Status is OrderStatus.Canceled)
            {
                throw new InvalidOrderStatusChangeException();
            }

            Status = OrderStatus.Completed;
        }

        public void Cancel()
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
