using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium_APBD_2.Configurations;

public class SampleEntityConfiguration : IEntityTypeConfiguration<SampleEntity>
{
    public void Configure(EntityTypeBuilder<SampleEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.ToTable("SampleEntities");

        // Relacja one-to-many (jeśli potrzebna):
        // builder.HasMany(e => e.RelatedEntities)
        //        .WithOne(r => r.SampleEntity)
        //        .HasForeignKey(r => r.SampleEntityId);

        // Seed data — min. 3 rekordy:
        builder.HasData(new List<SampleEntity>
        {
            new() { Id = 1, Name = "Record1" },
            new() { Id = 2, Name = "Record2" },
            new() { Id = 3, Name = "Record3" },
        });
    }
}

// ===== WZORZEC: konfiguracja tabeli łączącej =====
// public class SampleJunctionConfiguration : IEntityTypeConfiguration<SampleJunction>
// {
//     public void Configure(EntityTypeBuilder<SampleJunction> builder)
//     {
//         builder.HasKey(e => new { e.SampleEntityId, e.OtherEntityId });
//
//         builder.HasOne(e => e.SampleEntity)
//                .WithMany(p => p.SampleJunctions)
//                .HasForeignKey(e => e.SampleEntityId)
//                .OnDelete(DeleteBehavior.Cascade);
//
//         builder.HasOne(e => e.OtherEntity)
//                .WithMany(o => o.SampleJunctions)
//                .HasForeignKey(e => e.OtherEntityId)
//                .OnDelete(DeleteBehavior.NoAction);
//
//         builder.HasData(new List<SampleJunction>
//         {
//             new() { SampleEntityId = 1, OtherEntityId = 1, Amount = 2 },
//         });
//     }
// }
