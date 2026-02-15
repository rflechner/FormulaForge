namespace FormulaForge.Engine.Runtime;

public enum CodeRunResult
{
    Success,
    VariableNotFound,
    VariableOverwriteNotAllowed,
    BuiltInVariableOverwriteNotAllowed,
    FunctionNotFound,
    FunctionOverwriteNotAllowed,
}