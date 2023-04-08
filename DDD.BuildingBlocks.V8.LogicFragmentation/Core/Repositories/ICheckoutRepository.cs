using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Cart;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Repositories;

public interface ICheckoutRepository
{
    Task<CheckoutCart> GetById(Guid id, CancellationToken cancellationToken);
}