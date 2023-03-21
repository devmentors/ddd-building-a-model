using System.Text.Json;
using DDD.BuildingBlocks.Core.V2.Encapsulated.App;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Inputs;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.CLI
{
    public static class V2
    {
        private static readonly OrderService _orderService;
        private static readonly ProductService _productService;
        private static readonly CustomerService _customerService;

        static V2()
        {
            _orderService = new OrderService();
            _productService = new ProductService();
            _customerService = new CustomerService();
        }
        
        public static void Run()
        {
            //User
            var customerId = _customerService.GetAllCustomers().First().Id;
            //Product he/she picked
            var products = _productService.GetAllProducts()
                .Select(_ => new CreateOrderProductInput
                {
                    ProductId = _.Id,
                    Quantity = 1
                })
                .Take(1)
                .ToList();
            
            var orderId = _orderService.CreateOrder(new CreateOrderInput
            {
                CustomerId = customerId,
                Products = products,
                City = "Warsaw",
                StreetName = "Sezamkowa St.", 
                StreetNumber = 123,
                PaymentMethod = PaymentMethod.Cash
            });

            Console.WriteLine("Order added...");
            PrintState();
            
            _orderService.PlaceOrder(orderId);
            Console.WriteLine("Order placed...");
            PrintState();

            try
            {
                Console.WriteLine($"Trying to add product to order via {nameof(OrderService)}");
                _orderService.AddProductToOrder(_productService.GetAllProducts().First().Id, 1, orderId);
                Console.WriteLine("(╯°□°）╯︵ ┻━┻ Data corruption");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(⌐■_■) Invariant protected - {ex.GetType().Name} thrown!");
            }
            
            try
            {
                Console.WriteLine($"Trying to add product to order via {nameof(ProductService)}");
                _productService.AddProductToOrder(_productService.GetAllProducts().First().Id, 1, orderId);
                Console.WriteLine("(╯°□°）╯︵ ┻━┻ Data corruption");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"(⌐■_■) Invariant protected - {ex.GetType().Name} thrown!");
            }
            
            Console.WriteLine("Done!");
        }

        private static void PrintState()
        {
            Console.WriteLine(ConsoleUtils.CurrentStateSeparator);
            
            Console.WriteLine(JsonSerializer.Serialize(_orderService.GetAllOrders(), new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }
    }
}
