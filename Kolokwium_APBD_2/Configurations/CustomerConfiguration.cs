using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.CustomerId);
        
        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);
        builder.Property(e => e.BirthDate).HasColumnType("datetime");
        
        builder.ToTable("Customer");
        
        builder.HasMany(e => e.Deliveries)
            .WithOne(d => d.Customer)
            .HasForeignKey(d => d.CustomerId);

        builder.HasData(new List<Customer>
        {
            new() { CustomerId = 1, FirstName = "Ania", LastName = "Kowalska", BirthDate = DateTime.Now },
            new() { CustomerId = 2, FirstName = "Patrycja", LastName = "Nowak", BirthDate = DateTime.Now },
            new() { CustomerId = 3, FirstName = "Maja", LastName = "Kobierska", BirthDate = DateTime.Now }
        });
    }
}