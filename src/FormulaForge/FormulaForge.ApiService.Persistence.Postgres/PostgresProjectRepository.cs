using FormulaForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class PostgresProjectRepository(FormulaForgeDbContext dbContext) : IProjectRepository
{
    public async Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
    }

    public IAsyncEnumerable<Project> GetProjectsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return dbContext.Projects
            .AsNoTracking()
            .AsAsyncEnumerable();
    }
}