using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Exceptions;
using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Order;
using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Repositories;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Application.Commands.PlaceOrder;

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

        var order = checkout.PlaceOrder();

        /*
         * This is obviously a naive implementation but in same cases might yield better results
         * than using JOINs, since we can issue simpler queries in parallel and then
         * scatter and collect with Task.WhenAll(...)
         */
        foreach (var orderLine in order.Lines)
        {
            var orderWithLimitedProductExistsInThisQuarter = await _orderRepository.Exists(
                specification: new ActiveOrderWithLimitedProductThisQuarter(order.CustomerId, orderLine.Name),
                cancellationToken: cancellationToken);

            if (orderWithLimitedProductExistsInThisQuarter)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }
        }

        await _orderRepository.Save(order, cancellationToken);
    }
}