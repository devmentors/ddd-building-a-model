using DDD.BuildingBlocks.Core.V3.Decomposed.Exceptions;
using DDD.BuildingBlocks.Core.V3.Decomposed.Order;
using DDD.BuildingBlocks.Core.V3.Decomposed.Shared;

namespace DDD.BuildingBlocks.Core.V3.Decomposed.Cart
{
    public class Cart
    {
        private List<CartProduct> _products = new();

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Customer Customer { get; private set; }
        public Payment Payment { get; private set; }
        public CustomerAddress CustomerAddress { get; private set; }
        public IReadOnlyCollection<CartProduct> Products => _products;

        public Cart(Customer customer)
            => Customer = customer;

        public void AddProduct(Product product)
        {
            var cartProduct = _products.SingleOrDefault(x => x.Product.Id == product.Id);

            if (cartProduct is {Quantity: > 2})
            {
                throw new ExceededMaximumQuantityLimitException();
            }

            if (cartProduct is {Quantity: <= 2})
            {
                cartProduct.IncreaseQuantity();
                return;
            }

            _products.Add(new CartProduct(product, 1));
        }

        public void RemoveProduct(Product product)
        {
            var cartProduct = _products.SingleOrDefault(x => x.Product.Id == product.Id);

            if (cartProduct is null)
            {
                throw new CartProductNotFoundException();
            }

            cartProduct.DecreaseQuantity();
        }

        public void Clear() => _products.Clear();

        public void SetShipment(CustomerAddress customerAddress)
            => CustomerAddress = customerAddress;

        public void SetPayment(Payment payment)
        {
            var cartValue = _products.Sum(x => x.Quantity * x.Product.Price);

            // do this logic belong to the Cart itself?
            if (cartValue > 20_000 && payment.PaymentMethod is PaymentMethod.Cash)
            {
                throw new PaymentMethodNotAllowedException();
            }

            Payment = payment;
        }

        public Order.Order CreateOrder()
        {
            var orderProducts = _products
                .Select(x => new OrderProduct(x.Product, x.Quantity))
                .ToList();

            Clear();
            return new Order.Order(Customer,orderProducts, CustomerAddress, Payment, Id);
        }
    }
}
