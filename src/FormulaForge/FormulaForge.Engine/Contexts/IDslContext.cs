using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Contexts;

public interface IDslContext
{
    bool TryGetSeries(string name, out ITimeSeries<decimal> series);

    Scope GetScope(FunctionRegistryId scope);
    
    IEnumerable<YearMonth> Months { get; }
    
    Scope GlobalScope { get; }

    bool TryGetFunction(FunctionRegistryId id, out StatementNode.FunctionDeclarationNode? function);

    CodeRunResult RegisterFunction(FunctionRegistryId id, StatementNode.FunctionDeclarationNode function);
}