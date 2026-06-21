using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class ProductDeliveryConfiguration : IEntityTypeConfiguration<ProductDelivery>
{
    public void Configure(EntityTypeBuilder<ProductDelivery> builder)
    {
        builder.HasKey(e => new { e.ProductId, e.DeliveryId });
        
        builder.ToTable("ProductDelivery");
        
        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductDeliveries)
            .HasForeignKey(e => e.ProductId);
        
        builder.HasOne(e => e.Delivery)
            .WithMany(p => p.ProductDeliveries)
            .HasForeignKey(e => e.DeliveryId);

        builder.HasData(new List<ProductDelivery>
        {
            new() { ProductId = 1, DeliveryId = 1, Amount = 1 },
            new() { ProductId = 2, DeliveryId = 2, Amount = 2 },
            new() { ProductId = 3, DeliveryId = 3, Amount = 3 }
        });
    }
}