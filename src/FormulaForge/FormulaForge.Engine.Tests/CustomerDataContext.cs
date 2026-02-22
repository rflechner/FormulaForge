using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests;

public sealed class CustomerDataContext
{
    public required string CustomerId { get; init; }

    public required TimeSeries<ScalarValueNode.DecimalScalarValue> AccountBalance { get; init; }

    public required TimeSeries<ScalarValueNode.IntegerScalarValue> AssetsCount { get; init; }

    public TimeSeries<ScalarValueNode.DecimalScalarValue> ChangeRate { get; init; } = new();
}

public sealed class CustomerDslContext : DslContextBase
{
    
    private readonly CustomerDataContext _c;

    public CustomerDslContext(CustomerDataContext c)
    {
        _c = c;
    }
    
    public IEnumerable<Period> Months { get; init; } = [];
    
    protected override Scope CreateGlobalScope()
    {
        var accountBalance = _c.AccountBalance
            .Cast<ScalarValueNode, ScalarValueNode.DecimalScalarValue>();

        var changeRate = _c.ChangeRate
            .Cast<ScalarValueNode, ScalarValueNode.DecimalScalarValue>();

        var assetsCount = _c.AssetsCount
            .Cast<ScalarValueNode, ScalarValueNode.IntegerScalarValue>();

        return new()
        {
            BuiltInVariables = new Dictionary<string, RuntimeVariableValue>
            {
                ["current_month"] = new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2)),
                ["current_year"] = new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2026)),
                ["account_balance"] = new RuntimeVariableValue.RuntimeTimeSeriesValue(accountBalance),
                ["change_rate"] = new RuntimeVariableValue.RuntimeTimeSeriesValue(changeRate),
                ["assets_count"] = new RuntimeVariableValue.RuntimeTimeSeriesValue(assetsCount),
            },
        };
    }

    public override bool TryGetSeries(string name, out TimeSeries<decimal>? series)
    {
        series = null!;
        return name switch
        {
            //"balance" => Wrap(c.AccountBalanceByMonth, out series),
            //"assets"  => Wrap(c.AssetsCountByMonth, out series), // convert int->decimal
            _ => false
        };
    }
}
