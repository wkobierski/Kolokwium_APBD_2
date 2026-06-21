using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId);
        
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Price).HasColumnType("decimal(10,2)");
        
        builder.ToTable("Product");
        
        builder.HasMany(e => e.ProductDeliveries)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId);

        builder.HasData(new List<Product>
        {
            new() {ProductId = 1, Name = "Produkt 1", Price = 10},
            new() {ProductId = 2, Name = "Produkt 2", Price = 20},
            new() {ProductId = 3, Name = "Produkt 3", Price = 30}
        });
    }
}