using Microsoft.EntityFrameworkCore;
using VNExos.Domain.Entities;

namespace VNExos.Domain.Presistence;

public class VNExosContext(DbContextOptions<VNExosContext> options) : DbContext(options)
{
    public DbSet<Language> Languages { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Language>()
            .HasIndex(p => p.Code)
                .IsUnique(true);
    }
}
