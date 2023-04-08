using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Order;
using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Infrastructure.Data;

internal class OrderRepository : IOrderRepository
{
    private readonly SalesDbContext _dbContext;

    public OrderRepository(SalesDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<bool> Exists(Specification<Order> specification, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.Where(specification).AnyAsync(cancellationToken);
    }

    public async Task Save(Order order, CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}