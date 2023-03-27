using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Exceptions;
using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Shared;

namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Cart
{
    public class Cart
    {
        private List<CartProduct> _products = new();

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<CartProduct> Products => _products;

        public Cart(Guid customerId)
            => CustomerId = customerId;

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

        public CheckoutCart Checkout()
        {
            if (Products.Count == 0)
            {
                throw new CannotCheckoutEmptyCartException();
            }
            return new(this);
        }
    }
}
