namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Repositories;

public interface IOrderRepository
{
    Task<bool> Exists(Specification<Order.Order> specification, CancellationToken cancellationToken);
    Task Save(Order.Order order, CancellationToken cancellationToken);
}