using DDD.BuildingBlocks.Core.V2.Encapsulated.DAL;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.App
{
    public class ProductService
    {
        public Guid CreateProduct(string name, string sku, decimal price, bool limitedAvailability = false)
        {
            var product = new Product(name, sku, price, limitedAvailability);

            NaiveRepository.SaveProduct(product);
            return product.Id;
        }

        public void AddProductToOrder(Guid productId, int quantity, Guid orderId)
        {
            var product = NaiveRepository.GetProductById(productId);

            var productOrder = new OrderProduct(product, quantity);

            var order = NaiveRepository.GetOrderById(orderId);

            order.AddProduct(productOrder);

            NaiveRepository.SaveOrder(order);
        }

        public IReadOnlyCollection<Product> GetAllProducts() => NaiveRepository.GetAllProducts();
        public Product GetProductById(Guid productId) => NaiveRepository.GetProductById(productId);
    }
}
