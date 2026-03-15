using FormulaForge.ApiService.Dto;
using FormulaForge.ApiService.Persistence;
using FormulaForge.ApiService.Persistence.Postgres;
using FormulaForge.Domain.Entities;
using FormulaForge.Engine.Time;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<FormulaForgeDbContext>("formulaforge");
builder.Services.AddScoped<IProjectRepository, PostgresProjectRepository>();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "FormulaForge API";
        document.Info.Version = "v1";
        document.Info.Description = "API pour la gestion des projets FormulaForge.";
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Appliquer les migrations au démarrage
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FormulaForgeDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

app.MapGet("/projects", (string userId, IProjectRepository repository) =>
{
    return repository.GetProjectsAsync(userId);
})
.WithName("GetProjects")
.WithSummary("Récupère la liste de tous les projets")
.WithDescription("Renvoie une liste asynchrone de tous les projets enregistrés.")
.Produces<IAsyncEnumerable<Project>>(StatusCodes.Status200OK);

app.MapGet("/projects/{id:guid}", async (Guid id, string userId, IProjectRepository repository) =>
{
    var project = await repository.GetProjectAsync(id, userId);
    return project is not null ? Results.Ok(project) : Results.NotFound();
})
.WithName("GetProjectById")
.WithSummary("Récupère un projet par son identifiant")
.WithDescription("Renvoie les détails d'un projet spécifique basé sur son GUID.")
.Produces<Project>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/projects", async (Project project, IProjectRepository repository) =>
{
    await repository.AddProjectAsync(project);
    return Results.Created($"/projects/{project.Id}", project);
})
.WithName("CreateProject")
.WithSummary("Crée un nouveau projet")
.WithDescription("Ajoute un nouveau projet à la base de données.")
.Produces<Project>(StatusCodes.Status201Created);

app.MapPut("/projects/{id:guid}", async (Guid id, Project project, IProjectRepository repository) =>
{
    if (id != project.Id)
    {
        return Results.BadRequest();
    }

    await repository.UpdateProjectAsync(project);
    return Results.NoContent();
})
.WithName("UpdateProject")
.WithSummary("Met à jour un projet existant")
.WithDescription("Modifie les informations d'un projet identifié par son GUID.")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status400BadRequest);

app.MapDelete("/projects/{id:guid}", async (Guid id, IProjectRepository repository) =>
{
    await repository.DeleteProjectAsync(id);
    return Results.NoContent();
})
.WithName("DeleteProject")
.WithSummary("Delete a project")
.WithDescription("Supprime définitivement un projet de la base de données par son GUID.")
.Produces(StatusCodes.Status204NoContent);


var samples = app.MapGroup("samples");
samples.MapGet("expenses", async (HttpContext context) =>
    {
        var random = new Random();
        var start = DateTimeOffset.Now.AddYears(-1);
        var end = DateTimeOffset.Now.AddYears(1);

        var result = new List<TimeSeriesDecimalValueDto>();
        
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            result.Add(new TimeSeriesDecimalValueDto(Period.OneDay(DateOnly.FromDateTime(date.DateTime)), (decimal)random.NextDouble()));
        }
        
        return Results.Ok(result);
    })
    .WithName("GetExpenses")
    .WithSummary("Generates sample of expenses dataset")
    .WithDescription("Generates sample of expenses dataset.")
    .Produces<TimeSeriesDecimalValueDto[]>();


app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}