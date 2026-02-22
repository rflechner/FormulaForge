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

    public IAsyncEnumerable<Project> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Projects
            .AsNoTracking()
            .AsAsyncEnumerable();
    }

    public async Task AddProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await dbContext.Projects.FindAsync([projectId], cancellationToken);
        if (project != null)
        {
            dbContext.Projects.Remove(project);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}