using EasyParsing.Parsers.Maths;

namespace FormulaForge.Engine.DomainSpecificLanguage.Ast;

public abstract record AstNode;

public sealed record CommentNode(string Text) : AstNode;

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

public record FunctionSignatureExpressionNode(string FunctionName, VariableName[] Parameters) : AstNode;

public record FunctionCallExpressionNode(string FunctionName, ValueExpressionNode[] Arguments) : ValueExpressionNode;

public abstract record StatementNode : AstNode
{
    public sealed record VariableAssignmentExpressionNode(VariableName Variable, ValueExpressionNode Value) : StatementNode;

    public sealed record FunctionDeclarationNode(string FunctionName, VariableName[] Parameters, ValueExpressionNode Body) : StatementNode;
}
