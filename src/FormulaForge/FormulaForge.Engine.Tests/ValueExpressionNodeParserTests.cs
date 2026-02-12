using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class ValueExpressionNodeParserTests
{
    [Theory]
    [InlineData("1 + 2", 1, "+", 2)]
    [InlineData("11 + 2", 11, "+", 2)]
    [InlineData("1000 / 2000", 1000, "/", 2000)]
    public void BinaryOperationExpressionParser_ShouldParseBinaryOperationsOfTwoIntegers(
        string text,
        int expectedLeftOperand, string rawOperator, int expectedRightOperand)
    {
        IParsingResult<OperationExpressionNode> result = ValueExpressionNodeParser.BinaryOperationExpressionParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        var leftOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(expectedLeftOperand)));
        var rightOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(expectedRightOperand)));
        var operation = new OperationExpressionNode.BinaryOperationExpressionNode(leftOperand, rightOperand, rawOperator);
        Assert.Equal((OperationExpressionNode)operation, result.Result);
    }
    
    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseAddOperationsOfTreeIntegers()
    {
        var operandParser =
            from _ in Parse.SkipSpaces()
            from n in FunctionCallNodeParser.ValueAccessParser
            from __ in Parse.SkipSpaces()
            select new BinaryOperationOperandValue<ValueExpressionNode>(n);

        var subOperationStart = Parse.SkipSpaces() << Parse.StringMatch("(") >> Parse.SkipSpaces();
        var subOperationEnd   = Parse.SkipSpaces() << Parse.StringMatch(")") >> Parse.SkipSpaces();
        
        var parser = MathsParser.ParseAlgebraicExpression(
            operandParser,
            subOperationStart, subOperationEnd,
            [
                new Operator<string>(OperatorKind.Infix, "+", 10),
                new Operator<string>(OperatorKind.Infix, "-", 10),
                new Operator<string>(OperatorKind.Infix, "*", 20),
                new Operator<string>(OperatorKind.Infix, "/", 20),
            ]);

        var text = "(1 + 25) * 589";
        
        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(result.Result);
        var rootOperation = (BinaryOperation<ValueExpressionNode>)result.Result;

        Assert.Equal("*", rootOperation.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(rootOperation.Left);
        var leftOperation = (BinaryOperation<ValueExpressionNode>)rootOperation.Left;
        Assert.Equal("+", leftOperation.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Left);
        var leftLeft = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Left;
        var value1 = new ScalarValueNode.IntegerScalarValue(1);
        Assert.Equal(value1, leftLeft.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Right);
        var leftRight = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Right;
        var value25 = new ScalarValueNode.IntegerScalarValue(25);
        Assert.Equal(value25, leftRight.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(rootOperation.Right);
        var right = (BinaryOperationOperandValue<ValueExpressionNode>)rootOperation.Right;
        var value589 = new ScalarValueNode.IntegerScalarValue(589);
        Assert.Equal(value589, right.Value);
    }
    
    
}