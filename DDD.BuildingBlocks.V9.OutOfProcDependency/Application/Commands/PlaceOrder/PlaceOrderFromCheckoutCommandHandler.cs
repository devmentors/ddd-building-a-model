using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Exceptions;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Order;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Repositories;

namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Application.Commands.PlaceOrder;

public class PlaceOrderFromCheckoutCommandHandler
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IOrderRepository _orderRepository;

    public PlaceOrderFromCheckoutCommandHandler(
        ICheckoutRepository checkoutRepository,
        IOrderRepository orderRepository)
    {
        _checkoutRepository = checkoutRepository ?? throw new ArgumentNullException(nameof(checkoutRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }
    
    public async Task Handle(PlaceOrderFromCheckoutCommand command, CancellationToken cancellationToken)
    {
        var checkout = await _checkoutRepository.GetById(command.CheckoutId, cancellationToken);

        var order = await checkout.PlaceOrder(_orderRepository);

        await _orderRepository.Save(order, cancellationToken);
    }
}