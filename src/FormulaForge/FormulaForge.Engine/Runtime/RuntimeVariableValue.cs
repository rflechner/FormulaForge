using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Runtime;

public abstract record RuntimeVariableValue
{
    public record RuntimeScalarValue(ScalarValueNode Value) : RuntimeVariableValue;
    
    public record RuntimeComplexValue(object Value) : RuntimeVariableValue;
}