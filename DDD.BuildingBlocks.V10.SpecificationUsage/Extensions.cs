using DDD.BuildingBlocks.V10.SpecificationUsage.Application.Commands.PlaceOrder;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;
using DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.BuildingBlocks.V10.SpecificationUsage;

public static class Extensions
{
    public static void RegisterV10SpecificationUsage(this IServiceCollection services)
    {
        services.AddDbContext<SalesDbContext>();
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ICheckoutRepository, CheckoutRepository>();
        services.AddTransient<PlaceOrderFromCheckoutCommandHandler>();
    }
    
    public static void InitV10SpecificationUsage(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SalesDbContext>();
        context.Database.Migrate();
    }
}