using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests;

public class ScriptRunnerTests
{
    [Fact]
    public void DeclaringVariableX_ShouldCompileAndSetValueInContext()
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
    
}