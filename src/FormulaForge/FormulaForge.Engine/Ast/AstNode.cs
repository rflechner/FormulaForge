namespace FormulaForge.Engine.Ast;

public abstract record AstNode
{
    
}

public abstract record ScalarValueNode : AstNode
{
    public sealed record IntegerScalarValue(int Value);
    
    public sealed record DecimalScalarValue(decimal Value);
    
    public sealed record BooleanScalarValue(bool Value);
}
