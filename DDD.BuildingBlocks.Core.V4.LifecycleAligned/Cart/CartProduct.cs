﻿using DDD.BuildingBlocks.Core.V4.LifecycleAligned.Exceptions;
using DDD.BuildingBlocks.Core.V4.LifecycleAligned.Shared;

namespace DDD.BuildingBlocks.Core.V4.LifecycleAligned.Cart
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
