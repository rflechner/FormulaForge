using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

public class FunctionsNodesParser
{
    public static readonly IParser<string> FunctionNameParser =
        from start in Parse.Satisfy(c => c.IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(c => c.IsLetterOrUnderscoreOrDigit).Optionnal()
        select $"{start}{rest.GetValueOrDefault(string.Empty)}";

    public static readonly IParser<FunctionCallExpressionNode> FunctionSignatureWithoutParameters =
        from name in FunctionNameParser.Track()
        from sp1 in Parse.SkipSpaces()
        from p1 in Parse.OneChar('(')
        from sp2 in Parse.SkipSpaces()
        from p2 in Parse.OneChar(')').Track()
        select new FunctionCallExpressionNode(name.Range + p2.Range, name.Value, []);

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
        from name in FunctionNameParser.Track()
        from sp1 in Parse.SkipSpaces()
        from parameters in Parse.Between(Parse.OneChar('('), FunctionCallMultipleParametersParser | FunctionCallSingleParameterParser,  Parse.OneChar(')')).Track()
        select new FunctionCallExpressionNode(name.Range + parameters.Range, name.Value, parameters.Value.Item);

    public static readonly IParser<FunctionCallExpressionNode> FunctionCall =
        FunctionSignatureWithParameters | FunctionSignatureWithoutParameters;
    
    internal static readonly IParser<VariableName[]> FunctionSignatureMultipleParametersParser = 
        from sp1 in Parse.SkipSpaces()
        from names in ValueExpressionNodeParser.VariableNameParser.SeparatedBy(FunctionSignatureParameterSeparatorParser)
        from sp2 in Parse.SkipSpaces()
        select names;

    public static readonly IParser<FunctionSignatureExpressionNode> FunctionSignatureParser =
        from name in FunctionNameParser.Track()
        from sp1 in Parse.SkipSpaces()
        from parameters in Parse.Between(Parse.OneChar('('), FunctionSignatureMultipleParametersParser,  Parse.OneChar(')')).Track()
        select new FunctionSignatureExpressionNode(name.Range + parameters.Range, name.Value, parameters.Value.Item);
    
}