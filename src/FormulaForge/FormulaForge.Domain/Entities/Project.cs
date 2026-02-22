using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Domain.Entities;

public class Project
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public required string Name { get; set; } = string.Empty;
    
    public Dictionary<string, ScalarValueNode.DecimalScalarValue> DecimalScalarValues { get; set; } = new();
    
    public Dictionary<string, ScalarValueNode.IntegerScalarValue> IntegerScalarValues { get; set; } = new();
    
    public Dictionary<string, ScalarValueNode.BooleanScalarValue> BooleanScalarValues { get; set; } = new();
    
    public Dictionary<string, TimeSeries<ScalarValueNode.DecimalScalarValue>> DecimalTimeSeriesValues { get; set; } = new();
    
    public Dictionary<string, TimeSeries<ScalarValueNode.IntegerScalarValue>> IntegerTimeSeriesValues { get; set; } = new();
    
    public Dictionary<string, TimeSeries<ScalarValueNode.BooleanScalarValue>> BooleanTimeSeriesValues { get; set; } = new();
}