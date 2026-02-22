using FormulaForge.Domain.Entities;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class PostresProjectRepository : IProjectRepository
{
    public Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Project> GetProjectsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}