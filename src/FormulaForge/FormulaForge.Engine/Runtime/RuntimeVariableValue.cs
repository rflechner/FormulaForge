using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Runtime;

public abstract record RuntimeVariableValue
{
    public record RuntimeScalarValue(ScalarValueNode Value) : RuntimeVariableValue;
    
    public record RuntimeTimeSeriesValue(TimeSeries<ScalarValueNode> Value) : RuntimeVariableValue;
}