using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests.Runtime;

public class ScriptInterpreterTests
{
    [Fact]
    public void DeclaringIntVariableX_ShouldCompileAndSetValueInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);
        
        var program = "x = 123";
        runner.Run(program);
        
        var variableExists = context.GlobalScope.TryGetScalar("x", out var xValue);
        Assert.True(variableExists);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(123), xValue);
    }
    
    [Fact]
    public void DeclaringMultipleVariables_ShouldCompileAndSetValuesInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);
        
        var program = """

                      x = 123
                      y = 456.789
                      b = true
                      c = false

                      """;
        runner.Run(program);
        
        Assert.True(context.GlobalScope.TryGetScalar("x", out var xValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(123), xValue);
        
        Assert.True(context.GlobalScope.TryGetScalar("y", out var yValue));
        Assert.Equal(new ScalarValueNode.DecimalScalarValue(456.789m), yValue);
        
        Assert.True(context.GlobalScope.TryGetScalar("b", out var bValue));
        Assert.Equal(new ScalarValueNode.BooleanScalarValue(true), bValue);
        
        Assert.True(context.GlobalScope.TryGetScalar("c", out var cValue));
        Assert.Equal(new ScalarValueNode.BooleanScalarValue(false), cValue);
    }
    
    [Theory]
    [InlineData("x = 180 + 20", 180 + 20)]
    [InlineData("x = 12234 + 5 * 9 / (9 - 3 * (4+7))", 12234 + 5 * 9 / (9 - 3 * (4+7)))]
    public void AssignSumOfIntsToVariableX_ShouldCompileAndSetValueInContext(string program, int expectedValue)
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);
        
        runner.Run(program);
        
        var variableExists = context.GlobalScope.TryGetScalar("x", out var xValue);
        Assert.True(variableExists);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(expectedValue), xValue);
    }
    
    [Fact]
    public void AssignSumOfDecimalsToVariableX_ShouldCompileAndSetValueInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);

        string program = "x = 12234.0 + 5.2 * 9.5 / (9.1 - 9.3 * (4+7))";
        decimal expectedValue = 12234.0m + 5.2m * 9.5m / (9.1m - 9.3m * (4+7));
        runner.Run(program);
        
        var variableExists = context.GlobalScope.TryGetScalar("x", out var xValue);
        Assert.True(variableExists);
        Assert.Equal(new ScalarValueNode.DecimalScalarValue(expectedValue), xValue);
    }
    
    [Fact]
    public void AssignFunctionCallResultToVariableX_ShouldCompileAndSetValueInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);

        string program = """
                         
                         add(a, b) = a + b
                         
                         y = 1+1
                         x = add(1, 2)
                         
                         """;
        runner.Run(program);
        
        Assert.True(context.GlobalScope.TryGetScalar("y", out var yValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(2), yValue);
        
        Assert.True(context.GlobalScope.TryGetScalar("x", out var xValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(3), xValue);
        
        Assert.Equal(2, context.GlobalScope.Variables.Count);
    }    
    [Fact]
    public void AssignVariableYToVariableX_ShouldCompileAndSetValueInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalanceByMonth = new TimeSeries<decimal>(),
            AssetsCountByMonth = new TimeSeries<int>(),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptInterpreter(context);

        string program = """
                         y = 1 + 1
                         x = y
                         """;
        runner.Run(program);
        
        Assert.True(context.GlobalScope.TryGetScalar("y", out var yValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(2), yValue);
        
        Assert.True(context.GlobalScope.TryGetScalar("x", out var xValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(2), xValue);
        
        Assert.Equal(2, context.GlobalScope.Variables.Count);
    }
    
}