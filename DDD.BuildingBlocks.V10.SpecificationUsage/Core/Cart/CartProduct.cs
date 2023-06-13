using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart
{
    public class CartProduct
    {
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        private CartProduct()
        {
        }
        
        public CartProduct(Guid cartId, Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            CartId = cartId;
            ProductId = product.Id;
            
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            Quantity = quantity;
        }

        public void IncreaseQuantity() => Quantity++;
        public void DecreaseQuantity()
        {
            if (Quantity - 1 < 0)
            {
                throw new InvalidCartProductQuantityException();
            }

            Quantity--;
        }
    }
}
