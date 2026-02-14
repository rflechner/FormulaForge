using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public static class StatementsParser
{
    
    public static readonly IParser<StatementNode.VariableAssignmentExpressionNode> AssignmentParser =
        from sp1 in Parse.SkipSpaces()
        from variable in ValueExpressionNodeParser.VariableNameParser
        from sp2 in Parse.SkipSpaces()
        from assignmentOperator in Parse.StringMatch("=")
        from sp3 in Parse.SkipSpaces()
        from value in ValueExpressionNodeParser.ValueExpression
        from sp4 in Parse.SkipSpaces()
        select new StatementNode.VariableAssignmentExpressionNode(variable, value);
    
}