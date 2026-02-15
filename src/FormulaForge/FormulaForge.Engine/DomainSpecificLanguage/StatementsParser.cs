using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

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
    
    public static readonly IParser<StatementNode.FunctionDeclarationNode> FunctionDeclarationParser =
        from sp0 in Parse.SkipSpaces()
        from signature in FunctionsNodesParser.FunctionSignatureParser
        from sp1 in Parse.SkipSpaces()
        from eq in Parse.StringMatch("=")
        from sp2 in Parse.SkipSpaces()
        from body in ValueExpressionNodeParser.ValueExpression
        select new StatementNode.FunctionDeclarationNode(signature.FunctionName, signature.Parameters, body);
    
}