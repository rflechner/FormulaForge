using EasyParsing.Parsers.Maths;
using FormulaForge.Engine.Contexts;
using FormulaForge.Engine.DomainSpecificLanguage;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;

namespace FormulaForge.Engine;

public sealed class ScriptRunner(IDslContext context)
{
    public CodeRunResult Run(string code)
    {
        var parsingResult = FormulaProgramScriptParser.ProgramParser.Parse(code);
        if (!parsingResult.Success) throw new Exception("Failed to parse script", new Exception(parsingResult.FailureMessage));

        var astNodes = parsingResult.Result!;

        foreach (var node in astNodes)
        {
            var result = CodeRunResult.Success;

            switch (node)
            {
                case StatementNode.FunctionDeclarationNode functionDeclarationNode:
                    break;
                case StatementNode.VariableAssignmentExpressionNode variableAssignment:
                    var variableName = variableAssignment.Variable.Name;
                    result = RunVariableAssignment(variableAssignment, result, variableName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(node));
            }
            
            if (result != CodeRunResult.Success)
                return result;
        }
        
        return CodeRunResult.Success;
    }

    private CodeRunResult RunVariableAssignment(StatementNode.VariableAssignmentExpressionNode variableAssignment, CodeRunResult result, string variableName)
    {
        switch (variableAssignment.Value)
        {
            case ComputedExpressionNode computedExpressionNode:
                var value = Evaluate(computedExpressionNode.Expression);
                result = context.TrySetScalar(variableName, value);
                break;
            case FunctionCallExpressionNode functionCallExpressionNode:
                break;
            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                result = context.TrySetScalar(variableName, constantValueExpressionNode.Value);
                break;
            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                break;
            case LiteralExpressionNode literalExpressionNode:

                switch (literalExpressionNode)
                {
                    case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                        result = context.TrySetScalar(variableName, constantValueExpressionNode.Value);
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

    private ScalarValueNode Evaluate(BinaryOperationOperand<ValueExpressionNode> operand)
    {
        switch (operand)
        {
            case BinaryOperationOperandValue<ValueExpressionNode> binaryOperationOperandValue:

                switch (binaryOperationOperandValue.Value)
                {
                    case ComputedExpressionNode computedExpressionNode:
                        break;
                    case FunctionCallExpressionNode functionCallExpressionNode:
                        break;
                    case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                        return constantValueExpressionNode.Value;
                    case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                        break;
                    case LiteralExpressionNode literalExpressionNode:
                        switch (literalExpressionNode)
                        {
                            case LiteralExpressionNode.ConstantValueExpressionNode constantValueExpressionNode:
                                return constantValueExpressionNode.Value;
                            case LiteralExpressionNode.VariableValueExpressionNode variableValueExpressionNode:
                                if (!context.TryGetScalar(variableValueExpressionNode.VariableName.Name, out var value) || value == null)
                                    throw new Exception($"Variable {variableValueExpressionNode.VariableName.Name} not found");
                                return value;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(literalExpressionNode));
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                break;
            case BinaryOperation<ValueExpressionNode> binaryOperation:
                return binaryOperation.Operator.Text switch
                {
                    "+" => Add(Evaluate(binaryOperation.Left), Evaluate(binaryOperation.Right)),
                    "-" => Subtract(Evaluate(binaryOperation.Left), Evaluate(binaryOperation.Right)),
                    "*" => Multiply(Evaluate(binaryOperation.Left), Evaluate(binaryOperation.Right)),
                    "/" => Divide(Evaluate(binaryOperation.Left), Evaluate(binaryOperation.Right)),
                    _ => throw new ArgumentOutOfRangeException(nameof(binaryOperation))
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(operand));
        }
        
        throw new Exception("Operand not handled.");
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