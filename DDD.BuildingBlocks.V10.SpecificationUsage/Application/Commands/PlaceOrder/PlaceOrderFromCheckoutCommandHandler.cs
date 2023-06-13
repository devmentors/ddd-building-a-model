using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Application.Commands.PlaceOrder;

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
                specification: new HasActiveOrderWithLimitedProductThisQuarter(order.CustomerId, orderLine.Name),
                cancellationToken: cancellationToken);

            if (orderWithLimitedProductExistsInThisQuarter)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }
        }

        await _orderRepository.Save(order, cancellationToken);
    }
}