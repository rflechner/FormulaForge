using EasyParsing.Parsers.Maths;

namespace FormulaForge.Engine.Ast;

public abstract record AstNode;

public abstract record ValueExpressionNode : AstNode;

public abstract record ScalarValueNode
{
    public sealed record IntegerScalarValue(int Value) : ScalarValueNode;
    
    public sealed record DecimalScalarValue(decimal Value) : ScalarValueNode;
    
    public sealed record BooleanScalarValue(bool Value) : ScalarValueNode;
}

public sealed record VariableName(string Name) : AstNode;

public abstract record LiteralExpressionNode : ValueExpressionNode
{
    public sealed record ConstantValueExpressionNode(ScalarValueNode Value) : LiteralExpressionNode;
    
    public sealed record VariableValueExpressionNode(VariableName VariableName) : LiteralExpressionNode;
}

public sealed record ComputedExpressionNode(BinaryOperationOperand<ValueExpressionNode> Expression) : ValueExpressionNode;

public record FunctionCallExpressionNode(string FunctionName, ValueExpressionNode[] Arguments) : ValueExpressionNode;

public abstract record StatementNode : AstNode
{
    public sealed record ExpressionStatementNode(ValueExpressionNode Expression) : StatementNode;
    
    public abstract record AssignmentExpressionNode : StatementNode
    {
        public sealed record VariableAssignmentExpressionNode(VariableName Variable, ValueExpressionNode Value) : AssignmentExpressionNode;
    }
}
