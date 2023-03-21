﻿using DDD.BuildingBlocks.Core.V2.Encapsulated.DAL.Exceptions;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.DAL
{
    public static class NaiveRepository
    {
        public static Product GetProductById(Guid productId)
        {
            NaiveDatabase.Products.TryGetValue(productId, out var product);
            
            if (product is null)
            {
                throw new RowNotFoundException();
            }

            return product;
        }

        public static IReadOnlyCollection<Product> GetProductsById(ICollection<Guid> productIds)
        {
            return productIds.Select(GetProductById).ToList();
        }

        public static IReadOnlyCollection<Product> GetAllProducts() => NaiveDatabase.Products.Values.ToList();

        public static Order GetOrderById(Guid orderId)
        {
            NaiveDatabase.Orders.TryGetValue(orderId, out var order);
            
            if (order is null)
            {
                throw new RowNotFoundException();
            }

            return order;
        }

        public static IReadOnlyCollection<Order> GetAllOrders() => NaiveDatabase.Orders.Values.ToList();

        public static Customer GetCustomerById(Guid customerId)
        {
            NaiveDatabase.Customers.TryGetValue(customerId, out var customer);
            
            if (customer is null)
            {
                throw new RowNotFoundException();
            }

            return customer;
        }

        public static IReadOnlyCollection<Customer> GetAllCustomers() => NaiveDatabase.Customers.Values.ToList();

        public static void SaveOrder(Order order)
        {
            NaiveDatabase.Orders.TryGetValue(order.Id, out var existingOrder);
            
            if (existingOrder is not null)
            {
                NaiveDatabase.Orders[order.Id] = order;
            }
            else
            {
                NaiveDatabase.Orders.Add(order.Id, order);
            }
        }

        public static void SaveProduct(Product product)
        {
            NaiveDatabase.Products.TryGetValue(product.Id, out var existingProduct);
            
            if (existingProduct is not null)
            {
                NaiveDatabase.Products[product.Id] = product;
            }
            else
            {
                NaiveDatabase.Products.Add(product.Id, product);
            }
        }

        private static class NaiveDatabase
        {
            public static Dictionary<Guid, Product> Products = new()
            {
                {
                    Guid.Parse("05cc59cc-d90c-41a9-b51d-3703618d66ca"),
                    new Product
                    (
                        "Intel Core i9 12345K",
                        $"PRODUCT/{123}",
                        new decimal(1500.99),
                        false,
                        Guid.Parse("05cc59cc-d90c-41a9-b51d-3703618d66ca")
                    )
                },
                {
                    Guid.Parse("594e7ce0-9e35-4d9b-b241-2bfb4e73abf2"),
                    new Product
                    (
                        "NVIDIA RTX 9070 Super Ultra",
                        $"PRODUCT/{234}",
                        new decimal(7150.99),
                        true,
                        Guid.Parse("594e7ce0-9e35-4d9b-b241-2bfb4e73abf2")
                    )
                }
            };

            public static Dictionary<Guid, Customer> Customers = new()
            {
                {
                    Guid.Parse("3d5cd8c2-71a1-42e2-9abd-b3c7555814f7"), 
                    new Customer
                    (
                        "John Smith",
                        "Address of John Smith",
                        Guid.Parse("3d5cd8c2-71a1-42e2-9abd-b3c7555814f7")
                    )
                }
            };

            public static Dictionary<Guid, Order> Orders = new();
        }
    }
}
