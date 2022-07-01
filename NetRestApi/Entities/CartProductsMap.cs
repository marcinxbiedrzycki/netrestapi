using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetRestApi.Entities;

public class CartProductsMap : IEntityTypeConfiguration<CartProducts>
{
    public void Configure(EntityTypeBuilder<CartProducts> builder)
    {
        builder.HasKey(x => new { x.CartId, x.ProductId });

        builder.HasOne(x => x.Cart).WithMany(x => x.CartProducts).HasForeignKey(x => x.CartId);
        builder.HasOne(x => x.Product).WithMany(x => x.CartProducts).HasForeignKey(x => x.ProductId);
    }
}