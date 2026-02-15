namespace FormulaForge.Engine.Runtime;

public enum CodeRunResult
{
    Success,
    VariableOverwriteNotAllowed,
    BuiltInVariableOverwriteNotAllowed,
    FunctionNotFound,
    FunctionOverwriteNotAllowed,
}