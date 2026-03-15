using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

public static class StatementsParser
{
    public static readonly IParser<StatementNode.VariableAssignmentExpressionNode> AssignmentParser =
        from sp1 in Parse.SkipSpaces()
        from variable in ValueExpressionNodeParser.VariableNameParser.Track()
        from sp2 in Parse.SkipSpaces()
        from assignmentOperator in Parse.StringMatch("=")
        from sp3 in Parse.SkipSpaces()
        from value in ValueExpressionNodeParser.ValueExpression.Track()
        from sp4 in Parse.SkipSpaces()
        select new StatementNode.VariableAssignmentExpressionNode(variable.Range + value.Range, variable.Value, value.Value);
    
    public static readonly IParser<StatementNode.FunctionDeclarationNode> FunctionDeclarationParser =
        from sp0 in Parse.SkipSpaces()
        from signature in FunctionsNodesParser.FunctionSignatureParser.Track()
        from sp1 in Parse.SkipSpaces()
        from eq in Parse.StringMatch("=")
        from sp2 in Parse.SkipSpaces()
        from body in ValueExpressionNodeParser.ValueExpression.Track()
        select new StatementNode.FunctionDeclarationNode(signature.Range + body.Range, signature.Value.FunctionName, signature.Value.Parameters, body.Value);
    
    public static readonly IParser<CommentNode> CommentParser =
        from sp1 in Parse.SkipSpaces() 
        from c in Parse.OneChar('#')
        from text in Parse.ManySatisfy(c => c != '\n').Track()
        select new CommentNode(text.Range, text.Value.Trim());
    
}