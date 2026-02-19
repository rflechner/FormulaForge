using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Contexts;

public abstract class DslContextBase : IDslContext
{
    protected readonly Dictionary<FunctionRegistryId, StatementNode.FunctionDeclarationNode> _functions = new();
    protected readonly Lazy<Scope> _globalScopeFactory;

    public DslContextBase()
    {
        _globalScopeFactory = new Lazy<Scope>(CreateGlobalScope);
    }
    
    public Scope GlobalScope => _globalScopeFactory.Value;
    
    public virtual bool TryGetSeries(string name, out TimeSeries<decimal>? series)
    {
        series = null;
        return false;
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

    protected abstract Scope CreateGlobalScope();
}