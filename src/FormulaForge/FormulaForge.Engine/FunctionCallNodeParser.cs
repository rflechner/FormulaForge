using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class FunctionCallNodeParser
{
    public static readonly IParser<string> FunctionNameParser =
        from start in Parse.Satisfy(c => c.IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(c => c.IsLetterOrUnderscoreOrDigit).Optionnal()
        select $"{start}{rest.GetValueOrDefault(string.Empty)}";

    public static readonly IParser<FunctionCallExpressionNode> FunctionCallWithoutParameters =
        from name in FunctionNameParser
        from sp1 in Parse.SkipSpaces()
        from p1 in Parse.OneChar('(')
        from sp2 in Parse.SkipSpaces()
        from p2 in Parse.OneChar(')')
        select new FunctionCallExpressionNode(name, []);
    
}