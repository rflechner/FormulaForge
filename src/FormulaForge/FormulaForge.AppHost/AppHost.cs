var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume("formulaforgestorage")
    .WithPgAdmin(p => p.WithHostPort(5050))
    ;

var encryptionKey = builder.AddParameter("encryption-key", secret: true);
var pocketIdClientId = builder.AddParameter("pocket-id-client-id", secret: true);
var pocketIdClientSecret = builder.AddParameter("pocket-id-client-secret", secret: true);

var smtp4dev = builder
    .AddContainer("smtp4dev", "rnwood/smtp4dev", "latest")
    .WithHttpEndpoint(targetPort: 80, name: "http", port: 5052)
    .WithEndpoint(targetPort: 25, name: "smtp", port: 2525)
    .WithExternalHttpEndpoints()
    ;

var pocketId = builder
    .AddContainer("pocket-id", "ghcr.io/pocket-id/pocket-id", "v2")
    .WithVolume("pocket-id-data", "/app/data")
    .WithHttpEndpoint(targetPort: 1411, name: "http", port: 5051)
    .WithExternalHttpEndpoints()
    .WithEnvironment("ENCRYPTION_KEY", encryptionKey)
    .WithEnvironment("UI_CONFIG_DISABLED", "true")
    .WithEnvironment("SMTP_HOST", smtp4dev.GetEndpoint("smtp"))
    .WithEnvironment("SMTP_PORT", smtp4dev.GetEndpoint("smtp").Property(EndpointProperty.TargetPort))
    .WithEnvironment("SMTP_FROM", "pocket-id@formulaforge.local")
    .WithEnvironment("SMTP_SECURE", "false")
    .WithEnvironment("APP_URL", "http://localhost:5051")
    ;

var db = postgres.AddDatabase("formulaforge", databaseName: "formulaforge");

var apiService = builder
    .AddProject<Projects.FormulaForge_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(smtp4dev)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.FormulaForge_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WithReference(db)
    .WithEnvironment("Authentication__PocketId__Authority", pocketId.GetEndpoint("http"))
    .WithEnvironment("Authentication__PocketId__ClientId", pocketIdClientId)
    .WithEnvironment("Authentication__PocketId__ClientSecret", pocketIdClientSecret)
    .WaitFor(db)
    .WaitFor(apiService)
    .WaitFor(pocketId)
    .WaitFor(smtp4dev);

builder.Build().Run();
