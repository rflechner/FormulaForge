using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Contexts;

public interface IDslContext
{
    bool TryGetSeries(string name, out ITimeSeries<decimal> series);

    bool TryGetScalar(string name, out ScalarValueNode? value);
    
    CodeRunResult TrySetScalar(string name, ScalarValueNode value);

    IEnumerable<YearMonth> Months { get; }
    
    HashSet<string> BuiltInVariableNames { get; }
}

public enum CodeRunResult
{
    Success,
    VariableOverwriteNotAllowed,
    BuiltInVariableOverwriteNotAllowed,
}
