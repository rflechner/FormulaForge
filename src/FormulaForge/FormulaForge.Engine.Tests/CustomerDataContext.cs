using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests;

public sealed class CustomerDataContext
{
    public required string CustomerId { get; init; }

    public required YearMonth ReferenceMonth { get; init; }

    public required MonthlySeries<decimal> AccountBalanceByMonth { get; init; }

    public required MonthlySeries<int> AssetsCountByMonth { get; init; }

    public decimal Balance(int monthOffset = 0)
        => AccountBalanceByMonth.GetOrThrow(ReferenceMonth.AddMonths(monthOffset), "Balance");

    public int Assets(int monthOffset = 0)
        => AssetsCountByMonth.GetOrThrow(ReferenceMonth.AddMonths(monthOffset), "AssetsCount");
}

public sealed class CustomerDslContext(CustomerDataContext c) : IDslContext
{
    private readonly Dictionary<string, ScalarValueNode> _variables = new();
    
    public IEnumerable<YearMonth> Months => c.AccountBalanceByMonth.Months.Concat(c.AssetsCountByMonth.Months).Distinct();
    
    public HashSet<string> BuiltInVariableNames => [
        "balance", 
        "assets",
        "current_month",
        "current_year"
    ];

    public bool TryGetSeries(string name, out ITimeSeries<decimal> series)
    {
        series = null!;
        return name switch
        {
            //"balance" => Wrap(c.AccountBalanceByMonth, out series),
            //"assets"  => Wrap(c.AssetsCountByMonth, out series), // convert int->decimal
            _ => false
        };
    }

    public bool TryGetScalar(string name, out ScalarValueNode? value)
    {
        switch (name)
        {
            case "current_month":
                value = new ScalarValueNode.IntegerScalarValue(TimeProvider.System.GetLocalNow().Month);
                return true;
            case "current_year":
                value = new ScalarValueNode.IntegerScalarValue(TimeProvider.System.GetLocalNow().Year);
                return true;
        }
        
        if (_variables.TryGetValue(name, out value)) return true;
        
        value = null;
        return false;
    }

    public CodeRunResult TrySetScalar(string name, ScalarValueNode value)
    {
        if (BuiltInVariableNames.Contains(name))
            return CodeRunResult.BuiltInVariableOverwriteNotAllowed;
        
        if (!_variables.TryAdd(name, value))
            return CodeRunResult.VariableOverwriteNotAllowed;

        return CodeRunResult.Success;
    }
    
}