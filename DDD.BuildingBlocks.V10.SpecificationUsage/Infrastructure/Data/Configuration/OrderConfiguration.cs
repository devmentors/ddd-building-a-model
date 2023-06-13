using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Order;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>, IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Lines);

        builder.Property(x => x.PlaceDateUtc);

        builder.Property(x => x.Shipment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Shipment>(x));
        
        builder.Property(x => x.Payment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Payment>(x));
    }

    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.SKU);
        builder.Property(x => x.Quantity);
        builder.Property(x => x.OrderLineNumber);
        builder.Property(x => x.UnitPrice);
    }
}