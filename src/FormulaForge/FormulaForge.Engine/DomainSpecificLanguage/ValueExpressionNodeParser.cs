using System.Globalization;
using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers;
using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.DomainSpecificLanguage;

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
    
    private static readonly IParser<VariableName> VariableNameContainingNumbersParser =
        from start in Parse.ManySatisfy(c => c.IsLetterOrUnderscore)
        from rest in Parse.ManySatisfy(c => c.IsLetterOrUnderscoreOrDigit)
        select new VariableName($"{start}{rest}");

    /// <summary>
    /// Parses a variable value expression.
    /// </summary>
    public static readonly IParser<VariableName> VariableNameParser =
        VariableNameContainingNumbersParser 
            | Parse.ManySatisfy(c => c.IsLetterOrUnderscore)
                .Select(x => new VariableName(x));
    
    public static readonly IParser<LiteralExpressionNode.VariableValueExpressionNode> VariableValueExpressionParser =
        VariableNameParser
                .Select(x => new LiteralExpressionNode.VariableValueExpressionNode(x));
    
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
    
    public static IParser<ComputedExpressionNode> OperationsParser
    {
        get
        {
            var operandBody = new LazyParser<ValueExpressionNode>(() => FunctionsNodesParser.FunctionCall! | ValueAccessExpression);
            var operandParser =
                from _ in Parse.SkipSpaces()
                from n in operandBody
                from __ in Parse.SkipSpaces()
                select new BinaryOperationOperandValue<ValueExpressionNode>(n);

            var subOperationStart = Parse.SkipSpaces() << Parse.StringMatch("(") >> Parse.SkipSpaces();
            var subOperationEnd   = Parse.SkipSpaces() << Parse.StringMatch(")") >> Parse.SkipSpaces();
        
            return MathsParser.ParseAlgebraicExpression(
                operandParser,
                subOperationStart, subOperationEnd,
                [
                    new Operator<string>(OperatorKind.Infix, "+", 10),
                    new Operator<string>(OperatorKind.Infix, "-", 10),
                    new Operator<string>(OperatorKind.Infix, "*", 20),
                    new Operator<string>(OperatorKind.Infix, "/", 20),
                ]).Select(o => new ComputedExpressionNode(o));            
        }
    }
    
    public static readonly IParser<ValueExpressionNode> ValueAccessExpression = 
        ScalarValueParser.Select(x => new LiteralExpressionNode.ConstantValueExpressionNode(x)) | 
        LiteralExpressionNodeParser.Cast<LiteralExpressionNode, ValueExpressionNode>();
    
    public static readonly IParser<ValueExpressionNode> ValueExpression = 
        OperationsParser.Cast<ComputedExpressionNode, ValueExpressionNode>()
        | FunctionsNodesParser.FunctionCall
        | ValueAccessExpression;
    
}