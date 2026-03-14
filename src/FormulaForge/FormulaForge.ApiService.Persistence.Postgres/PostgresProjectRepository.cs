using FormulaForge.Domain.Entities;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
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
            .FirstOrDefaultAsync(p => p.Id == project.Id, cancellationToken);

        if (existingProject == null)
        {
            throw new KeyNotFoundException($"Project with ID {project.Id} not found.");
        }

        existingProject.Name = project.Name;
        existingProject.Code = project.Code;

        await RemoveExistingScalarAndTimeSeriesValuesAsync(project, cancellationToken);

        await AddScalarAndTimeSeriesValuesAsync(project, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task AddScalarAndTimeSeriesValuesAsync(Project project, CancellationToken cancellationToken)
    {
        foreach (var scalar in project.DecimalScalarValues)
        {
            scalar.ProjectId = project.Id;
            await dbContext.DecimalScalarValues.AddAsync(scalar, cancellationToken);
        }

        foreach (var scalar in project.IntegerScalarValues)
        {
            scalar.ProjectId = project.Id;
            await dbContext.IntegerScalarValues.AddAsync(scalar, cancellationToken);
        }

        foreach (var scalar in project.BooleanScalarValues)
        {
            scalar.ProjectId = project.Id;
            await dbContext.BooleanScalarValues.AddAsync(scalar, cancellationToken);
        }

        foreach (var timeSeries in project.DecimalTimeSeriesValues)
        {
            timeSeries.ProjectId = project.Id;
            await dbContext.DecimalTimeSeriesValues.AddAsync(timeSeries, cancellationToken);
        }

        foreach (var timeSeries in project.IntegerTimeSeriesValues)
        {
            timeSeries.ProjectId = project.Id;
            await dbContext.IntegerTimeSeriesValues.AddAsync(timeSeries, cancellationToken);
        }

        foreach (var timeSeries in project.BooleanTimeSeriesValues)
        {
            timeSeries.ProjectId = project.Id;
            await dbContext.BooleanTimeSeriesValues.AddAsync(timeSeries, cancellationToken);
        }
    }

    private async Task RemoveExistingScalarAndTimeSeriesValuesAsync(Project project, CancellationToken cancellationToken)
    {
        var existingDecimalScalars = await dbContext.DecimalScalarValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.DecimalScalarValues.RemoveRange(existingDecimalScalars);

        var existingIntegerScalars = await dbContext.IntegerScalarValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.IntegerScalarValues.RemoveRange(existingIntegerScalars);

        var existingBooleanScalars = await dbContext.BooleanScalarValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.BooleanScalarValues.RemoveRange(existingBooleanScalars);

        var existingDecimalTimeSeries = await dbContext.DecimalTimeSeriesValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.DecimalTimeSeriesValues.RemoveRange(existingDecimalTimeSeries);

        var existingIntegerTimeSeries = await dbContext.IntegerTimeSeriesValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.IntegerTimeSeriesValues.RemoveRange(existingIntegerTimeSeries);

        var existingBooleanTimeSeries = await dbContext.BooleanTimeSeriesValues
            .Where(v => v.ProjectId == project.Id)
            .ToListAsync(cancellationToken);
        dbContext.BooleanTimeSeriesValues.RemoveRange(existingBooleanTimeSeries);
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
