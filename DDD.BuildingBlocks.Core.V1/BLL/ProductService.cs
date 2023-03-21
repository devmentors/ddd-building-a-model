using DDD.BuildingBlocks.Core.V1.DAL;
using DDD.BuildingBlocks.Core.V1.Models;

namespace DDD.BuildingBlocks.Core.V1.BLL
{
    public class ProductService
    {
        public Guid CreateProduct(string name, string sku, decimal price)
        {
            var product = new Product
            {
                Name = name,
                SKU = sku,
                Price = price
            };

            NaiveRepository.SaveProduct(product);
            return product.Id;
        }

        public void AddProductToOrder(Guid productId, int quantity, Guid orderId)
        {
            var product = NaiveRepository.GetProductById(productId);

            var productOrder = new OrderProduct
            {
                Product = product,
                Quantity = quantity
            };

            var order = NaiveRepository.GetOrderById(orderId);

            order.Products.Add(productOrder);

            NaiveRepository.SaveOrder(order);
        }

        public IReadOnlyCollection<Product> GetAllProducts() => NaiveRepository.GetAllProducts();
        public Product GetProductById(Guid productId) => NaiveRepository.GetProductById(productId);
    }
}
