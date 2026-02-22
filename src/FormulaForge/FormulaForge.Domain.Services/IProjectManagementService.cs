using FormulaForge.ApiService.Persistence;
using FormulaForge.Domain.Entities;

namespace FormulaForge.Domain.Services;

public interface IProjectManagementService
{
    Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Project> GetProjectsAsync(CancellationToken cancellationToken = default);
    Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
}

public class ProjectManagementService(IProjectRepository projectRepository) : IProjectManagementService
{
    public Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return projectRepository.GetProjectAsync(projectId, cancellationToken);
    }

    public IAsyncEnumerable<Project> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        return projectRepository.GetProjectsAsync(cancellationToken);
    }

    public Task CreateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        return projectRepository.AddProjectAsync(project, cancellationToken);
    }

    public Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        return projectRepository.UpdateProjectAsync(project, cancellationToken);
    }

    public Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        return projectRepository.DeleteProjectAsync(project.Id, cancellationToken);
    }
}