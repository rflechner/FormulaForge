using EasyParsing;
using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Tests.DomainSpecificLanguage;

public class StatementNodeParserTests
{
    [Fact]
    public void AssignmentParserWithConstantInt_ShouldSuccess()
    {
        var parser = StatementsParser.AssignmentParser;
        
        IParsingResult<StatementNode.VariableAssignmentExpressionNode> result = parser.Parse("toto = 234");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result);
        
        var variableAssignment = result.Result;
        
        Assert.Equal("toto", variableAssignment.Variable.Name);
        Assert.IsType<LiteralExpressionNode.ConstantValueExpressionNode>(variableAssignment.Value);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(234), ((LiteralExpressionNode.ConstantValueExpressionNode)variableAssignment.Value).Value);
    }
    
    [Fact]
    public void AssignmentParserWithVariable_ShouldSuccess()
    {
        var parser = StatementsParser.AssignmentParser;
        
        IParsingResult<StatementNode.VariableAssignmentExpressionNode> result = parser.Parse("toto = tata");
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        
        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result);
        
        var variableAssignment = result.Result;
        
        Assert.Equal("toto", variableAssignment.Variable.Name);
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(variableAssignment.Value);
        Assert.Equal(new VariableName(default, "tata").IgnorePosition(), ((LiteralExpressionNode.VariableValueExpressionNode)variableAssignment.Value).VariableName.IgnorePosition());
    }

    [Fact]
    public void AssignmentParser_ShouldParseComplexExpressionWithFunctionAndVariable()
    {
        var parser = StatementsParser.AssignmentParser;

        var text = "total_count = 43 + add(29, 34.2) * other_var";

        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result);
        var assignment = result.Result;

        // Vérifier le nom de variable assignée
        Assert.Equal("total_count", assignment.Variable.Name);

        // Vérifier que la valeur est une expression calculée
        Assert.IsType<ComputedExpressionNode>(assignment.Value);
        var computedExpr = (ComputedExpressionNode)assignment.Value;

        // L'expression : 43 + add(29, 34.2) * other_var
        // Structure: BinaryOperation(+) avec
        //   - Left: 43
        //   - Right: BinaryOperation(*) avec Left=add(29, 34.2), Right=other_var

        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>>(computedExpr.Expression);
        var rootOp = (EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>)computedExpr.Expression;

        // Vérifier l'opérateur racine (+)
        Assert.Equal("+", rootOp.Operator.Text);

        // Vérifier l'opérande gauche : 43
        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>>(rootOp.Left);
        var left43 = ((EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>)rootOp.Left).Value;
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(default, new ScalarValueNode.IntegerScalarValue(43)).IgnorePosition(), left43.IgnorePosition());

        // Vérifier l'opérande droite : add(29, 34.2) * other_var
        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>>(rootOp.Right);
        var multiplyOp = (EasyParsing.Parsers.Maths.BinaryOperation<ValueExpressionNode>)rootOp.Right;
        Assert.Equal("*", multiplyOp.Operator.Text);

        // Vérifier add(29, 34.2)
        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>>(multiplyOp.Left);
        var addFuncValue = ((EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>)multiplyOp.Left).Value;
        Assert.IsType<FunctionCallExpressionNode>(addFuncValue);
        var addFunc = (FunctionCallExpressionNode)addFuncValue;
        Assert.Equal("add", addFunc.FunctionName);
        Assert.Equal(2, addFunc.Arguments.Length);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(default, new ScalarValueNode.IntegerScalarValue(29)).IgnorePosition(), addFunc.Arguments[0].IgnorePosition());
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(default, new ScalarValueNode.DecimalScalarValue(34.2m)).IgnorePosition(), addFunc.Arguments[1].IgnorePosition());

        // Vérifier other_var
        Assert.IsType<EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>>(multiplyOp.Right);
        var otherVarValue = ((EasyParsing.Parsers.Maths.BinaryOperationOperandValue<ValueExpressionNode>)multiplyOp.Right).Value;
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(otherVarValue);
        var otherVar = (LiteralExpressionNode.VariableValueExpressionNode)otherVarValue;
        Assert.Equal("other_var", otherVar.VariableName.Name);
    }

    [Fact]
    public void AssignmentParser_ShouldParseFunctionCallAssignment()
    {
        var parser = StatementsParser.AssignmentParser;

        var text = "x = add(1,2)";

        var result = parser.Parse(text);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result);
        var assignment = result.Result;

        // Vérifier le nom de variable assignée
        Assert.Equal("x", assignment.Variable.Name);

        // Vérifier que la valeur est un appel de fonction
        Assert.IsType<FunctionCallExpressionNode>(assignment.Value);
        var funcCall = (FunctionCallExpressionNode)assignment.Value;

        // Vérifier le nom de la fonction et les arguments
        Assert.Equal("add", funcCall.FunctionName);
        Assert.Equal(2, funcCall.Arguments.Length);
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(default, new ScalarValueNode.IntegerScalarValue(1)).IgnorePosition(), funcCall.Arguments[0].IgnorePosition());
        Assert.Equal(new LiteralExpressionNode.ConstantValueExpressionNode(default, new ScalarValueNode.IntegerScalarValue(2)).IgnorePosition(), funcCall.Arguments[1].IgnorePosition());
    }
}