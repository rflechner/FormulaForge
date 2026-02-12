using EasyParsing.Parsers.Maths;

namespace FormulaForge.Engine.Ast;

public abstract record AstNode
{
    
}

public abstract record ValueExpressionNode : AstNode;

public abstract record ScalarValueNode
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

public sealed record ComputedExpressionNode(BinaryOperationOperand<ValueExpressionNode> Expression) : ValueExpressionNode;

public abstract record OperationExpressionNode : ValueExpressionNode
{
    public sealed record ReadExpressionNode(ValueExpressionNode Address) : OperationExpressionNode;
    
    public sealed record ComputedExpressionNode(OperationExpressionNode Expression) : OperationExpressionNode;
}

public abstract record AssignmentExpressionNode : AstNode
{
    public sealed record VariableAssignmentExpressionNode(LiteralExpressionNode.VariableValueExpressionNode Variable, OperationExpressionNode Value) : AssignmentExpressionNode;
}


public record FunctionCallExpressionNode(string FunctionName, ValueExpressionNode[] Arguments) : OperationExpressionNode;

