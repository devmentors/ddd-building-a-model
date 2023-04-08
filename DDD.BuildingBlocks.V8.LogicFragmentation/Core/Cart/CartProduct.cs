using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Exceptions;
using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Shared;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Cart
{
    public class CartProduct
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        
        public CartProduct(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
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
