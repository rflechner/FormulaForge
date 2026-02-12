using FormulaForge.Engine.Ast;

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
        Assert.Empty(result.Result.Arguments);
    }

    [Fact]
    public void FunctionCallWithOneIntParameter_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithParameters;

        var result = parser.Parse("testFunction(12)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Equal(1, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
    }

    [Fact]
    public void FunctionCallWithTwoIntParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithParameters;

        var result = parser.Parse("testFunction(12, 21)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Equal(2, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.IntegerScalarValue(21));
    }

    [Fact]
    public void FunctionCallWithTreeIntParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithParameters;

        var result = parser.Parse("testFunction(12, 1234, 21)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Equal(3, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.IntegerScalarValue(1234));
        Assert.Equal(result.Result.Arguments[2], new ScalarValueNode.IntegerScalarValue(21));
    }

    [Fact]
    public void FunctionCallWithFourIntParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithParameters;

        var result = parser.Parse("testFunction(12, 1234, 9, 21)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Equal(4, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.IntegerScalarValue(1234));
        Assert.Equal(result.Result.Arguments[2], new ScalarValueNode.IntegerScalarValue(9));
        Assert.Equal(result.Result.Arguments[3], new ScalarValueNode.IntegerScalarValue(21));
    }

    [Fact]
    public void FunctionCallWithTreeIntAndOneDecimalParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCallWithParameters;

        var result = parser.Parse(@"test_function(12, 99.2324, 9, 22341)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("test_function", result.Result.FunctionName);
        Assert.Equal(4, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.DecimalScalarValue(99.2324m));
        Assert.Equal(result.Result.Arguments[2], new ScalarValueNode.IntegerScalarValue(9));
        Assert.Equal(result.Result.Arguments[3], new ScalarValueNode.IntegerScalarValue(22341));
    }
    
}