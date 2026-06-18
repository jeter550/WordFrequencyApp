namespace WordFrequency.Infrastructure.Data;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WordFrequencyResult> Analyses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WordFrequencyResult>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.OriginalText)
                .IsRequired()
                .HasMaxLength(2048);

            entity.Property(e => e.TotalWords)
                .IsRequired();

            entity.Property(e => e.UniqueWords)
                .IsRequired();

            entity.OwnsMany(e => e.Results, b =>
            {
                b.ToJson();
                b.Property(r => r.Word).HasMaxLength(256);
                b.Property(r => r.Count);
                b.Property(r => r.Rank);
            });
        });
    }
}
