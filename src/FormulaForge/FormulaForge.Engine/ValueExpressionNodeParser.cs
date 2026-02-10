using System.Globalization;
using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine;

public class ValueExpressionNodeParser
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
    
    private static readonly IParser<LiteralExpressionNode.VariableValueExpressionNode> VariableNameContainingNumbersParser =
        from start in Parse.ManySatisfy(c => c.IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(c => c.IsLetterOrUnderscoreOrDigit)
        select new LiteralExpressionNode.VariableValueExpressionNode(new string(start.Concat(rest).ToArray()));

    /// <summary>
    /// Parses a variable value expression.
    /// </summary>
    public static readonly IParser<LiteralExpressionNode.VariableValueExpressionNode> VariableValueExpressionParser =
        VariableNameContainingNumbersParser 
            | Parse.ManySatisfy(c => c.IsLetterOrUnderscore)
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

    private static IParser<OperationExpressionNode> TermParser =>
        (from lparen in Parse.OneChar('(')
         from expr in ExpressionParser
         from rparen in Parse.OneChar(')')
         select expr).Cast<OperationExpressionNode, OperationExpressionNode>()
        | ReadExpressionParser.Cast<OperationExpressionNode.ReadExpressionNode, OperationExpressionNode>();

    private static IParser<OperationExpressionNode> MultiplicationParser =>
        from left in TermParser
        from rest in Parse.Many(
            from _1 in Parse.SkipSpaces()
            from op in (Parse.StringMatch("*") | Parse.StringMatch("/"))
            from _2 in Parse.SkipSpaces()
            from right in TermParser
            select (op, right)
        )
        select rest.Aggregate(left, (acc, next) => new OperationExpressionNode.BinaryOperationExpressionNode(acc, next.right, next.op));

    private static IParser<OperationExpressionNode> AdditionParser =>
        from left in MultiplicationParser
        from rest in Parse.Many(
            from _1 in Parse.SkipSpaces()
            from op in (Parse.StringMatch("+") | Parse.StringMatch("-") )
            from _2 in Parse.SkipSpaces()
            from right in MultiplicationParser
            select (op, right)
        )
        select rest.Aggregate(left, (acc, next) => new OperationExpressionNode.BinaryOperationExpressionNode(acc, next.right, next.op));

    private static IParser<OperationExpressionNode> ExpressionParser => AdditionParser;

    public static readonly IParser<OperationExpressionNode> BinaryOperationExpressionParser = 
        from _1 in Parse.SkipSpaces()
        from expr in ExpressionParser
        from _2 in Parse.SkipSpaces()
        select expr;
    
}