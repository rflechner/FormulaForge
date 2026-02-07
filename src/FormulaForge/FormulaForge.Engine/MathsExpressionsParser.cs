using System.Globalization;
using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class MathsExpressionsParser
{
    /// <summary>
    /// Parses an integer value.
    /// </summary>
    public static readonly IParser<ScalarValueNode.IntegerScalarValue> IntegerValueParser = 
        Parse.ManySatisfy(char.IsNumber).Select(x => new ScalarValueNode.IntegerScalarValue(int.Parse(x)));
    
    /// <summary>
    /// Parses a decimal value.
    /// </summary>
    public static readonly IParser<ScalarValueNode.DecimalScalarValue> DecimalValueParser = 
        from absolute in Parse.ManySatisfy(char.IsNumber)
        from separator in Parse.OneChar('.')    
        from relative in Parse.ManySatisfy(char.IsNumber)
        select new ScalarValueNode.DecimalScalarValue(decimal.Parse($"{absolute}.{relative}", NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));

    /// <summary>
    /// Parses a boolean value.
    /// </summary>
    public static readonly IParser<ScalarValueNode.BooleanScalarValue> BooleanValueParser = 
        from value in (Parse.StringMatch("true") | Parse.StringMatch("false"))
        from _ in Parse.SkipSpaces()
        select new ScalarValueNode.BooleanScalarValue(bool.Parse(value));
    
    /// <summary>
    /// Parses a scalar value.
    /// </summary>
    public static readonly IParser<ScalarValueNode> ScalarValueParser = 
        BooleanValueParser.Cast<ScalarValueNode.BooleanScalarValue, ScalarValueNode>() 
        | DecimalValueParser.Cast<ScalarValueNode.DecimalScalarValue, ScalarValueNode>()
        | IntegerValueParser.Cast<ScalarValueNode.IntegerScalarValue, ScalarValueNode>();
    
    private static bool IsLetterOrUnderscore(char c) => char.IsLetter(c) || c == '_';
    
    private static bool IsLetterOrUnderscoreOrDigit(char c) => char.IsLetterOrDigit(c) || c == '_';
    
    private static readonly IParser<LiteralExpressionNode.VariableValueExpressionNode> VariableNameContainingNumbersParser =
        from start in Parse.ManySatisfy(IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(IsLetterOrUnderscoreOrDigit)
        select new LiteralExpressionNode.VariableValueExpressionNode(new string(start.Concat(rest).ToArray()));

    /// <summary>
    /// Parses a variable value expression.
    /// </summary>
    public static readonly IParser<LiteralExpressionNode.VariableValueExpressionNode> VariableValueExpressionParser =
        VariableNameContainingNumbersParser 
            | Parse.ManySatisfy(IsLetterOrUnderscore)
                .Select(x => new LiteralExpressionNode.VariableValueExpressionNode(x.ToString()));
    
    /// <summary>
    /// Parses a constant value expression.
    /// </summary>
    public static readonly IParser<LiteralExpressionNode.ConstantValueExpressionNode> ConstantValueExpressionParser =
        ScalarValueParser.Select(x => new LiteralExpressionNode.ConstantValueExpressionNode(x));
    
    /// <summary>
    /// Parses a literal expression.
    /// </summary>
    public static readonly IParser<LiteralExpressionNode> LiteralExpressionNodeParser =
        ConstantValueExpressionParser.Cast<LiteralExpressionNode.ConstantValueExpressionNode, LiteralExpressionNode>()
            | VariableValueExpressionParser.Cast<LiteralExpressionNode.VariableValueExpressionNode, LiteralExpressionNode>();
    
    
    public static readonly IParser<OperationExpressionNode.ReadExpressionNode> ReadExpressionParser =
        LiteralExpressionNodeParser.Select(x => new OperationExpressionNode.ReadExpressionNode(x));

    private static IParser<OperationExpressionNode.BinaryOperationExpressionNode> BasicBinaryOperationExpressionParser =>
        from left in ReadExpressionParser
        from trimLeft in Parse.SkipSpaces()
        from @operator in Parse.StringMatch("+") | Parse.StringMatch("-") | Parse.StringMatch("*") | Parse.StringMatch("/")
        from trimOperator in Parse.SkipSpaces()
        from right in ReadExpressionParser
        from trimRight in Parse.SkipSpaces()
        select new OperationExpressionNode.BinaryOperationExpressionNode(left, right, @operator);
    
    public static readonly IParser<OperationExpressionNode.BinaryOperationExpressionNode[]> BinaryOperationExpressionParser =
            Parse.Many(BasicBinaryOperationExpressionParser).Select(x => x.ToArray());
    
}