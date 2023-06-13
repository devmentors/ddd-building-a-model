using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data;

internal class OrderRepository : IOrderRepository
{
    private readonly SalesDbContext _dbContext;
    private readonly ILogger<IOrderRepository> _logger;

    public OrderRepository(SalesDbContext dbContext, ILogger<IOrderRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger;
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