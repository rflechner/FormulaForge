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
    public void DecimalValueParser_ShouldParseIntegerValues()
    {
        var text = "123.456";

        var result = MathsExpressionsParser.DecimalValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(123.456m, result.Result.Value);
    }
    
    [Fact]
    public void DecimalValueParser_ShouldNotParseIntegerValues()
    {
        var text = "123456";

        var result = MathsExpressionsParser.DecimalValueParser.Parse(text);
        
        Assert.False(result.Success);
        Assert.Null(result.Result);
    }
    
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void BooleanValueParser_ShouldParseIntegerValues(string text, bool expectedValue)
    {
        var result = MathsExpressionsParser.BooleanValueParser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(expectedValue, result.Result.Value);
    }
    
}