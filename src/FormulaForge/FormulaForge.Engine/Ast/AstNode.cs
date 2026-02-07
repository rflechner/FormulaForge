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

public abstract record LiteralExpressionNode : AstNode
{
    public sealed record ConstantValueExpressionNode(ScalarValueNode Value) : LiteralExpressionNode;
    
    public sealed record VariableValueExpressionNode(string VariableName) : LiteralExpressionNode;
}

