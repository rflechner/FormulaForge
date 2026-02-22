using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Runtime;

public record FunctionRegistryId(string Name, int ParameterCount);

public sealed class ScriptInterpreter(IDslContext context)
{
    public CodeRunResult Run(string code)
    {
        var parsingResult = FormulaProgramScriptParser.ProgramParser.Parse(code);
        if (!parsingResult.Success) throw new Exception("Failed to parse script", new Exception(parsingResult.FailureMessage));

        var astNodes = parsingResult.Result!;

        foreach (var node in astNodes)
        {
            if (node is not StatementNode statement) continue;
            var result = ProcessStatement(statement);
            if (result != CodeRunResult.Success)
                return result;
        }
        
        return CodeRunResult.Success;
    }

    public CodeRunResult ProcessStatement(StatementNode statement)
    {
        return ProcessStatement(context.GlobalScope, statement);
    }
    
    private CodeRunResult ProcessStatement(Scope scope, StatementNode statement)
    {
        CodeRunResult result;

        switch (statement)
        {
            case StatementNode.FunctionDeclarationNode functionDeclarationNode:
                var functionId = new FunctionRegistryId(functionDeclarationNode.FunctionName, functionDeclarationNode.Parameters.Length);
                result = context.RegisterFunction(functionId, functionDeclarationNode);
                break;
            case StatementNode.VariableAssignmentExpressionNode variableAssignment:
                var variableName = variableAssignment.Variable.Name;
                result = RunVariableAssignment(scope, variableAssignment, variableName);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statement));
        }
        
        return result;
    }

    private CodeRunResult RunVariableAssignment(Scope scope, StatementNode.VariableAssignmentExpressionNode variableAssignment, string variableName)
    {
        var result = CodeRunResult.Success;
        RuntimeVariableValue? value = null;
        
        switch (variableAssignment.Value)
        {
            case ComputedExpressionNode computedExpressionNode:
                value = Evaluate(scope, computedExpressionNode.Expression);
                break;
            case FunctionCallExpressionNode functionCallExpressionNode:
                result = CallFunction(scope, functionCallExpressionNode, out value);
                if (result != CodeRunResult.Success) return result;
                break;
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                value = new RuntimeVariableValue.RuntimeScalarValue(constantValueExpressionNode.Value);
                break;
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                result = scope.TryGetScalar(variableValueExpressionNode.VariableName.Name, out value) ? CodeRunResult.Success : CodeRunResult.VariableNotFound;
                break;
            case LiteralExpressionNode literalExpressionNode:

                switch (literalExpressionNode)
                {
                    case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                        result = scope.TrySetScalar(variableName, new RuntimeVariableValue.RuntimeScalarValue(constantValueExpressionNode.Value));
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
        
        if (value != null)
            return scope.TrySetScalar(variableName, value);

        return result;
    }

    private CodeRunResult CallFunction(Scope scope, FunctionCallExpressionNode functionCallExpressionNode, out RuntimeVariableValue? result)
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

    private RuntimeVariableValue Evaluate(Scope scope, BinaryOperationOperand<ValueExpressionNode> operand)
    {
        switch (operand)
        {
            case BinaryOperationOperandValue<ValueExpressionNode> binaryOperationOperandValue:

                var valueExpressionNode = binaryOperationOperandValue.Value;
                
                if (!TryEvaluateValueExpression(scope, valueExpressionNode, out var result) || result == null)
                    throw new Exception("Failed to evaluate operand value");
                
                return result;

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

    private bool TryEvaluateValueExpression(Scope scope, ValueExpressionNode valueExpressionNode, out RuntimeVariableValue? result)
    {
        switch (valueExpressionNode)
        {
            case ComputedExpressionNode computedExpressionNode:
                result = Evaluate(scope, computedExpressionNode.Expression);
                return true;
            
            case FunctionCallExpressionNode functionCallExpressionNode:
                var codeRunResult = CallFunction(scope, functionCallExpressionNode, out result);
                return codeRunResult == CodeRunResult.Success;
            
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                result = new RuntimeVariableValue.RuntimeScalarValue(constantValueExpressionNode.Value);
                return true;
            
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                if (!scope.TryGetScalar(variableValueExpressionNode.VariableName.Name, out var value) || value == null)
                    throw new Exception($"Variable {variableValueExpressionNode.VariableName.Name} not found");
                result = value;
                return true;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private RuntimeVariableValue Add(RuntimeVariableValue a, RuntimeVariableValue b) =>
        (a, b) switch
        {
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                new RuntimeVariableValue.RuntimeScalarValue(Add(scalarA.Value, scalarB.Value)),

            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Add(complexA, complexB),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                Add(scalarB, complexA),
            
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Add(scalarA, complexB),

            _ => throw new Exception("Unsupported types for addition: " + a.GetType() + ", " + b.GetType() + "")
        };

    private ScalarValueNode Add(ScalarValueNode a, ScalarValueNode b)
    {
        switch (a, b)
        {
            case (ScalarValueNode.BooleanScalarValue aBool, ScalarValueNode.BooleanScalarValue bBool):
                return (new ScalarValueNode.BooleanScalarValue(aBool.Value || bBool.Value));
            
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

    private RuntimeVariableValue Subtract(RuntimeVariableValue a, RuntimeVariableValue b) =>
        (a, b) switch
        {
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                new RuntimeVariableValue.RuntimeScalarValue(Subtract(scalarA.Value, scalarB.Value)),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Subtract(complexA, complexB),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                Subtract(scalarB, complexA),
            
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Subtract(scalarA, complexB),

            _ => throw new Exception("Unsupported types for subtraction: " + a.GetType() + ", " + b.GetType() + "")
        };

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
                throw new Exception("Unsupported types for subtraction: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

    private RuntimeVariableValue Multiply(RuntimeVariableValue a, RuntimeVariableValue b) =>
        (a, b) switch
        {
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeScalarValue scalarB) => 
                new RuntimeVariableValue.RuntimeScalarValue(Multiply(scalarA.Value, scalarB.Value)),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Multiply(complexA, complexB),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                Multiply(scalarB, complexA),
            
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Multiply(scalarA, complexB),
            
            _ => throw new Exception("Unsupported types for multiplication: " + a.GetType() + ", " + b.GetType() + "")
        };

    private RuntimeVariableValue Multiply(RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB)
    {
        var timeSeriesValues = complexB.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode>(t.Period, new ScalarValueNode.DecimalScalarValue(AsDecimal(t.Value).Value * AsDecimal(scalarA.Value).Value)))
            .CreateTimeSeries();
        
        return new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSeriesValues);
    }
    
    private RuntimeVariableValue Multiply(RuntimeVariableValue.RuntimeTimeSeriesValue a, RuntimeVariableValue.RuntimeTimeSeriesValue b)
    {
        return ComputeTimeSeries(a, b, (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value * newVal.Value.Value));
    }
    
    private RuntimeVariableValue Add(RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB)
    {
        var timeSeriesValues = complexB.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode>(t.Period, new ScalarValueNode.DecimalScalarValue(AsDecimal(t.Value).Value + AsDecimal(scalarA.Value).Value)))
            .CreateTimeSeries();
        
        return new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSeriesValues);
    }
    
    private RuntimeVariableValue Subtract(RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB)
    {
        var timeSeriesValues = complexB.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode>(t.Period, new ScalarValueNode.DecimalScalarValue(AsDecimal(t.Value).Value - AsDecimal(scalarA.Value).Value)))
            .CreateTimeSeries();
        
        return new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSeriesValues);
    }
    
    private RuntimeVariableValue Add(RuntimeVariableValue.RuntimeTimeSeriesValue a, RuntimeVariableValue.RuntimeTimeSeriesValue b)
    {
        return ComputeTimeSeries(a, b, 
            (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value + newVal.Value.Value));
    }
    
    private RuntimeVariableValue Subtract(RuntimeVariableValue.RuntimeTimeSeriesValue a, RuntimeVariableValue.RuntimeTimeSeriesValue b)
    {
        return ComputeTimeSeries(a, b, 
            (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value - newVal.Value.Value));
    }
    
    private RuntimeVariableValue Divide(RuntimeVariableValue.RuntimeTimeSeriesValue a, RuntimeVariableValue.RuntimeTimeSeriesValue b)
    {
        return ComputeTimeSeries(a, b, 
            (oldVal, newVal) => new ScalarValueNode.DecimalScalarValue(oldVal.Value.Value / newVal.Value.Value));
    }

    private RuntimeVariableValue ComputeTimeSeries(RuntimeVariableValue.RuntimeTimeSeriesValue a, RuntimeVariableValue.RuntimeTimeSeriesValue b, Func<TimeSeriesValue<ScalarValueNode.DecimalScalarValue>, TimeSeriesValue<ScalarValueNode.DecimalScalarValue>, ScalarValueNode.DecimalScalarValue> compute)
    {
        var x = a.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(t.Period, AsDecimal(t.Value)))
            .CreateTimeSeries();
        
        var y = b.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(t.Period, AsDecimal(t.Value)))
            .CreateTimeSeries();
        
        TimeSeries<ScalarValueNode.DecimalScalarValue>[] sources = [x, y];
        var timeSeriesValues = sources
            .Aggregate(compute)
            .Cast<ScalarValueNode, ScalarValueNode.DecimalScalarValue>();
        
        return new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSeriesValues);
    }

    private ScalarValueNode.DecimalScalarValue AsDecimal(ScalarValueNode value) =>
        value switch
        {
            ScalarValueNode.BooleanScalarValue booleanScalarValue => booleanScalarValue.Value
                ? new ScalarValueNode.DecimalScalarValue(1)
                : new ScalarValueNode.DecimalScalarValue(0),
            ScalarValueNode.DecimalScalarValue decimalScalarValue => decimalScalarValue,
            ScalarValueNode.IntegerScalarValue integerScalarValue => new ScalarValueNode.DecimalScalarValue(
                integerScalarValue.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };

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
                throw new Exception("Unsupported types for multiplication: " + a.GetType() + ", " + b.GetType() + "");
        }
    }

    private RuntimeVariableValue Divide(RuntimeVariableValue a, RuntimeVariableValue b) =>
        (a, b) switch
        {
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeScalarValue scalarB) => 
                new RuntimeVariableValue.RuntimeScalarValue(Divide(scalarA.Value, scalarB.Value)),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Divide(complexA, complexB),
            
            (RuntimeVariableValue.RuntimeTimeSeriesValue complexA, RuntimeVariableValue.RuntimeScalarValue scalarB) =>
                Divide(scalarB, complexA),
            
            (RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB) =>
                Divide(scalarA, complexB),

            _ => throw new Exception("Unsupported types for multiplication: " + a.GetType() + ", " + b.GetType() + "")
        };
    
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
                throw new Exception("Unsupported types for division: " + a.GetType() + ", " + b.GetType() + "");
        }
    }
    
    private RuntimeVariableValue Divide(RuntimeVariableValue.RuntimeScalarValue scalarA, RuntimeVariableValue.RuntimeTimeSeriesValue complexB)
    {
        var timeSeriesValues = complexB.Value
            .Select(t => new TimeSeriesValue<ScalarValueNode>(t.Period, new ScalarValueNode.DecimalScalarValue(AsDecimal(t.Value).Value / AsDecimal(scalarA.Value).Value)))
            .CreateTimeSeries();
        
        return new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSeriesValues);
    }

}