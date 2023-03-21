using DDD.BuildingBlocks.Core.V1.DAL;
using DDD.BuildingBlocks.Core.V1.Exceptions;
using DDD.BuildingBlocks.Core.V1.Inputs;
using DDD.BuildingBlocks.Core.V1.Models;

namespace DDD.BuildingBlocks.Core.V1.BLL
{
    public class OrderService
    {
        public Guid CreateOrder(CreateOrderInput input)
        {
            var products = NaiveRepository.GetProductsById(input.Products.Select(x => x.ProductId).ToList())
                .ToDictionary(x => x.Id, x => x);

            var orderProducts = input.Products
                .Select(product => new OrderProduct
                {
                    Product = products.GetValueOrDefault(product.ProductId),
                    Quantity = product.Quantity
                })
                .ToList();

            var customer = NaiveRepository.GetCustomerById(input.CustomerId);

            var order = new Order
            {
                Products = orderProducts,
                City = input.Shipment.City,
                Street = input.Shipment.StreetName,
                StreetNumber = input.Shipment.StreetNumber,
                PaymentMethod = input.Payment.PaymentMethod,
                Customer = customer
            };

            NaiveRepository.SaveOrder(order);

            return order.Id;
        }

        public void AddProductToOrder(Guid productId, int quantity, Guid orderId)
        {
            var product = NaiveRepository.GetProductById(productId);

            var order = NaiveRepository.GetOrderById(orderId);

            if (order.Products.Count >= 2)
            {
                throw new ExceededMaximumQuantityLimitException();
            }
            
            var productOrder = new OrderProduct
            {
                Product = product,
                Quantity = quantity
            };

            if (order.Status is not OrderStatus.BeforeCheckout)
            {
                throw new CannotAddProductToPlacedOrderException();
            }

            order.Products.Add(productOrder);

            NaiveRepository.SaveOrder(order);
        }

        public void PlaceOrder(Guid orderId)
        {
            var order = NaiveRepository.GetOrderById(orderId);

            order.Status = OrderStatus.Placed;

            NaiveRepository.SaveOrder(order);
        }

        public void CancelOrder(Guid orderId)
        {
            var order = NaiveRepository.GetOrderById(orderId);

            order.Status = OrderStatus.Canceled;

            NaiveRepository.SaveOrder(order);
        }

        public IReadOnlyCollection<Order> GetAllOrders() => NaiveRepository.GetAllOrders();
        public Order GetOrderById(Guid orderId) => NaiveRepository.GetOrderById(orderId);
    }
}
