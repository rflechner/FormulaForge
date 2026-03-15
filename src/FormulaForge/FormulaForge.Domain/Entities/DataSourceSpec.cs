namespace FormulaForge.Domain.Entities;

public abstract record DataSourceSpec(string Name);

public sealed record RestApiDataSourceSpec(string Name, Uri Endpoint) : DataSourceSpec(Name); 
