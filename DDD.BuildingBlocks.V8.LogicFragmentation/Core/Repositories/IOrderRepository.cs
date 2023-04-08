namespace DDD.BuildingBlocks.V8.LogicFragmentation.Core.Repositories;

public interface IOrderRepository
{
    Task<bool> Exists(Specification<Order.Order> specification, CancellationToken cancellationToken);
    Task Save(Order.Order order, CancellationToken cancellationToken);
}