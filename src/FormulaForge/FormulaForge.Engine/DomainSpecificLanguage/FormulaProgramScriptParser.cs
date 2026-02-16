using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

public static class FormulaProgramScriptParser
{
    public static readonly IParser<AstNode[]> ProgramParser = 
        Parse
            .Many(
                StatementsParser.CommentParser.Cast<CommentNode, AstNode>()
                | StatementsParser.AssignmentParser.Cast<StatementNode.VariableAssignmentExpressionNode, StatementNode>()
                | StatementsParser.FunctionDeclarationParser.Cast<StatementNode.FunctionDeclarationNode, StatementNode>()
                )
            .Select(statements => statements.ToArray());
}
