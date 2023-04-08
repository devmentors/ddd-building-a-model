using DDD.BuildingBlocks.V9.OutOfProcDependency.Core.Order;
using Microsoft.EntityFrameworkCore;

namespace DDD.BuildingBlocks.V9.OutOfProcDependency.Infrastructure.Data;

internal class SalesDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}