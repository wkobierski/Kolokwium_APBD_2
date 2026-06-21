using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.HasKey(e => e.DeliveryId);
        
        builder.Property(e => e.Date).HasColumnType("datetime");
        
        builder.ToTable("Delivery");
        
        builder.HasOne(d => d.Customer)
            .WithMany(c => c.Deliveries)
            .HasForeignKey(d => d.CustomerId);
        
        builder.HasOne(d => d.Driver)
            .WithMany(d => d.Deliveries)
            .HasForeignKey(d => d.DriverId);

        builder.HasData(new List<Delivery>
        {
            new() { DeliveryId = 1, CustomerId = 1, DriverId = 1, Date = DateTime.Now },
            new() { DeliveryId = 2, CustomerId = 2, DriverId = 2, Date = DateTime.Now },
            new() { DeliveryId = 3, CustomerId = 3, DriverId = 3, Date = DateTime.Now }
        });
    }
}