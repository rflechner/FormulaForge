using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;

namespace FormulaForge.Engine.Contexts;

public sealed class Scope
{
    public Dictionary<string, RuntimeVariableValue> BuiltInVariables { get; init; } = new();
    
    public Dictionary<string, RuntimeVariableValue> Variables { get; } = new();
    
    public Scope? Parent { get; init; }
    
    public Scope CreateChild() => new()
    {
        Parent = this,
        BuiltInVariables = Parent?.BuiltInVariables ?? BuiltInVariables,
    };
    
    public bool TryGetScalar(string name, out RuntimeVariableValue? value)
    {
        if (BuiltInVariables.TryGetValue(name, out var builtInValue))
        {
            value = builtInValue;
            return true;
        }
        
        if (Variables.TryGetValue(name, out value)) return true;
        
        value = null;
        return false;
    }
    
    public CodeRunResult TrySetScalar(string name, RuntimeVariableValue value)
    {
        if (BuiltInVariables.ContainsKey(name))
            return CodeRunResult.BuiltInVariableOverwriteNotAllowed;
        
        if (!Variables.TryAdd(name, value))
            return CodeRunResult.VariableOverwriteNotAllowed;

        return CodeRunResult.Success;
    }
}
