using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests.Runtime;

public class TimeSeriesBindingTests
{
    [Fact]
    public void ApplyRateFromBinding_ShouldComputeExpectedValues()
    {
        var year = Period.OfYear(2025);
        var months = year.GetMonths().ToArray();

        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalance = TimeSeries.Create(months.Select((period, i) => new TimeSeriesValue<decimal>(period, i * 100m))),
            AssetsCount = TimeSeries.Create(months.Select((period, i) => new TimeSeriesValue<int>(period, i + 2))),
            ChangeRate = TimeSeries.Create(months.Select((period, i) => new TimeSeriesValue<decimal>(period, 0.9m + i * 0.01m)))
        };
        var context = new CustomerDslContext(dataContext)
        {
            Months = months
        };
        
        var expectedBalanceInCurrency = TimeSeries.Aggregate
            ([dataContext.AccountBalance, dataContext.ChangeRate], 
                (oldVal, newVal) => oldVal.Value * newVal.Value);
        
        var runner = new ScriptInterpreter(context);

        var program = """

                      add(a, b) = a + b
                      
                      apply_rate(amount, rate) = amount * rate
                      
                      balance_in_currency = apply_rate(account_balance, change_rate)
                      
                      x = 2
                      y = add(x, 1)
                      last_year = current_year - 1

                      """;
        runner.Run(program);
        
        Assert.True(context.GlobalScope.TryGetScalar("x", out var xValue));
        Assert.Equal(new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2)), xValue);

        Assert.True(context.GlobalScope.TryGetScalar("y", out var yValue));
        Assert.Equal(new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(3)), yValue);

        Assert.True(context.GlobalScope.TryGetScalar("last_year", out var lastYearValue));
        Assert.Equal(new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2025)), lastYearValue);
        
        Assert.Equal(4, context.GlobalScope.Variables.Count);
    }
}