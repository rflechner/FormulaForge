namespace FormulaForge.Engine.Ast;

public abstract record AstNode
{
    
}

public abstract record ScalarValueNode : AstNode
{
    public sealed record IntegerScalarValue(int Value) : ScalarValueNode;
    
    public sealed record DecimalScalarValue(decimal Value) : ScalarValueNode;
    
    public sealed record BooleanScalarValue(bool Value) : ScalarValueNode;
}
