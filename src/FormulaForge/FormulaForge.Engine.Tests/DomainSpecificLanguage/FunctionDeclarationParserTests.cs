using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Tests.DomainSpecificLanguage;

public class FunctionDeclarationParserTests
{
    [Fact]
    public void InlineFunctionWithOneParam_ShouldParseFunctionDeclaration()
    {
        var code = "add(a, b) = a + b";
        var parser = StatementsParser.FunctionDeclarationParser;

        var result = parser.Parse(code);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("add", result.Result.FunctionName);
        Assert.Equal(2, result.Result.Parameters.Length);
        
        Assert.Equal("a", result.Result.Parameters[0].Name);
        Assert.Equal("b", result.Result.Parameters[1].Name);

        Assert.NotNull(result.Result.Body);

        Assert.IsType<ComputedExpressionNode>(result.Result.Body);
        var computedExpr = (ComputedExpressionNode)result.Result.Body;

        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>>(computedExpr.Expression);
        var binaryOp = (EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>)computedExpr.Expression;

        Assert.Equal("+", binaryOp.Operator.Text);

        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>>(binaryOp.Left);
        var leftOperand = ((EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>)binaryOp.Left).Value;
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(leftOperand);
        Assert.Equal("a", ((LiteralExpressionNode.VariableValueExpressionNode)leftOperand).VariableName.Name);

        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>>(binaryOp.Right);
        var rightOperand = ((EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>)binaryOp.Right).Value;
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(rightOperand);
        Assert.Equal("b", ((LiteralExpressionNode.VariableValueExpressionNode)rightOperand).VariableName.Name);
    }
}