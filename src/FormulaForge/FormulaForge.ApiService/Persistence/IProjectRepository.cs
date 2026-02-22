using FormulaForge.Domain.Entities;

namespace FormulaForge.ApiService.Persistence;

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    
    IAsyncEnumerable<Project> GetProjectsAsync(Guid projectId, CancellationToken cancellationToken = default);
}
