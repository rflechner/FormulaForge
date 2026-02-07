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
    
}