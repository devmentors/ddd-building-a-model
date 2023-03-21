using DDD.BuildingBlocks.Core.V2.Encapsulated.DAL;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Inputs;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.App
{
    public class OrderService
    {
        public Guid CreateOrder(CreateOrderInput input)
        {
            var products = NaiveRepository.GetProductsById(input.Products.Select(x => x.ProductId).ToList())
                .ToDictionary(x => x.Id, x => x);

            var orderProducts = input.Products
                .Select(product => new OrderProduct(products.GetValueOrDefault(product.ProductId), product.Quantity))
                .ToList();

            var customer = NaiveRepository.GetCustomerById(input.CustomerId);

            var order = new Order
            (
                customer,
                orderProducts,
                input.City,
                input.StreetName,
                input.StreetNumber,
                input.PaymentMethod
            );

            NaiveRepository.SaveOrder(order);

            return order.Id;
        }

        public void AddProductToOrder(Guid productId, int quantity, Guid orderId)
        {
            var product = NaiveRepository.GetProductById(productId);

            var productOrder = new OrderProduct(product, quantity);

            var order = NaiveRepository.GetOrderById(orderId);

            order.AddProduct(productOrder);

            NaiveRepository.SaveOrder(order);
        }

        public void PlaceOrder(Guid orderId)
        {
            var order = NaiveRepository.GetOrderById(orderId);

            order.Place();

            NaiveRepository.SaveOrder(order);
        }

        public void CancelOrder(Guid orderId)
        {
            var order = NaiveRepository.GetOrderById(orderId);

            order.Cancel();

            NaiveRepository.SaveOrder(order);
        }

        public IReadOnlyCollection<Order> GetAllOrders() => NaiveRepository.GetAllOrders();
        public Order GetOrderById(Guid orderId) => NaiveRepository.GetOrderById(orderId);
    }
}
