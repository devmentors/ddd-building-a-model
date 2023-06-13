using System.Text.Json.Serialization;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data.Configuration;

internal sealed class CheckoutCartConfiguration : IEntityTypeConfiguration<CheckoutCart>
{
    public void Configure(EntityTypeBuilder<CheckoutCart> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerId);
        builder.HasMany(x => x.Products);
        
        builder.Property(x => x.Shipment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Shipment>(x));
        
        builder.Property(x => x.Payment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Payment>(x));
    }
}