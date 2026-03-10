var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume("formulaforgestorage")
    .WithPgAdmin(p => p.WithHostPort(5050))
    ;

var encryptionKey = builder.AddParameter("encryption-key", secret: true);

var pocketId = builder
    .AddContainer("pocket-id", "ghcr.io/pocket-id/pocket-id", "v2")
    .WithBindMount("./pocket-id-data", "/app/data")
    .WithHttpEndpoint(targetPort: 1411, name: "http")
    .WithExternalHttpEndpoints()
    .WithEnvironment("ENCRYPTION_KEY", encryptionKey)
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