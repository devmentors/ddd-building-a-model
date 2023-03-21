namespace DDD.BuildingBlocks.Core.V2.Encapsulated.Models
{
    public class OrderProduct
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        
        public OrderProduct(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }
            Quantity = quantity;
        }
    }
}
