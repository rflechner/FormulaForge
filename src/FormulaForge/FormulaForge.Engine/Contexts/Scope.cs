using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;

namespace FormulaForge.Engine.Contexts;

public sealed class Scope
{
    public HashSet<string> BuiltInVariableNames { get; init; } = new();
    
    public Dictionary<string, ScalarValueNode> Variables { get; } = new();
    
    public Scope? Parent { get; init; }
    
    public Scope CreateChild() => new()
    {
        Parent = this,
        BuiltInVariableNames = Parent?.BuiltInVariableNames ?? BuiltInVariableNames,
    };
    
    public bool TryGetScalar(string name, out ScalarValueNode? value)
    {
        if (Variables.TryGetValue(name, out value)) return true;
        
        value = null;
        return false;
    }
    
    public CodeRunResult TrySetScalar(string name, ScalarValueNode value)
    {
        if (BuiltInVariableNames.Contains(name))
            return CodeRunResult.BuiltInVariableOverwriteNotAllowed;
        
        if (!Variables.TryAdd(name, value))
            return CodeRunResult.VariableOverwriteNotAllowed;

        return CodeRunResult.Success;
    }
}
