namespace FormulaForge.Engine.Ast;

public abstract record AstNode
{
    
}

public abstract record ValueExpressionNode : AstNode;

public abstract record ScalarValueNode : ValueExpressionNode
{
    public sealed record IntegerScalarValue(int Value) : ScalarValueNode;
    
    public sealed record DecimalScalarValue(decimal Value) : ScalarValueNode;
    
    public sealed record BooleanScalarValue(bool Value) : ScalarValueNode;
}

public abstract record LiteralExpressionNode : ValueExpressionNode
{
    public sealed record ConstantValueExpressionNode(ScalarValueNode Value) : LiteralExpressionNode;
    
    public sealed record VariableValueExpressionNode(string VariableName) : LiteralExpressionNode;
}

public abstract record OperationExpressionNode : ValueExpressionNode
{
    public sealed record BinaryOperationExpressionNode(OperationExpressionNode LeftOperand, OperationExpressionNode RightOperand, string Operator) : OperationExpressionNode;
    
    public sealed record UnaryOperationExpressionNode(OperationExpressionNode Operand, string Operator) : OperationExpressionNode;
    
    public sealed record ReadExpressionNode(ValueExpressionNode Address) : OperationExpressionNode;
}

public abstract record AssignmentExpressionNode : AstNode
{
    public sealed record VariableAssignmentExpressionNode(LiteralExpressionNode.VariableValueExpressionNode Variable, OperationExpressionNode Value) : AssignmentExpressionNode;
}


public record FunctionCallExpressionNode(string FunctionName, ValueExpressionNode[] Arguments) : OperationExpressionNode;

