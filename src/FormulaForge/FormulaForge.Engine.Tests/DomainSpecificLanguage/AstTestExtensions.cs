using System.Linq;
using EasyParsing;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Tests.DomainSpecificLanguage;

public static class AstTestExtensions
{
    public static T IgnorePosition<T>(this T? node) where T : AstNode
    {
        if (node == null) return null!;

        return node switch
        {
            LiteralExpressionNode.ConstantValueExpressionNode c => (T)(object)(c with { PositionRange = default }),
            LiteralExpressionNode.VariableValueExpressionNode v => (T)(object)(v with 
            { 
                PositionRange = default,
                VariableName = v.VariableName with { PositionRange = default }
            }),
            VariableName vn => (T)(object)(vn with { PositionRange = default }),
            FunctionCallExpressionNode f => (T)(object)(f with 
            { 
                PositionRange = default,
                Arguments = f.Arguments.Select(IgnorePosition).ToArray()
            }),
            ComputedExpressionNode ce => (T)(object)(ce with { PositionRange = default }),
            StatementNode.VariableAssignmentExpressionNode va => (T)(object)(va with
            {
                PositionRange = default,
                Variable = va.Variable.IgnorePosition(),
                Value = va.Value.IgnorePosition()
            }),
            _ => node
        };
    }
}
