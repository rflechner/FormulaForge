using FormulaForge.Engine.Contexts;

namespace FormulaForge.Web.Contexts;

public class DynamicDataContext
{
    
}

public sealed class DynamicDslContext(DynamicDataContext dataContext) : DslContextBase
{
    protected override Scope CreateGlobalScope()
    {
        return new Scope();
    }
}
