using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Domain.Entities;

public class Project
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public required string UserId { get; set; } = string.Empty;
    
    public required string Name { get; set; } = string.Empty;
    
    public string Code { get; set; } = string.Empty;
    
    public List<DecimalScalarValueEntity> DecimalScalarValues { get; set; } = new();
    
    public List<IntegerScalarValueEntity> IntegerScalarValues { get; set; } = new();
    
    public List<BooleanScalarValueEntity> BooleanScalarValues { get; set; } = new();
    
    public List<DecimalTimeSeriesEntity> DecimalTimeSeriesValues { get; set; } = new();
    
    public List<IntegerTimeSeriesEntity> IntegerTimeSeriesValues { get; set; } = new();
    
    public List<BooleanTimeSeriesEntity> BooleanTimeSeriesValues { get; set; } = new();
}

public class DecimalScalarValueEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public decimal Value { get; set; }
    public Guid ProjectId { get; set; }
}

public class IntegerScalarValueEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public int Value { get; set; }
    public Guid ProjectId { get; set; }
}

public class BooleanScalarValueEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public bool Value { get; set; }
    public Guid ProjectId { get; set; }
}

public class DecimalTimeSeriesEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public List<DecimalTimeSeriesEntry> Entries { get; set; } = new();
    public Guid ProjectId { get; set; }
}

public class DecimalTimeSeriesEntry
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public decimal Value { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public Guid TimeSeriesId { get; set; }
}

public class IntegerTimeSeriesEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public List<IntegerTimeSeriesEntry> Entries { get; set; } = new();
    public Guid ProjectId { get; set; }
}

public class IntegerTimeSeriesEntry
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public int Value { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public Guid TimeSeriesId { get; set; }
}

public class BooleanTimeSeriesEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Key { get; set; }
    public List<BooleanTimeSeriesEntry> Entries { get; set; } = new();
    public Guid ProjectId { get; set; }
}

public class BooleanTimeSeriesEntry
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public bool Value { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public Guid TimeSeriesId { get; set; }
}