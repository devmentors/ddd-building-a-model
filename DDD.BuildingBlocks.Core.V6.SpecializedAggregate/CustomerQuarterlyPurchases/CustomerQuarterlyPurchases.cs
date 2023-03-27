using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Cart;
using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Exceptions;
using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Order;
using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Shared;

namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.CustomerQuarterlyPurchases;

public class CustomerQuarterlyPurchases
{
    public Guid CustomerId { get; }
    
    private YearQuarter _quarter;
    private readonly List<Order.Order> _ordersInQuarter = new();
    private bool _isCurrentQuarter => _quarter.Equals(new YearQuarter(DateTime.UtcNow));
    
    public CustomerQuarterlyPurchases(Guid customerId,YearQuarter quarter)
    {
        CustomerId = customerId;
        _quarter = quarter ?? throw new ArgumentNullException(nameof(quarter));
    }

    
    public Order.Order PlaceOrderFromCheckout(CheckoutCart checkoutCart)
    {
        //Adding aspect of time here gives some interesting space for optimization
        //i.e. historical quarters won't be accessed that often as current quarter (so we can store them differently etc.)
        if (_isCurrentQuarter is false)
        {
            throw new CannotPlaceOrderInHistoricalQuarterException();
        }
        
        EnsureOrderWillNotViolateProductAvailabilityLimits(checkoutCart);
        return checkoutCart.PlaceOrder();
    }

    private void EnsureOrderWillNotViolateProductAvailabilityLimits(CheckoutCart checkout)
    {
        var limitedAvailabilityProductsInCheckout =
            checkout.Products.Where(x => x.Product.LimitedAvailability);

        foreach (var limitedProduct in limitedAvailabilityProductsInCheckout)
        {
            if (limitedProduct.Quantity > 1)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }

            //DISCUSS: Perhaps place order date and finalize order date should be treated differently (edge case)
            var nonCanceledOrdersThisQuarter = _ordersInQuarter
                .Where(x => x.Status is not OrderStatus.Canceled);

            var productAlreadyPurchasedThisQuarter = nonCanceledOrdersThisQuarter.SelectMany(x => x.Lines)
                //TODO: Do we introduce SKU on OrderLine? Or match products with lines in some other way?
                .Any(x => x.Name.Equals(limitedProduct.Product.Name));

            if (productAlreadyPurchasedThisQuarter)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }
        }
    }
}