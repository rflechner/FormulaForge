namespace FormulaForge.Engine.Tests;

public class FunctionCallNodeParserTests
{
    [Fact]
    public void FunctionCallWithoutParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithoutParameters;

        var result = parser.Parse("testFunction()");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
    }
}