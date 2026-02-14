using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class FormulaProgramScriptParser
{
    public static readonly IParser<AstNode[]> ProgramParser = 
        Parse
            .Many(
                StatementsParser.AssignmentParser.Cast<StatementNode.VariableAssignmentExpressionNode, StatementNode>()
                | StatementsParser.FunctionDeclarationParser.Cast<StatementNode.FunctionDeclarationNode, StatementNode>()
                )
            .Select(statements => statements.ToArray());
}
