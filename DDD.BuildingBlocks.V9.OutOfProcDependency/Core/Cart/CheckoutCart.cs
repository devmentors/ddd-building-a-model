using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Exceptions;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Order;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Repositories;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Shared;

namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Cart;

public sealed class CheckoutCart
{
    private List<CartProduct> _products = new();

    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Payment Payment { get; private set; }
    public Shipment Shipment { get; private set; }
    public IReadOnlyCollection<CartProduct> Products => _products;

    internal CheckoutCart(Cart cart, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CustomerId = cart.CustomerId;
        _products = cart.Products.ToList();
    }

    public void SetShipment(Shipment shipment)
        => Shipment = shipment;

    public void SetPayment(Payment payment)
    {
        var cartValue = _products.Sum(x => x.Quantity * x.Product.Price);

        // does this logic belong to the Cart itself?
        if (cartValue > 20_000 && payment.PaymentMethod is PaymentMethod.Cash)
        {
            throw new PaymentMethodNotAllowedException();
        }

        Payment = payment;
    }

    public async Task<Order.Order> PlaceOrder(IOrderRepository orderRepository)
    {
        /*
         * This is obviously a naive implementation but in same cases might yield better results
         * than using JOINs, since we can issue simpler queries in parallel and then
         * scatter and collect with Task.WhenAll(...)
         */
        foreach (var product in Products)
        {
            var orderWithLimitedProductExistsInThisQuarter = await orderRepository.Exists(
                specification: new ActiveOrderWithLimitedProductThisQuarter(CustomerId, product.Product.Name),
                cancellationToken: CancellationToken.None);

            if (orderWithLimitedProductExistsInThisQuarter)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }
        }
        
        return Order.Order.CreateFromCheckout(this);
    }
}