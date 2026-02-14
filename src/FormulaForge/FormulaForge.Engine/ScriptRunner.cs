using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine;

public sealed class ScriptRunner(IDslContext context)
{
    public CodeRunResult Run(string code)
    {
        var parsingResult = FormulaProgramScriptParser.ProgramParser.Parse(code);
        if (!parsingResult.Success) throw new Exception("Failed to parse script", new Exception(parsingResult.FailureMessage));

        var astNodes = parsingResult.Result!;

        foreach (var node in astNodes)
        {
            var result = CodeRunResult.Success;

            switch (node)
            {
                case StatementNode.FunctionDeclarationNode functionDeclarationNode:
                    break;
                case StatementNode.VariableAssignmentExpressionNode variableAssignment:
                    var variableName = variableAssignment.Variable.Name;
                    result = RunVariableAssignment(variableAssignment, result, variableName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(node));
            }
            
            if (result != CodeRunResult.Success)
                return result;
        }
        
        return CodeRunResult.Success;
    }

    private CodeRunResult RunVariableAssignment(StatementNode.VariableAssignmentExpressionNode variableAssignment, CodeRunResult result,
        string variableName)
    {
        switch (variableAssignment.Value)
        {
            case ComputedExpressionNode computedExpressionNode:
                break;
            case FunctionCallExpressionNode functionCallExpressionNode:
                break;
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                result = context.TrySetScalar(variableName, constantValueExpressionNode.Value);
                break;
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                break;
            case LiteralExpressionNode literalExpressionNode:

                switch (literalExpressionNode)
                {
                    case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                        result = context.TrySetScalar(variableName, constantValueExpressionNode.Value);
                        break;
                    case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(literalExpressionNode));
                }
                        
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }
}