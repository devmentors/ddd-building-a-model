using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;
using DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data;

public class SalesDbContext : DbContext
{
    public DbSet<CheckoutCart> Checkouts { get; set; }
    public DbSet<Order> Orders { get; set; }

    public SalesDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CartProductConfiguration());
        modelBuilder.ApplyConfiguration(new CheckoutCartConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
        modelBuilder.ApplyConfiguration<OrderLine>(new OrderConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=ddd;Username=postgres;Password=");
        optionsBuilder.LogTo(Console.WriteLine);
    }
}