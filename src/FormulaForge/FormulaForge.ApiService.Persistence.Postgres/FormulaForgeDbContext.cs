using FormulaForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class FormulaForgeDbContext(DbContextOptions<FormulaForgeDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Convertisseur global pour forcer UTC sur DateTimeOffset
        var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
            v => v.UtcDateTime,
            v => v);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.GetProperties()
                .Where(p => p.ClrType == typeof(DateTimeOffset) || p.ClrType == typeof(DateTimeOffset?));
            foreach (var property in properties)
            {
                property.SetValueConverter(dateTimeOffsetConverter);
            }
        }

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Code).IsRequired(false);

            entity.HasMany(e => e.DecimalScalarValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.IntegerScalarValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.BooleanScalarValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.DecimalTimeSeriesValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.IntegerTimeSeriesValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.BooleanTimeSeriesValues)
                .WithOne()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DecimalTimeSeriesEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Entries)
                .WithOne()
                .HasForeignKey(e => e.TimeSeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IntegerTimeSeriesEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Entries)
                .WithOne()
                .HasForeignKey(e => e.TimeSeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BooleanTimeSeriesEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Entries)
                .WithOne()
                .HasForeignKey(e => e.TimeSeriesId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DecimalTimeSeriesEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Start).IsRequired();
            entity.Property(e => e.End).IsRequired();
        });

        modelBuilder.Entity<IntegerTimeSeriesEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Start).IsRequired();
            entity.Property(e => e.End).IsRequired();
        });

        modelBuilder.Entity<BooleanTimeSeriesEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Start).IsRequired();
            entity.Property(e => e.End).IsRequired();
        });
    }
}
