namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Application.Commands.PlaceOrder;

public record PlaceOrderFromCheckoutCommand(Guid CheckoutId);