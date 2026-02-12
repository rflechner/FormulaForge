using EasyParsing;
using EasyParsing.Dsl;
using EasyParsing.Dsl.Linq;
using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Ast;

namespace FormulaForge.Engine.Tests;

public class ValueExpressionNodeParserTests
{
    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseAddOperationsOfTreeIntegers()
    {
        var parser = ValueExpressionNodeParser.ValueExpression;

        var text = "(1 + 25) * 589";
        
        var result = parser.Parse(text);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<ComputedExpressionNode>(result.Result);
        var computedExpression = (ComputedExpressionNode)result.Result;

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(computedExpression.Expression);
        var rootOperation = (BinaryOperation<ValueExpressionNode>)computedExpression.Expression;

        Assert.Equal("*", rootOperation.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(rootOperation.Left);
        var leftOperation = (BinaryOperation<ValueExpressionNode>)rootOperation.Left;
        Assert.Equal("+", leftOperation.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Left);
        var leftLeft = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Left;
        var value1 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1));
        Assert.Equal(value1, leftLeft.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftOperation.Right);
        var leftRight = (BinaryOperationOperandValue<ValueExpressionNode>)leftOperation.Right;
        var value25 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(25));
        Assert.Equal(value25, leftRight.Value);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(rootOperation.Right);
        var right = (BinaryOperationOperandValue<ValueExpressionNode>)rootOperation.Right;
        var value589 = new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(589));
        Assert.Equal(value589, right.Value);
    }

    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseComplexExpressionWithFunctionCall()
    {
        var parser = ValueExpressionNodeParser.ValueExpression;

        var text = "(1 + 25) * 589 - add(1, 32)";

        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<ComputedExpressionNode>(result.Result);
        var computedExpression = (ComputedExpressionNode)result.Result;

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(computedExpression.Expression);
        var rootOperation = (BinaryOperation<ValueExpressionNode>)computedExpression.Expression;

        Assert.Equal("-", rootOperation.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(rootOperation.Left);
        var leftMultiply = (BinaryOperation<ValueExpressionNode>)rootOperation.Left;
        Assert.Equal("*", leftMultiply.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(leftMultiply.Left);
        var leftAdd = (BinaryOperation<ValueExpressionNode>)leftMultiply.Left;
        Assert.Equal("+", leftAdd.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftAdd.Left);
        var value1 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftAdd.Left).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1)), value1);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftAdd.Right);
        var value25 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftAdd.Right).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(25)), value25);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftMultiply.Right);
        var value589 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftMultiply.Right).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(589)), value589);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(rootOperation.Right);
        var rightValue = ((BinaryOperationOperandValue<ValueExpressionNode>)rootOperation.Right).Value;

        Assert.IsType<FunctionCallExpressionNode>(rightValue);
        var funcCall = (FunctionCallExpressionNode)rightValue;
        Assert.Equal("add", funcCall.FunctionName);
        Assert.Equal(2, funcCall.Arguments.Length);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1)), funcCall.Arguments[0]);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(32)), funcCall.Arguments[1]);
    }

    [Fact]
    public void BinaryOperationExpressionParser_ShouldParseNestedExpressionsWithVariablesAndFunctions()
    {
        var parser = ValueExpressionNodeParser.ValueExpression;

        var text = "(1 + 25) * 589 / add_numbers(count(collection), 6/2)";

        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<ComputedExpressionNode>(result.Result);
        var computedExpression = (ComputedExpressionNode)result.Result;

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(computedExpression.Expression);
        var rootOperation = (BinaryOperation<ValueExpressionNode>)computedExpression.Expression;

        Assert.Equal("/", rootOperation.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(rootOperation.Left);
        var leftMultiply = (BinaryOperation<ValueExpressionNode>)rootOperation.Left;
        Assert.Equal("*", leftMultiply.Operator.Text);

        Assert.IsType<BinaryOperation<ValueExpressionNode>>(leftMultiply.Left);
        var leftAdd = (BinaryOperation<ValueExpressionNode>)leftMultiply.Left;
        Assert.Equal("+", leftAdd.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftAdd.Left);
        var value1 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftAdd.Left).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(1)), value1);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftAdd.Right);
        var value25 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftAdd.Right).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(25)), value25);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(leftMultiply.Right);
        var value589 = ((BinaryOperationOperandValue<ValueExpressionNode>)leftMultiply.Right).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(589)), value589);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(rootOperation.Right);
        var rightValue = ((BinaryOperationOperandValue<ValueExpressionNode>)rootOperation.Right).Value;

        Assert.IsType<FunctionCallExpressionNode>(rightValue);
        var addNumbersFunc = (FunctionCallExpressionNode)rightValue;
        Assert.Equal("add_numbers", addNumbersFunc.FunctionName);
        Assert.Equal(2, addNumbersFunc.Arguments.Length);
        
        Assert.IsType<FunctionCallExpressionNode>(addNumbersFunc.Arguments[0]);
        var countFunc = (FunctionCallExpressionNode)addNumbersFunc.Arguments[0];
        Assert.Equal("count", countFunc.FunctionName);
        Assert.Single(countFunc.Arguments);
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(countFunc.Arguments[0]);
        var collectionVar = (LiteralExpressionNode.VariableValueExpressionNode)countFunc.Arguments[0];
        Assert.Equal("collection", collectionVar.VariableName);

        Assert.IsType<ComputedExpressionNode>(addNumbersFunc.Arguments[1]);
        var divisionExpr = (ComputedExpressionNode)addNumbersFunc.Arguments[1];
        Assert.IsType<BinaryOperation<ValueExpressionNode>>(divisionExpr.Expression);
        var divisionOp = (BinaryOperation<ValueExpressionNode>)divisionExpr.Expression;
        Assert.Equal("/", divisionOp.Operator.Text);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(divisionOp.Left);
        var value6 = ((BinaryOperationOperandValue<ValueExpressionNode>)divisionOp.Left).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(6)), value6);

        Assert.IsType<BinaryOperationOperandValue<ValueExpressionNode>>(divisionOp.Right);
        var value2 = ((BinaryOperationOperandValue<ValueExpressionNode>)divisionOp.Right).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(new ScalarValueNode.IntegerScalarValue(2)), value2);
    }


}