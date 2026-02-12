using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class ValueExpressionNodeParserTests
{
    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseAddOperationsOfTreeIntegers()
    {
        var parser = ValueExpressionNodeParser.OperationsParser;

        var text = "(1 + 25) * 589";
        
        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(result.Result.Expression);
        var rootOperation = (BinaryOperation<ValueExpressionNode>)result.Result.Expression;

        Assert.Equal("*", rootOperation.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(rootOperation.Left);
        var leftOperation = (BinaryOperation<ValueExpressionNode>)rootOperation.Left;
        Assert.Equal("+", leftOperation.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Left);
        var leftLeft = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Left;
        var value1 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1));
        Assert.Equal(value1, leftLeft.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Right);
        var leftRight = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Right;
        var value25 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(25));
        Assert.Equal(value25, leftRight.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(rootOperation.Right);
        var right = (BinaryOperationOperandValue<ValueExpressionNode>)rootOperation.Right;
        var value589 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(589));
        Assert.Equal(value589, right.Value);
    }
    
}