using EasyParsing;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class MathsExpressionsParserTests
{
    [Theory]
    [InlineData("1 + 2", 1, "+", 2)]
    [InlineData("11 + 2", 11, "+", 2)]
    [InlineData("1000 / 2000", 1000, "/", 2000)]
    public void BinaryOperationExpressionParser_ShouldParseBinaryOperationsOfTwoIntegers(
        string text,
        int expectedLeftOperand, string rawOperator, int expectedRightOperand)
    {
        IParsingResult<OperationExpressionNode.BinaryOperationExpressionNode[]> result = MathsExpressionsParser.BinaryOperationExpressionParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        var leftOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(expectedLeftOperand)));
        var rightOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(expectedRightOperand)));
        var operation = new OperationExpressionNode.BinaryOperationExpressionNode(leftOperand, rightOperand, rawOperator);
        Assert.Equal([operation], result.Result);
    }
    
    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseAddOperationsOfTreeIntegers()
    {
        IParsingResult<OperationExpressionNode.BinaryOperationExpressionNode[]> result = MathsExpressionsParser.BinaryOperationExpressionParser.Parse("1 + 20 + 300");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        var leftOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1)));
        var rightOperand = new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(20)));
        var operation1 = new OperationExpressionNode.BinaryOperationExpressionNode(leftOperand, rightOperand, "+");
        
        var operation2 = new OperationExpressionNode.BinaryOperationExpressionNode(
            operation1, 
            new OperationExpressionNode.ReadExpressionNode(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(300))), 
            "+");
        
        Assert.Equal([operation2], result.Result);
    }
    
    
}