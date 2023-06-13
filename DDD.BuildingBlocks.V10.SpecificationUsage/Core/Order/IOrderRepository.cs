using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;

public interface IOrderRepository
{
    Task<bool> Exists(Specification<Core.Order.Order> specification, CancellationToken cancellationToken);
    Task Save(Core.Order.Order order, CancellationToken cancellationToken);
}