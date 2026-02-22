using FormulaForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class FormulaForgeDbContext(DbContextOptions<FormulaForgeDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            // Pour simplifier l'implémentation initiale, nous allons stocker les dictionnaires complexes en JSON
            // Dans une implémentation réelle plus robuste, on pourrait utiliser des tables séparées ou des colonnes jsonb de PostgreSQL.
            entity.Property(e => e.DecimalScalarValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.DecimalScalarValue>>(v, (JsonSerializerOptions?)null) ?? new());

            entity.Property(e => e.IntegerScalarValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.IntegerScalarValue>>(v, (JsonSerializerOptions?)null) ?? new());

            entity.Property(e => e.BooleanScalarValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.BooleanScalarValue>>(v, (JsonSerializerOptions?)null) ?? new());

            entity.Property(e => e.DecimalTimeSeriesValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.Time.TimeSeries<FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.DecimalScalarValue>>>(v, (JsonSerializerOptions?)null) ?? new());

            entity.Property(e => e.IntegerTimeSeriesValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.Time.TimeSeries<FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.IntegerScalarValue>>>(v, (JsonSerializerOptions?)null) ?? new());

            entity.Property(e => e.BooleanTimeSeriesValues)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, FormulaForge.Engine.Time.TimeSeries<FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.BooleanScalarValue>>>(v, (JsonSerializerOptions?)null) ?? new());
        });
    }
}
