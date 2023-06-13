namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;

public interface ICheckoutRepository
{
    Task<CheckoutCart> GetById(Guid id, CancellationToken cancellationToken);
}