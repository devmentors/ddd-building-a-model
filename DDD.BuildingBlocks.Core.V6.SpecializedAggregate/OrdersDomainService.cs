using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Cart;

namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate;

public sealed class OrdersDomainService
{
    public void PlaceOrder(CheckoutCart checkout)
    {
        var history = new CustomerLimitedProductsPurchaseHistory.CustomerLimitedProductsPurchaseHistory(Guid.NewGuid());

        var limitedAvailabilityProductsInCheckout = checkout.Products
            .Where(x => x.Product.LimitedAvailability);

        foreach (var product in limitedAvailabilityProductsInCheckout)
        {
            history.RegisterLimitedProduct(product.Product.Id);
        }

        var order = checkout.PlaceOrder();
        
        //SAVE history
        //SAVE order
    }
}