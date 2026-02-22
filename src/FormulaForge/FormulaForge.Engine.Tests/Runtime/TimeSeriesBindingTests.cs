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
            AccountBalance = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(period, new ScalarValueNode.DecimalScalarValue(i * 100m))).CreateTimeSeries(),
            AssetsCount = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.IntegerScalarValue>(period, new ScalarValueNode.IntegerScalarValue(i + 2))).CreateTimeSeries(),
            ChangeRate = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(period, new ScalarValueNode.DecimalScalarValue(0.9m + i * 0.01m))).CreateTimeSeries()
        };
        var context = new CustomerDslContext(dataContext)
        {
            Months = months
        };
        
        var expectedBalanceInCurrency = TimeSeries.Aggregate
            ([dataContext.AccountBalance, dataContext.ChangeRate], 
                (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value * newVal.Value.Value));
        
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

        Assert.True(context.GlobalScope.TryGetScalar("balance_in_currency", out var balanceInCurrencyValue));
        TimeSeries<ScalarValueNode> balanceInCurrencyTimeSeries = Assert.IsType<RuntimeVariableValue.RuntimeTimeSeriesValue>(balanceInCurrencyValue).Value;
        var decimalScalarValues = balanceInCurrencyTimeSeries
            .Select(t => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(t.Period, (ScalarValueNode.DecimalScalarValue)t.Value))
            .CreateTimeSeries();

        Assert.Equal(expectedBalanceInCurrency, decimalScalarValues);

        Assert.Equal(4, context.GlobalScope.Variables.Count);
    }
    
    [Fact]
    public void ApplyRateFromBindingWithTimeSeriesOfDecimalAndInteger_ShouldComputeExpectedValues()
    {
        var year = Period.OfYear(2025);
        var months = year.GetMonths().ToArray();

        var dataContext = new CustomerDataContext
        {
            CustomerId = "123456789",
            AccountBalance = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(period, new ScalarValueNode.DecimalScalarValue(i * 100m))).CreateTimeSeries(),
            AssetsCount = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.IntegerScalarValue>(period, new ScalarValueNode.IntegerScalarValue(i + 2))).CreateTimeSeries(),
            ChangeRate = months.Select((period, i) => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(period, new ScalarValueNode.DecimalScalarValue(0.9m + i * 0.01m))).CreateTimeSeries(),
            
        };
        var context = new CustomerDslContext(dataContext)
        {
            Months = months
        };

        var assetsCount = dataContext.AssetsCount
            .Select(i => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(i.Period, new ScalarValueNode.DecimalScalarValue(i.Value.Value)))
            .CreateTimeSeries();
        var expectedBalanceInCurrency = TimeSeries.Aggregate
            ([assetsCount, dataContext.ChangeRate], 
                (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value * newVal.Value.Value));
        
        var runner = new ScriptInterpreter(context);

        var program = """
                      compute_something(a, b) = a * b
                      virtual_assets = compute_something(assets_count, change_rate)
                      """;
        runner.Run(program);
        
        Assert.True(context.GlobalScope.TryGetScalar("virtual_assets", out var balanceInCurrencyValue));
        
        TimeSeries<ScalarValueNode> balanceInCurrencyTimeSeries = Assert.IsType<RuntimeVariableValue.RuntimeTimeSeriesValue>(balanceInCurrencyValue).Value;
        var decimalScalarValues = balanceInCurrencyTimeSeries
            .Select(t => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(t.Period, (ScalarValueNode.DecimalScalarValue)t.Value))
            .CreateTimeSeries();

        Assert.Equal(expectedBalanceInCurrency, decimalScalarValues);
    }
}