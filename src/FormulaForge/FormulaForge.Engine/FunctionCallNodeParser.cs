using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class FunctionCallNodeParser
{
    public static readonly IParser<string> FunctionNameParser =
        from start in Parse.Satisfy(c => c.IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(c => c.IsLetterOrUnderscoreOrDigit).Optionnal()
        select $"{start}{rest.GetValueOrDefault(string.Empty)}";

    public static readonly IParser<FunctionCallExpressionNode> FunctionSignatureWithoutParameters =
        from name in FunctionNameParser
        from sp1 in Parse.SkipSpaces()
        from p1 in Parse.OneChar('(')
        from sp2 in Parse.SkipSpaces()
        from p2 in Parse.OneChar(')')
        select new FunctionCallExpressionNode(name, []);

    private static readonly IParser<string> FunctionSignatureParameterSeparatorParser =
        from sp1 in Parse.SkipSpaces()
        from sep in Parse.StringMatch(",")
        from sp2 in Parse.SkipSpaces()
        select sep;

    internal static readonly IParser<ValueExpressionNode> ValueAccessParser =
        new LazyParser<ValueExpressionNode>(() => FunctionCall! | ValueExpressionNodeParser.ValueExpression);
        
    internal static readonly IParser<ValueExpressionNode[]> FunctionCallMultipleParametersParser = 
        from sp1 in Parse.SkipSpaces()
        from values in ValueAccessParser.SeparatedBy(FunctionSignatureParameterSeparatorParser)
        from sp2 in Parse.SkipSpaces()
        select values;

    internal static readonly IParser<ValueExpressionNode[]> FunctionCallSingleParameterParser =
        ValueAccessParser.Select(r => new[] { r });
    
    public static readonly IParser<FunctionCallExpressionNode> FunctionSignatureWithParameters =
        from name in FunctionNameParser
        from sp1 in Parse.SkipSpaces()
        from parameters in Parse.Between(Parse.OneChar('('), FunctionCallMultipleParametersParser | FunctionCallSingleParameterParser,  Parse.OneChar(')'))
        select new FunctionCallExpressionNode(name, parameters.Item);

    public static readonly IParser<FunctionCallExpressionNode> FunctionCall =
        FunctionSignatureWithParameters | FunctionSignatureWithoutParameters;
    
    internal static readonly IParser<VariableName[]> FunctionSignatureMultipleParametersParser = 
        from sp1 in Parse.SkipSpaces()
        from names in ValueExpressionNodeParser.VariableNameParser.SeparatedBy(FunctionSignatureParameterSeparatorParser)
        from sp2 in Parse.SkipSpaces()
        select names;

    public static readonly IParser<FunctionSignatureExpressionNode> FunctionSignatureParser =
        from name in FunctionNameParser
        from sp1 in Parse.SkipSpaces()
        from parameters in Parse.Between(Parse.OneChar('('), FunctionSignatureMultipleParametersParser,  Parse.OneChar(')'))
        select new FunctionSignatureExpressionNode(name, parameters.Item);
    
    public static IParser<StatementNode.FunctionDeclarationNode> FunctionDeclarationParser =
        from signature in FunctionSignatureParser
        from sp1 in Parse.SkipSpaces()
        from eq in Parse.StringMatch("=")
        from sp2 in Parse.SkipSpaces()
        from body in ValueExpressionNodeParser.ValueExpression
        select new StatementNode.FunctionDeclarationNode(signature.FunctionName, signature.Parameters, body);
}