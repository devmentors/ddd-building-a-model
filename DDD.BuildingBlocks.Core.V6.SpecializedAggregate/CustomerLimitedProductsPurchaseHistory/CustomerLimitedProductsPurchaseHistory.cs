using DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Exceptions;

namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.CustomerLimitedProductsPurchaseHistory;

public class CustomerLimitedProductsPurchaseHistory
{
    public Guid CustomerId { get; }
    
    private const int ProductLimit = 1;
    private readonly List<Guid> _productIds = new();

    public CustomerLimitedProductsPurchaseHistory(Guid customerId)
        => CustomerId = customerId;

    public void RegisterLimitedProduct(Guid productId)
    {
        if (_productIds.Count(x => x == productId) is ProductLimit)
        {
            throw new CannotRegisterRegisterLimitedProductsException();
        }
        
        _productIds.Add(productId);
    }
}