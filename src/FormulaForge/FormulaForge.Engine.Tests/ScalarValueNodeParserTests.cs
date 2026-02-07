using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class ScalarValueNodeParserTests
{
    [Fact]
    public void IntegerValueParser_ShouldParseIntegerValues()
    {
        var text = "123";

        var result = MathsExpressionsParser.IntegerValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(123, result.Result.Value);
    }
    
    [Fact]
    public void IntegerValueParser_ShouldNotParseNonIntegerValues()
    {
        var text = "hello";
        
        var result = MathsExpressionsParser.IntegerValueParser.Parse(text);
        
        Assert.False(result.Success);
        Assert.Null(result.Result);
    }
    
    [Fact]
    public void DecimalValueParser_ShouldParseValues()
    {
        var text = "123.456";

        var result = MathsExpressionsParser.DecimalValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(123.456m, result.Result.Value);
    }
    
    [Fact]
    public void DecimalValueParser_ShouldNotParseValues()
    {
        var text = "123456";

        var result = MathsExpressionsParser.DecimalValueParser.Parse(text);
        
        Assert.False(result.Success);
        Assert.Null(result.Result);
    }
    
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void BooleanValueParser_ShouldParseValues(string text, bool expectedValue)
    {
        var result = MathsExpressionsParser.BooleanValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(expectedValue, result.Result.Value);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void ScalarValueParser_ShouldParseBooleanValues(string text, bool expectedValue)
    {
        var result = MathsExpressionsParser.ScalarValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new ScalarValueNode.BooleanScalarValue(expectedValue), result.Result);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("123456", 123456)]
    public void ScalarValueParser_ShouldParseIntegerValues(string text, int expectedValue)
    {
        var result = MathsExpressionsParser.ScalarValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(expectedValue), result.Result);
    }

    [Theory]
    [InlineData("2.456", 2.456)]
    [InlineData("4.6", 4.6)]
    [InlineData("123.456", 123.456)]
    public void ScalarValueParser_ShouldParseDecimalValues(string text, decimal expectedValue)
    {
        var result = MathsExpressionsParser.ScalarValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(new ScalarValueNode.DecimalScalarValue(expectedValue), result.Result);
    }
    
}