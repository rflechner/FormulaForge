using FormulaForge.Domain.Entities;

namespace FormulaForge.Domain.Services;

public interface IProjectManagementService
{
    Task<Project?> GetProjectAsync(Guid projectId, string userId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Project> GetProjectsAsync(string userId, CancellationToken cancellationToken = default);
    Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
}