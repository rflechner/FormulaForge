using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests;

public class ScriptRunnerTests
{
    [Fact]
    public void DeclaringIntVariableX_ShouldCompileAndSetValueInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            ReferenceMonth = new YearMonth(2022, 12),
            AccountBalanceByMonth = new MonthlySeries<decimal>([]),
            AssetsCountByMonth = new MonthlySeries<int>([]),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptRunner(context);
        
        var program = "x = 123";
        runner.Run(program);
        
        var variableExists = context.TryGetScalar("x", out var xValue);
        Assert.True(variableExists);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(123), xValue);
    }
    
    [Fact]
    public void DeclaringMultipleVariables_ShouldCompileAndSetValuesInContext()
    {
        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            ReferenceMonth = new YearMonth(2022, 12),
            AccountBalanceByMonth = new MonthlySeries<decimal>([]),
            AssetsCountByMonth = new MonthlySeries<int>([]),
        };
        var context = new CustomerDslContext(dataContext);
        var runner = new ScriptRunner(context);
        
        var program = """

                      x = 123
                      y = 456.789
                      b = true
                      c = false

                      """;
        runner.Run(program);
        
        Assert.True(context.TryGetScalar("x", out var xValue));
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(123), xValue);
        
        Assert.True(context.TryGetScalar("y", out var yValue));
        Assert.Equal(new ScalarValueNode.DecimalScalarValue(456.789m), yValue);
        
        Assert.True(context.TryGetScalar("b", out var bValue));
        Assert.Equal(new ScalarValueNode.BooleanScalarValue(true), bValue);
        
        Assert.True(context.TryGetScalar("c", out var cValue));
        Assert.Equal(new ScalarValueNode.BooleanScalarValue(false), cValue);
    }
    
}