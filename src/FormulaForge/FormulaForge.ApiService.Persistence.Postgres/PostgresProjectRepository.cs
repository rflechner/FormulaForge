using FormulaForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaForge.ApiService.Persistence.Postgres;

public class PostgresProjectRepository(FormulaForgeDbContext dbContext) : IProjectRepository
{
    public async Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects
            .Include(p => p.DecimalScalarValues)
            .Include(p => p.IntegerScalarValues)
            .Include(p => p.BooleanScalarValues)
            .Include(p => p.DecimalTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
            .Include(p => p.IntegerTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
            .Include(p => p.BooleanTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
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
        var existingProject = await dbContext.Projects
            .Include(p => p.DecimalScalarValues)
            .Include(p => p.IntegerScalarValues)
            .Include(p => p.BooleanScalarValues)
            .Include(p => p.DecimalTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
            .Include(p => p.IntegerTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
            .Include(p => p.BooleanTimeSeriesValues)
                .ThenInclude(ts => ts.Entries)
            .FirstOrDefaultAsync(p => p.Id == project.Id, cancellationToken);

        if (existingProject == null)
        {
            throw new KeyNotFoundException($"Project with ID {project.Id} not found.");
        }

        // Mettre à jour les propriétés de base
        existingProject.Name = project.Name;
        existingProject.Code = project.Code;

        project.DecimalTimeSeriesValues.ForEach(sv => sv.ProjectId = project.Id);
        project.IntegerTimeSeriesValues.ForEach(sv => sv.ProjectId = project.Id);
        project.BooleanTimeSeriesValues.ForEach(sv => sv.ProjectId = project.Id);
        
        project.DecimalScalarValues.ForEach(sv => sv.ProjectId = project.Id);
        project.IntegerScalarValues.ForEach(sv => sv.ProjectId = project.Id);
        project.BooleanScalarValues.ForEach(sv => sv.ProjectId = project.Id);

        
        // // Remplacer les collections scalaires
        existingProject.DecimalScalarValues.Clear();
        existingProject.DecimalScalarValues.AddRange(project.DecimalScalarValues);
        
        existingProject.IntegerScalarValues.Clear();
        existingProject.IntegerScalarValues.AddRange(project.IntegerScalarValues);
        
        existingProject.BooleanScalarValues.Clear();
        existingProject.BooleanScalarValues.AddRange(project.BooleanScalarValues);
        
        // // Remplacer les collections de séries temporelles
        
        existingProject.DecimalTimeSeriesValues.Clear();
        existingProject.DecimalTimeSeriesValues.AddRange(project.DecimalTimeSeriesValues);
        
        existingProject.IntegerTimeSeriesValues.Clear();
        existingProject.IntegerTimeSeriesValues.AddRange(project.IntegerTimeSeriesValues);
        
        existingProject.BooleanTimeSeriesValues.Clear();
        existingProject.BooleanTimeSeriesValues.AddRange(project.BooleanTimeSeriesValues);

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