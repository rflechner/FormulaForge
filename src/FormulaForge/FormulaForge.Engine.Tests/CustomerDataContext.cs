using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
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
    
    private readonly Dictionary<FunctionRegistryId, Dictionary<string, ScalarValueNode>> _scopedVariables = new();
    
    private readonly Dictionary<FunctionRegistryId, StatementNode.FunctionDeclarationNode> _functions = new();
    
    public IEnumerable<YearMonth> Months => c.AccountBalanceByMonth.Months.Concat(c.AssetsCountByMonth.Months).Distinct();
    
    public HashSet<string> BuiltInVariableNames => [
        "balance", 
        "assets",
        "current_month",
        "current_year"
    ];

    public Scope GlobalScope { get; } = new()
    {
        BuiltInVariableNames = [
            "balance", 
            "assets",
            "current_month",
            "current_year"
        ]
    };

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

    public Scope GetScope(FunctionRegistryId scope)
    {
        return new Scope();
    }
    
    public bool TryGetFunction(FunctionRegistryId id, out StatementNode.FunctionDeclarationNode? function)
    {
        return _functions.TryGetValue(id, out function);
    }

    public CodeRunResult RegisterFunction(FunctionRegistryId id, StatementNode.FunctionDeclarationNode function)
    {
        return _functions.TryAdd(id, function) ? CodeRunResult.Success : CodeRunResult.FunctionOverwriteNotAllowed;
    }
}