using DDD.BuildingBlocks.V8.LogicFragmentation.Core.Order;
using Microsoft.EntityFrameworkCore;

namespace DDD.BuildingBlocks.V8.LogicFragmentation.Infrastructure.Data;

internal class SalesDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}