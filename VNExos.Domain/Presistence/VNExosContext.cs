using Microsoft.EntityFrameworkCore;
using VNExos.Domain.Entities;

namespace VNExos.Domain.Presistence;

public class VNExosContext(DbContextOptions<VNExosContext> options) : DbContext(options)
{
    public DbSet<Language> Languages { get; set; } = default!;
    public DbSet<Translation> Translations { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Language>()
            .HasIndex(p => p.Code)
                .IsUnique(true);

        modelBuilder.Entity<Translation>()
            .HasOne(r => r.Language)
            .WithMany(r => r.Translations)
            .HasForeignKey(r => r.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Translation>()
            .HasIndex(t => new { t.LanguageId, t.Origin })
                .IsUnique();
    }
}
