using EasyParsing;
using EasyParsing.Parsers.Maths;

namespace FormulaForge.Engine.DomainSpecificLanguage.Ast;

public abstract record AstNode(TextRange PositionRange);

public sealed record CommentNode(TextRange PositionRange, string Text) : AstNode(PositionRange);

public abstract record ValueExpressionNode(TextRange PositionRange) : AstNode(PositionRange);

public abstract record ScalarValueNode
{
    public sealed record IntegerScalarValue(int Value) : ScalarValueNode;
    
    public sealed record DecimalScalarValue(decimal Value) : ScalarValueNode;
    
    public sealed record BooleanScalarValue(bool Value) : ScalarValueNode;
}

public sealed record VariableName(TextRange PositionRange, string Name) : AstNode(PositionRange);

public abstract record LiteralExpressionNode(TextRange PositionRange) : ValueExpressionNode(PositionRange)
{
    public sealed record ConstantValueExpressionNode(TextRange PositionRange, ScalarValueNode Value) : LiteralExpressionNode(PositionRange);
    
    public sealed record VariableValueExpressionNode(TextRange PositionRange, VariableName VariableName) : LiteralExpressionNode(PositionRange);
}

public sealed record ComputedExpressionNode(TextRange PositionRange, BinaryOperationOperand<ValueExpressionNode> Expression) : ValueExpressionNode(PositionRange);

public record FunctionSignatureExpressionNode(TextRange PositionRange, string FunctionName, VariableName[] Parameters) : AstNode(PositionRange);

public record FunctionCallExpressionNode(TextRange PositionRange, string FunctionName, ValueExpressionNode[] Arguments) : ValueExpressionNode(PositionRange);

public abstract record StatementNode(TextRange PositionRange) : AstNode(PositionRange)
{
    public sealed record VariableAssignmentExpressionNode(TextRange PositionRange, VariableName Variable, ValueExpressionNode Value) : StatementNode(PositionRange);

    public sealed record FunctionDeclarationNode(TextRange PositionRange, string FunctionName, VariableName[] Parameters, ValueExpressionNode Body) : StatementNode(PositionRange);
}

public record InvalidLine(TextRange PositionRange, string Line) : AstNode(PositionRange);
