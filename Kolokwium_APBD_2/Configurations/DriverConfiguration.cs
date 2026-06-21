using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(e => e.DriverId);
        
        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);
        builder.Property(e => e.LicenceNumber).HasMaxLength(17);
        
        builder.ToTable("Driver");
        
        builder.HasMany(e => e.Deliveries)
            .WithOne(d => d.Driver)
            .HasForeignKey(d => d.DriverId);

        builder.HasData(new List<Driver>
        {
            new() { DriverId = 1, FirstName = "Jan", LastName = "Kowalski", LicenceNumber = "123456789"},
            new() { DriverId = 2, FirstName = "John", LastName = "Nowak", LicenceNumber = "123456788"},
            new() { DriverId = 3, FirstName = "Wiktor", LastName = "Kobierski", LicenceNumber = "123456787"}
        });

    }
}