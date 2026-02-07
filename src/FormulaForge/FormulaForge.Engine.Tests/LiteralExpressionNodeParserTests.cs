using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class LiteralExpressionNodeParserTests
{
    [Theory]
    [InlineData("t")]
    [InlineData("test")]
    [InlineData("testVariable")]
    [InlineData("test1Variable")]
    [InlineData("test1Variable223")]
    [InlineData("testVariable2")]
    [InlineData("test_variable2")]
    [InlineData("_test_variable2")]
    [InlineData("_test2")]
    [InlineData("_test")]
    public void VariableValueExpressionParser_ShouldParseDecimalValues(string variableName)
    {
        var result = MathsExpressionsParser.VariableValueExpressionParser.Parse(variableName);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new LiteralExpressionNode.VariableValueExpressionNode(variableName), result.Result);
    }
    
    [Theory]
    [InlineData("t")]
    [InlineData("testVariable")]
    [InlineData("test1Variable")]
    [InlineData("test1Variable223")]
    [InlineData("_test_variable2")]
    public void LiteralExpressionNodeParser_ShouldParseVariableName(string variableName)
    {
        var result = MathsExpressionsParser.LiteralExpressionNodeParser.Parse(variableName);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new LiteralExpressionNode.VariableValueExpressionNode(variableName), result.Result);
    }
    
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void LiteralExpressionNodeParser_ShouldParseBooleanValues(string text, bool expectedValue)
    {
        var result = MathsExpressionsParser.LiteralExpressionNodeParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.BooleanScalarValue(expectedValue)), result.Result);
    }
    
    [Theory]
    [InlineData("1.2", 1.2)]
    [InlineData("123.987", 123.987)]
    public void LiteralExpressionNodeParser_ShouldParseDecimalValues(string text, decimal expectedValue)
    {
        var result = MathsExpressionsParser.LiteralExpressionNodeParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.DecimalScalarValue(expectedValue)), result.Result);
    }
    
    [Theory]
    [InlineData("1", 1)]
    [InlineData("123", 123)]
    public void LiteralExpressionNodeParser_ShouldParseIntegerValues(string text, int expectedValue)
    {
        var result = MathsExpressionsParser.LiteralExpressionNodeParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(expectedValue)), result.Result);
    }
    
}