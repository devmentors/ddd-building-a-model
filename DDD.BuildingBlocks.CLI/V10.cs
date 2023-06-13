using DDD.BuildingBlocks.V10.SpecificationUsage;
using DDD.BuildingBlocks.V10.SpecificationUsage.Application.Commands.PlaceOrder;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.BuildingBlocks.CLI;

public class V10
{
    public static async Task Run()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.RegisterV10SpecificationUsage();

        var serviceProvider = services.BuildServiceProvider();
        //serviceProvider.InitV10SpecificationUsage();

        var context = serviceProvider.GetRequiredService<SalesDbContext>();

        var customerId = Guid.NewGuid();
        var cart = new Cart(customerId);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "PC",
            Price = 2100.37m,
            SKU = "1234",
            LimitedAvailability = true
        };
        
        cart.AddProduct(product);
        var checkout = cart.Checkout();

        var shipment = new Shipment("Warsaw", "Test", 12, "DevMentors");
        var payment = new Payment(PaymentMethod.Cash, PayerType.B2B);
        
        checkout.SetShipment(shipment);
        checkout.SetPayment(payment);
        //
        // await context.AddAsync(checkout);
        // await context.SaveChangesAsync();
        //
        // return;

        var checkoutId = serviceProvider.GetRequiredService<SalesDbContext>().Checkouts.FirstOrDefault().Id;
        var command = new PlaceOrderFromCheckoutCommand(checkoutId);
        var handler = serviceProvider.GetRequiredService<PlaceOrderFromCheckoutCommandHandler>();
        await handler.Handle(command, default);
    }
}