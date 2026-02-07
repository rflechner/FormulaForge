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

public abstract record OperationExpressionNode : AstNode
{
    public sealed record BinaryOperationExpressionNode(OperationExpressionNode LeftOperand, OperationExpressionNode RightOperand, string Operator) : OperationExpressionNode;
    
    public sealed record UnaryOperationExpressionNode(OperationExpressionNode Operand, string Operator) : OperationExpressionNode;
    
    public sealed record ReadExpressionNode(AstNode Address) : OperationExpressionNode;
}

public abstract record AssignmentExpressionNode : AstNode
{
    public sealed record VariableAssignmentExpressionNode(LiteralExpressionNode.VariableValueExpressionNode Variable, OperationExpressionNode Value) : AssignmentExpressionNode;
}
