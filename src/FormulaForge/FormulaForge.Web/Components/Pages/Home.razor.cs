using BlazorMonaco.Editor;
using FormulaForge.Domain.Entities;
using FormulaForge.Domain.Services;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;
using FormulaForge.Web.Components.Shared;
using FormulaForge.Web.Contexts;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Icons.Regular;
using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons;

namespace FormulaForge.Web.Components.Pages;

public partial class Home
{
    private List<DateTime> _months = new();
    private readonly List<ExcelGrid.RowData> _inputsRows = new();
    private readonly List<ExcelGrid.RowData> _outputsRows = new();
    private readonly DynamicDslContext _dslContext = new(new DynamicDataContext());
    private ScriptInterpreter? _interpreter;
    private StandaloneCodeEditor? _editor;
    public CodeRunResult? ScriptRunResult { get; set; }
    
    public string ProjectName { get; set; } = "Untitled";
    
    public Project? Project { get; set; }
    
    [Inject] public required IDialogService DialogService { get; init; }
    
    [Inject] public required IProjectManagementService ProjectManagement { get; init; }
    
    [Parameter] public Guid? ProjectId { get; set; }

    private static StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            Language = "bash",
            AutomaticLayout = true,
            Value = string.Empty
        };
    }

    protected override async Task OnInitializedAsync()
    {
        var period = new Period(new DateTime(2024, 1, 1), new DateTime(2027, 1, 1));
        var months = period.GetMonths().ToArray();
        _months = months.Select(m => m.InclusiveStart.DateTime).ToList();

        _dslContext.GlobalScope.Variables.Add("x",
            new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(10)));

        var random = new Random();
        var balance = new TimeSeries<ScalarValueNode>();
        var changeRate = new TimeSeries<ScalarValueNode>();

        for (var index = 0; index < months.Length; index++)
        {
            var month = months[index];
            balance.Add(month, new ScalarValueNode.IntegerScalarValue(index * 100), (a, b) => a.Value);
            changeRate.Add(month, new ScalarValueNode.DecimalScalarValue((decimal)random.NextDouble()),
                (a, b) => a.Value);
        }

        _dslContext.GlobalScope.Variables.Add("balance", new RuntimeVariableValue.RuntimeTimeSeriesValue(balance));
        _dslContext.GlobalScope.Variables.Add("change_rate",
            new RuntimeVariableValue.RuntimeTimeSeriesValue(changeRate));

        await LoadRows();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        await LoadRows();
    }

    private async Task LoadRows()
    {
        _inputsRows.Clear();
        _outputsRows.Clear();

        foreach (var (variableName, variableValue) in _dslContext.GlobalScope.Variables)
        {
            _outputsRows.Add(new ExcelGrid.RowData
            {
                Name = variableName,
                Values = Map(variableValue)
            });
        }

        StateHasChanged();
    }

    private async Task RunScript()
    {
        if (_editor == null)
        {
            StateHasChanged();
            return;
        }

        try
        {
            _interpreter = new ScriptInterpreter(_dslContext);

            var code = await _editor.GetValue();
            ScriptRunResult = _interpreter.Run(code);

            if (ScriptRunResult != null && ScriptRunResult != CodeRunResult.Success)
            {
                await DialogService.ShowErrorAsync(ScriptRunResult?.ToString() ?? "", "Error running script");
                StateHasChanged();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        await LoadRows();
    }

    private Dictionary<int, decimal> Map(RuntimeVariableValue variableValue)
    {
        switch (variableValue)
        {
            case RuntimeVariableValue.RuntimeScalarValue runtimeScalarValue:
                var value = Map(runtimeScalarValue.Value);
                return new Dictionary<int, decimal> { { 0, value } };

            case RuntimeVariableValue.RuntimeTimeSeriesValue runtimeTimeSeriesValue:
                return runtimeTimeSeriesValue.Value
                    .Select((v, i) => new
                    {
                        Value = Map(v.Value),
                        Index = i
                    })
                    .ToDictionary(v => v.Index, v => v.Value);

            default:
                throw new ArgumentOutOfRangeException(nameof(variableValue));
        }
    }

    private static decimal Map(ScalarValueNode scalarValue) =>
        scalarValue switch
        {
            ScalarValueNode.BooleanScalarValue booleanScalarValue => booleanScalarValue.Value ? 1 : 0,
            ScalarValueNode.DecimalScalarValue decimalScalarValue => decimalScalarValue.Value,
            ScalarValueNode.IntegerScalarValue integerScalarValue => integerScalarValue.Value,
            _ => throw new ArgumentOutOfRangeException()
        };
    
    private static Icon PlayIcon(bool active = false) =>
        active ? new Size20.PlaySettings()
            : new Icons.Regular.Size20.PlaySettings();
    
    private static Icon SaveIcon(bool active = false) =>
        active ? new Size20.Save()
            : new Icons.Regular.Size20.Save();

    private async Task SaveProject()
    {
        if (string.IsNullOrWhiteSpace(ProjectName))
        {
            await DialogService.ShowErrorAsync("Project name cannot be empty", "Error saving project");
            return;
        }

        if (Project == null)
        {
            Project = new Project
            {
                Name = ProjectName
            };
            await ProjectManagement.CreateProjectAsync(Project);
        }
        else
            await ProjectManagement.UpdateProjectAsync(Project);
    }
}