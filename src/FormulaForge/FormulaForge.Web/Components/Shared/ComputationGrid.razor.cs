using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using Microsoft.AspNetCore.Components;

namespace FormulaForge.Web.Components.Shared;

public partial class ComputationGrid
{
    [Parameter] public List<DateTime> Months { get; set; } = new();
    [Parameter] public List<RowSeriesData> Series { get; set; } = new();
    [Parameter] public List<RowScalarData> Scalars { get; set; } = new();

    public class RowScalarData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required ScalarValueNode ScalarValue { get; set; }
    }

    public class RowSeriesData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required Dictionary<int, decimal> Values { get; set; } = new();
    }

    private decimal GetValue(RowSeriesData row, int index) =>
        row.Values.GetValueOrDefault(index, 0);

    private string FormatValue(decimal val) => val == 0 ? "—" : val.ToString("N2");

    private string FormatScalarValue(ScalarValueNode scalar) => scalar switch
    {
        ScalarValueNode.BooleanScalarValue b => b.Value.ToString(),
        ScalarValueNode.DecimalScalarValue d => d.Value.ToString("N2"),
        ScalarValueNode.IntegerScalarValue i => i.Value.ToString(),
        _ => "?"
    };
}
