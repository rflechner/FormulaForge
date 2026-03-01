using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class FormulaForgeDbContextFactory : IDesignTimeDbContextFactory<FormulaForgeDbContext>
{
    public FormulaForgeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FormulaForgeDbContext>();
        
        // On cherche le fichier appsettings.json dans le projet ApiService
        // Ou on utilise une chaîne de connexion par défaut pour le design-time
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FormulaForge.ApiService"))
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("formulaforge") 
                               ?? "Host=localhost;Database=formulaforge;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);

        return new FormulaForgeDbContext(optionsBuilder.Options);
    }
}
