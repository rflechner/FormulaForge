using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FormulaForge.Web.Components.Shared;

public partial class ComputationGrid
{
    [Parameter] public List<DateTime> Months { get; set; } = new();
    [Parameter] public List<RowSeriesData> Series { get; set; } = new();
    [Parameter] public List<RowScalarData> Scalars { get; set; } = new();
    private string? _editingCellId;
    private ElementReference _activeInput;

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

    private decimal GetValue(RowSeriesData rowSeries, DateTime month) =>
        rowSeries.Values.GetValueOrDefault(month.Month, 0);

    private void StartEdit(string cellId)
    {
        _editingCellId = cellId;

        StateHasChanged();
    }

    private void StopEdit() => _editingCellId = null;

    private void UpdateValue(RowSeriesData rowSeries, DateTime month, object? value)
    {
        if (decimal.TryParse(value?.ToString(), out decimal res))
        {
            rowSeries.Values[month.Month] = res;
        }
    }

    private string FormatValue(decimal val) => val == 0 ? "-" : val.ToString("N2");

    private string FormatScalarValue(ScalarValueNode scalar)
    {
        switch (scalar)
        {
            case ScalarValueNode.BooleanScalarValue booleanScalarValue:
                return booleanScalarValue.Value.ToString();
            case ScalarValueNode.DecimalScalarValue decimalScalarValue:
                return decimalScalarValue.Value.ToString("N2");
            case ScalarValueNode.IntegerScalarValue integerScalarValue:
                return integerScalarValue.Value.ToString();
            default:
                throw new ArgumentOutOfRangeException(nameof(scalar));
        }
        
    }

    private void HandleKeyDown(KeyboardEventArgs e, RowSeriesData rowSeries, DateTime month)
    {
        if (e.Key == "Enter") StopEdit();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_editingCellId != null && _activeInput.Context != null)
        {
            await _activeInput.FocusAsync();
        }
    }
}