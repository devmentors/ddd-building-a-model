using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Cart;

namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Repositories;

public interface ICheckoutRepository
{
    Task<CheckoutCart> GetById(Guid id, CancellationToken cancellationToken);
}