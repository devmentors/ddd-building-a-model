using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data.Configuration;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.HasKey(x => new {x.CartId, x.ProductId});
        builder.HasOne(x => x.Product);
    }
}