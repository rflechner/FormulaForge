using FormulaForge.Domain.Entities;

namespace FormulaForge.ApiService.Persistence;

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(Guid projectId, string userId, CancellationToken cancellationToken = default);
    
    IAsyncEnumerable<Project> GetProjectsAsync(string userId, CancellationToken cancellationToken = default);

    Task AddProjectAsync(Project project, CancellationToken cancellationToken = default);
    
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
    
    Task DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
