using DDD.BuildingBlocks.Core.V2.Encapsulated.Exceptions;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.Models
{
    public class Order
    {
        private List<OrderProduct> _products;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Customer Customer { get; private set; }
        public IReadOnlyCollection<OrderProduct> Products { get => _products; }
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public int StreetNumber { get; private set; }
        public PaymentMethod PaymentMethodMethod { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsCompleted => Status is OrderStatus.Canceled || Status is OrderStatus.Completed;
        
        public Order(
            Customer customer,
            IReadOnlyCollection<OrderProduct> products,
            string city,
            string streetName,
            int streetNumber,
            PaymentMethod paymentMethod,
            Guid? id = null)
        {
            Status = OrderStatus.BeforeCheckout;
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _products = products.ToList();

            SetAddress(city, streetName, streetNumber);
            PaymentMethodMethod = paymentMethod;

            if (id is not null)
            {
                Id = id.Value;
            }
        }


        public void AddProduct(OrderProduct orderProduct)
        {
            if (Status is not OrderStatus.BeforeCheckout)
            {
                throw new CannotAddProductToPlacedOrderException();
            }

            _products.Add(orderProduct);
        }

        public void SetAddress(string city, string streetName, int streetNumber)
        {
            if (IsCompleted)
            {
                throw new CannotChangeAddressException();
            }
            
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new InvalidAddressException();
            }
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new InvalidAddressException();
            }

            City = city;
            StreetName = streetName;
            StreetNumber = streetNumber;
        }

        public void Cancel()
        {
            SetStatus(OrderStatus.Canceled);
        }

        public void Place()
        {
            SetStatus(OrderStatus.Placed);
        }

        private void SetStatus(OrderStatus status)
        {
            if (IsCompleted)
            {
                throw new OrderAlreadyCompletedException();
            }
            Status = status;
        }
    }

    public enum OrderStatus
    {
        BeforeCheckout = 1,
        Placed = 2,
        Completed = 3,
        Canceled = 4
    }
}
