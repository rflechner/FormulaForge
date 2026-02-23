var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume("formulaforgestorage")
    .WithPgAdmin(p => p.WithHostPort(5050))
    ;

var db = postgres.AddDatabase("formulaforge", databaseName: "formulaforge");

var apiService = builder
    .AddProject<Projects.FormulaForge_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.FormulaForge_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(apiService);

builder.Build().Run();