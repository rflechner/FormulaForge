using FormulaForge.ApiService.Persistence;
using FormulaForge.Domain.Entities;

namespace FormulaForge.Domain.Services.Impl;

public class ProjectManagementService(IProjectRepository projectRepository) : IProjectManagementService
{
    public Task<Project?> GetProjectAsync(Guid projectId, string userId, CancellationToken cancellationToken = default)
    {
        return projectRepository.GetProjectAsync(projectId, userId, cancellationToken);
    }

    public IAsyncEnumerable<Project> GetProjectsAsync(string userId, CancellationToken cancellationToken = default)
    {
        return projectRepository.GetProjectsAsync(userId, cancellationToken);
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