using Kolokwium_APBD_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_APBD_2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    // TODO: Dodaj DbSet dla każdej encji z zadania
    public DbSet<SampleEntity> SampleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
