using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Tests;

public class FormulaProgramScriptParserTests
{
    [Fact]
    public void FunctionCallWithoutParameters_ShouldParseFunctionCalls()
    {
        var code = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "DomainSpecificLanguage", "FormulaProgramScriptParserTests_program1.formula"));
        var parser = FormulaProgramScriptParser.ProgramParser;

        var result = parser.Parse(code);

        Assert.True(result.Success);
        Assert.NotNull(result.Result);

        Assert.Equal(3, result.Result.Length);

        // add(a, b) = a + b
        Assert.IsType<StatementNode.FunctionDeclarationNode>(result.Result[0]);
        var functionDecl = (StatementNode.FunctionDeclarationNode)result.Result[0];
        Assert.Equal("add", functionDecl.FunctionName);
        Assert.Equal(2, functionDecl.Parameters.Length);
        Assert.Equal("a", functionDecl.Parameters[0].Name);
        Assert.Equal("b", functionDecl.Parameters[1].Name);
        Assert.IsType<ComputedExpressionNode>(functionDecl.Body);

        // x = add(1,2)
        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result[1]);
        var assignment1 = (StatementNode.VariableAssignmentExpressionNode)result.Result[1];
        Assert.Equal("x", assignment1.Variable.Name);
        Assert.IsType<FunctionCallExpressionNode>(assignment1.Value);
        var funcCall1 = (FunctionCallExpressionNode)assignment1.Value;
        Assert.Equal("add", funcCall1.FunctionName);
        Assert.Equal(2, funcCall1.Arguments.Length);
        Assert.IsType<LiteralExpressionNode.ConstantValueExpressionNode>(funcCall1.Arguments[0]);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(1), ((LiteralExpressionNode.ConstantValueExpressionNode)funcCall1.Arguments[0]).Value);
        Assert.IsType<LiteralExpressionNode.ConstantValueExpressionNode>(funcCall1.Arguments[1]);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(2), ((LiteralExpressionNode.ConstantValueExpressionNode)funcCall1.Arguments[1]).Value);

        // y = mul(x,3)
        Assert.IsType<StatementNode.VariableAssignmentExpressionNode>(result.Result[2]);
        var assignment2 = (StatementNode.VariableAssignmentExpressionNode)result.Result[2];
        Assert.Equal("y", assignment2.Variable.Name);
        Assert.IsType<FunctionCallExpressionNode>(assignment2.Value);
        var funcCall2 = (FunctionCallExpressionNode)assignment2.Value;
        Assert.Equal("mul", funcCall2.FunctionName);
        Assert.Equal(2, funcCall2.Arguments.Length);
        Assert.IsType<LiteralExpressionNode.VariableValueExpressionNode>(funcCall2.Arguments[0]);
        Assert.Equal("x", ((LiteralExpressionNode.VariableValueExpressionNode)funcCall2.Arguments[0]).VariableName.Name);
        Assert.IsType<LiteralExpressionNode.ConstantValueExpressionNode>(funcCall2.Arguments[1]);
        Assert.Equal(new ScalarValueNode.IntegerScalarValue(3), ((LiteralExpressionNode.ConstantValueExpressionNode)funcCall2.Arguments[1]).Value);
    }
}