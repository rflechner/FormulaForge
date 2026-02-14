namespace FormulaForge.Engine.Tests;

public class FormulaProgramScriptParserTests
{
    [Fact]
    public void FunctionCallWithoutParameters_ShouldParseFunctionCalls()
    {
        var code = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "FormulaProgramScriptParserTests_program1.formula"));
        var parser = FormulaProgramScriptParser.ProgramParser;

        var result = parser.Parse(code);
        
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
    }
}