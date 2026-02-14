using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class FormulaProgramScriptParser
{
    public static readonly IParser<AstNode[]> ProgramParser = 
        Parse.Many(StatementsParser.AssignmentParser)
            .Select(statements => statements.ToArray());
}
