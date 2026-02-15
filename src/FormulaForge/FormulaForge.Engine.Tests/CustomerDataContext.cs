using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests;

public sealed class CustomerDataContext
{
    public required string CustomerId { get; init; }

    public required TimeSeries<decimal> AccountBalance { get; init; }

    public required TimeSeries<int> AssetsCount { get; init; }

    public TimeSeries<decimal> ChangeRate { get; init; } = new();
}

public sealed class CustomerDslContext(CustomerDataContext c) : IDslContext
{
    private readonly Dictionary<FunctionRegistryId, StatementNode.FunctionDeclarationNode> _functions = new();
    
    public IEnumerable<Period> Months { get; init; } = [];
    
    public Scope GlobalScope { get; } = new()
    {
        BuiltInVariables = new Dictionary<string, RuntimeVariableValue>
        {
            ["current_month"] = new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2)),
            ["current_year"] = new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(2026)),
            ["balance"] = new RuntimeVariableValue.RuntimeComplexValue(c.AccountBalance),
            ["change_rate"] = new RuntimeVariableValue.RuntimeComplexValue(c.ChangeRate),
        },
    };

    public bool TryGetSeries(string name, out TimeSeries<decimal> series)
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
