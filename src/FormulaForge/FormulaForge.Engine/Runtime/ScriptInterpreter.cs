using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine.Runtime;

public record FunctionRegistryId(string Name, int ParameterCount);

public sealed class ScriptInterpreter(IDslContext context)
{
    public CodeRunResult Run(string code)
    {
        var parsingResult = FormulaProgramScriptParser.ProgramParser.Parse(code);
        if (!parsingResult.Success) throw new Exception("Failed to parse script", new Exception(parsingResult.FailureMessage));

        var astNodes = parsingResult.Result!;

        foreach (var statement in astNodes)
        {
            var result = ProcessStatement(context.GlobalScope, statement);
            if (result != CodeRunResult.Success)
                return result;
        }
        
        return CodeRunResult.Success;
    }

    private CodeRunResult ProcessStatement(Scope scope, StatementNode statement)
    {
        var result = CodeRunResult.Success;

        switch (statement)
        {
            case StatementNode.FunctionDeclarationNode functionDeclarationNode:
                var functionId = new FunctionRegistryId(functionDeclarationNode.FunctionName, functionDeclarationNode.Parameters.Length);
                result = context.RegisterFunction(functionId, functionDeclarationNode);
                break;
            case StatementNode.VariableAssignmentExpressionNode variableAssignment:
                var variableName = variableAssignment.Variable.Name;
                result = RunVariableAssignment(scope, variableAssignment, result, variableName);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statement));
        }
        
        return result;
    }

    private CodeRunResult RunVariableAssignment(Scope scope, StatementNode.VariableAssignmentExpressionNode variableAssignment,
        CodeRunResult result, string variableName)
    {
        ScalarValueNode? value;
        
        switch (variableAssignment.Value)
        {
            case ComputedExpressionNode computedExpressionNode:
                value = Evaluate(scope, computedExpressionNode.Expression);
                result = scope.TrySetScalar(variableName, value);
                break;
            case FunctionCallExpressionNode functionCallExpressionNode:
                result = CallFunction(scope, functionCallExpressionNode, out value);
                if (result != CodeRunResult.Success) return result;
                result = scope.TrySetScalar(variableName, value!);
                break;
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                result = scope.TrySetScalar(variableName, constantValueExpressionNode.Value);
                break;
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                break;
            case LiteralExpressionNode literalExpressionNode:

                switch (literalExpressionNode)
                {
                    case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                        result = scope.TrySetScalar(variableName, constantValueExpressionNode.Value);
                        break;
                    case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(literalExpressionNode));
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    private CodeRunResult CallFunction(Scope scope, FunctionCallExpressionNode functionCallExpressionNode, out ScalarValueNode? result)
    {
        var functionId = new FunctionRegistryId(functionCallExpressionNode.FunctionName, functionCallExpressionNode.Arguments.Length);
        if (!context.TryGetFunction(functionId, out var function) || function == null)
        {
            result = null;
            return CodeRunResult.FunctionNotFound;
        }

        var childScope = scope.CreateChild();
        
        for (var i = 0; i < functionCallExpressionNode.Arguments.Length; i++)
        {
            var argument = functionCallExpressionNode.Arguments[i];
            if (!TryEvaluateValueExpression(scope, argument, out var argumentValue) || argumentValue == null)
                throw new Exception($"Failed to evaluate argument number {i} for function '{functionCallExpressionNode.FunctionName}' body");

            var parameter = function.Parameters[i];
            childScope.Variables[parameter.Name] = argumentValue;
        }

        if (!TryEvaluateValueExpression(childScope, function.Body, out result))
            throw new Exception($"Failed to evaluate function '{functionCallExpressionNode.FunctionName}' body");
        
        return CodeRunResult.Success;
    }

    private ScalarValueNode Evaluate(Scope scope, BinaryOperationOperand<ValueExpressionNode> operand)
    {
        switch (operand)
        {
            case BinaryOperationOperandValue<ValueExpressionNode> binaryOperationOperandValue:

                var valueExpressionNode = binaryOperationOperandValue.Value;
                if (TryEvaluateValueExpression(scope, valueExpressionNode, out var result)) return result!;
                throw new Exception("Failed to evaluate operand value");
                
            case BinaryOperation<ValueExpressionNode> binaryOperation:
                return binaryOperation.Operator.Text switch
                {
                    "+" => Add(Evaluate(scope, binaryOperation.Left), Evaluate(scope, binaryOperation.Right)),
                    "-" => Subtract(Evaluate(scope, binaryOperation.Left), Evaluate(scope, binaryOperation.Right)),
                    "*" => Multiply(Evaluate(scope, binaryOperation.Left), Evaluate(scope, binaryOperation.Right)),
                    "/" => Divide(Evaluate(scope, binaryOperation.Left), Evaluate(scope, binaryOperation.Right)),
                    _ => throw new ArgumentOutOfRangeException(nameof(binaryOperation))
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(operand));
        }
    }

    private bool TryEvaluateValueExpression(Scope scope, ValueExpressionNode valueExpressionNode, out ScalarValueNode? result)
    {
        switch (valueExpressionNode)
        {
            case ComputedExpressionNode computedExpressionNode:
                result = Evaluate(scope, computedExpressionNode.Expression);
                return true;
            
            case FunctionCallExpressionNode functionCallExpressionNode:
                break;
            
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                result = constantValueExpressionNode.Value;
                return true;
            
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                if (!scope.TryGetScalar(variableValueExpressionNode.VariableName.Name, out var value) || value == null)
                    throw new Exception($"Variable {variableValueExpressionNode.VariableName.Name} not found");
                result = value;
                return true;

            default:
                throw new ArgumentOutOfRangeException();
        }

        result = null;
        return false;
    }

    private ScalarValueNode Add(ScalarValueNode a, ScalarValueNode b)
    {
        switch (a, b)
        {
            case (ScalarValueNode.BooleanScalarValue aBool, ScalarValueNode.BooleanScalarValue bBool):
                return new ScalarValueNode.BooleanScalarValue(aBool.Value || bBool.Value);
            
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value + bDec.Value);
            
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.IntegerScalarValue(aInt.Value + bInt.Value);
            
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value + bInt.Value);
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aInt.Value + bDec.Value);
            
            default:
                throw new Exception("Unsupported types for addition: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

    private ScalarValueNode Subtract(ScalarValueNode a, ScalarValueNode b)
    {
        switch (a, b)
        {
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value - bDec.Value);
            
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.IntegerScalarValue(aInt.Value - bInt.Value);
            
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value - bInt.Value);
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aInt.Value - bDec.Value);
            
            default:
                throw new Exception("Unsupported types for addition: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

    private ScalarValueNode Multiply(ScalarValueNode a, ScalarValueNode b)
    {
        switch (a, b)
        {
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value * bDec.Value);
            
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.IntegerScalarValue(aInt.Value * bInt.Value);
            
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value * bInt.Value);
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aInt.Value * bDec.Value);
            
            default:
                throw new Exception("Unsupported types for addition: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

    private ScalarValueNode Divide(ScalarValueNode a, ScalarValueNode b)
    {
        switch (a, b)
        {
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value / bDec.Value);
            
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.IntegerScalarValue(aInt.Value / bInt.Value);
            
            case (ScalarValueNode.DecimalScalarValue aDec, ScalarValueNode.IntegerScalarValue bInt):
                return new ScalarValueNode.DecimalScalarValue(aDec.Value / bInt.Value);
            case (ScalarValueNode.IntegerScalarValue aInt, ScalarValueNode.DecimalScalarValue bDec):
                return new ScalarValueNode.DecimalScalarValue(aInt.Value / bDec.Value);
            
            default:
                throw new Exception("Unsupported types for addition: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

}