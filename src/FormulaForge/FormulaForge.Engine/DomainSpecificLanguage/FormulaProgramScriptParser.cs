using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

public static class FormulaProgramScriptParser
{
    public static readonly IParser<InvalidLine> InvalidLineParser =
        from sp1 in Parse.SkipSpaces() 
        from text in Parse.ManySatisfy(c => c != '\n').Track()
        select new InvalidLine(text.Range, text.Value.Trim());

    public static readonly IParser<AstNode[]> ProgramParser = 
        Parse
            .Many(
                StatementsParser.CommentParser.Cast<CommentNode, AstNode>()
                | StatementsParser.AssignmentParser.Cast<StatementNode.VariableAssignmentExpressionNode, StatementNode>()
                | StatementsParser.FunctionDeclarationParser.Cast<StatementNode.FunctionDeclarationNode, StatementNode>()
                | InvalidLineParser.Cast<InvalidLine, AstNode>()
                )
            .Select(statements => statements.ToArray());
}
