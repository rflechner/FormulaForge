using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class FunctionCallNodeParserTests
{
    [Fact]
    public void FunctionCallWithoutParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

        var result = parser.Parse("testFunction()");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Empty(result.Result.Arguments);
    }

    [Fact]
    public void FunctionCallWithOneIntParameter_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

        var result = parser.Parse("testFunction(12)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("testFunction", result.Result.FunctionName);
        Assert.Single(result.Result.Arguments);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
    }

    [Fact]
    public void FunctionCallWithTwoIntParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

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
        var parser = FunctionCallNodeParser.FunctionCall;

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
        var parser = FunctionCallNodeParser.FunctionCall;

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
        var parser = FunctionCallNodeParser.FunctionCall;

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

    [Fact]
    public void FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallAsParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

        var result = parser.Parse(@"test_function(12, 99.2324, count(), 9, 22341)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("test_function", result.Result.FunctionName);
        Assert.Equal(5, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.DecimalScalarValue(99.2324m));
        
        Assert.Equal(result.Result.Arguments[2], new FunctionCallExpressionNode("count", []));
        
        Assert.Equal(result.Result.Arguments[3], new ScalarValueNode.IntegerScalarValue(9));
        Assert.Equal(result.Result.Arguments[4], new ScalarValueNode.IntegerScalarValue(22341));
    }

    [Fact]
    public void FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallWithOneParamAsParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

        var result = parser.Parse(@"test_function(12, 99.2324, count(collection), 9, 22341)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("test_function", result.Result.FunctionName);
        Assert.Equal(5, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.DecimalScalarValue(99.2324m));

        var funcParam = (FunctionCallExpressionNode) result.Result.Arguments[2];
        Assert.Equal("count", funcParam.FunctionName);
        Assert.Single(funcParam.Arguments);
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(funcParam.Arguments[0]);
        var variable = (LiteralExpressionNode.VariableValueExpressionNode)(funcParam.Arguments[0]);
        Assert.Equal("collection", variable.VariableName);

        Assert.Equal(result.Result.Arguments[3], new ScalarValueNode.IntegerScalarValue(9));
        Assert.Equal(result.Result.Arguments[4], new ScalarValueNode.IntegerScalarValue(22341));
    }

    [Fact]
    public void FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallWithLotOfParamsAsParameters_ShouldParseFunctionCalls()
    {
        var parser = FunctionCallNodeParser.FunctionCall;

        var result = parser.Parse(@"test_function(12, 99.2324, big_call(collection, 789, count()), 9, 22341)");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.Equal("test_function", result.Result.FunctionName);
        Assert.Equal(5, result.Result.Arguments.Length);
        Assert.Equal(result.Result.Arguments[0], new ScalarValueNode.IntegerScalarValue(12));
        Assert.Equal(result.Result.Arguments[1], new ScalarValueNode.DecimalScalarValue(99.2324m));

        var funcParam = (FunctionCallExpressionNode) result.Result.Arguments[2];
        Assert.Equal("big_call", funcParam.FunctionName);
        Assert.Equal(3, funcParam.Arguments.Length);
        
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(funcParam.Arguments[0]);
        var p1 = (LiteralExpressionNode.VariableValueExpressionNode)funcParam.Arguments[0];
        Assert.Equal("collection", p1.VariableName);

        Assert.IsType<ScalarValueNode.IntegerScalarValue>(funcParam.Arguments[1]);
        var p2 = (ScalarValueNode.IntegerScalarValue)funcParam.Arguments[1];
        Assert.Equal(789, p2.Value);

        Assert.IsType<FunctionCallExpressionNode>(funcParam.Arguments[2]);
        var p3 = (FunctionCallExpressionNode)funcParam.Arguments[2];
        Assert.Equal("count", p3.FunctionName);
        Assert.Empty(p3.Arguments);

        Assert.Equal(result.Result.Arguments[3], new ScalarValueNode.IntegerScalarValue(9));
        Assert.Equal(result.Result.Arguments[4], new ScalarValueNode.IntegerScalarValue(22341));
    }
    
}