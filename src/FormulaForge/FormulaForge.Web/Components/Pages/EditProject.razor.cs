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

public partial class EditProject
{
    private List<DateTime> _months = new();
    private readonly List<ExcelGrid.RowData> _inputsRows = new();
    private readonly List<ExcelGrid.RowData> _outputsRows = new();
    private readonly DynamicDslContext _dslContext = new(new DynamicDataContext());
    private ScriptInterpreter? _interpreter;
    private MonacoEditor? _editor;
    private bool _ideInitialized = false;
    public CodeRunResult? ScriptRunResult { get; set; }
    
    public Project Project { get; set; } = new Project
    {
        Name = "Untitled project"
    };
    
    [Inject] public required IDialogService DialogService { get; init; }
    
    [Inject] public required IProjectManagementService ProjectManagement { get; init; }
    
    [Parameter] public Guid? ProjectId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (ProjectId.HasValue && await ProjectManagement.GetProjectAsync(ProjectId.Value) is { } project)
        {
            Project = project;

            if (_ideInitialized && _editor != null)
            {
                try
                {
                    await _editor.SetValue(project.Code);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (var scalarValue in project.BooleanScalarValues)
            {
                _dslContext.GlobalScope.Variables.Add(scalarValue.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.BooleanScalarValue(scalarValue.Value)));
            }

            foreach (var scalarValue in project.IntegerScalarValues)
            {
                _dslContext.GlobalScope.Variables.Add(scalarValue.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(scalarValue.Value)));
            }

            foreach (var scalarValue in project.DecimalScalarValues)
            {
                _dslContext.GlobalScope.Variables.Add(scalarValue.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.DecimalScalarValue(scalarValue.Value)));
            }
            
            foreach (var ts in project.IntegerTimeSeriesValues)
            {
                var timeSerie = ts.Entries.Select(e => new TimeSeriesValue<ScalarValueNode.IntegerScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.IntegerScalarValue(e.Value)))
                    .CreateTimeSeries()
                    .Cast<ScalarValueNode, ScalarValueNode.IntegerScalarValue>()
                    .CreateTimeSeries();
                _dslContext.GlobalScope.Variables.Add(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSerie));
            }

            foreach (var ts in project.DecimalTimeSeriesValues)
            {
                var timeSerie = ts.Entries.Select(e => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.DecimalScalarValue(e.Value)))
                    .CreateTimeSeries()
                    .Cast<ScalarValueNode, ScalarValueNode.DecimalScalarValue>()
                    .CreateTimeSeries();
                _dslContext.GlobalScope.Variables.Add(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSerie));
            }

            foreach (var ts in project.BooleanTimeSeriesValues)
            {
                var timeSerie = ts.Entries.Select(e => new TimeSeriesValue<ScalarValueNode.BooleanScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.BooleanScalarValue(e.Value)))
                    .CreateTimeSeries()
                    .Cast<ScalarValueNode, ScalarValueNode.BooleanScalarValue>()
                    .CreateTimeSeries();
                _dslContext.GlobalScope.Variables.Add(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(timeSerie));
            }
            
            _months = 
                project.IntegerTimeSeriesValues.SelectMany(m => m.Entries.Select(e => e.Start.DateTime))
                    .Concat(project.DecimalTimeSeriesValues.SelectMany(m => m.Entries.Select(e => e.Start.DateTime)))
                    .Concat(project.BooleanTimeSeriesValues.SelectMany(m => m.Entries.Select(e => e.Start.DateTime)))
                    .Distinct()
                    .OrderBy(m => m)
                    .ToList();
            
            await DisplayRows(clear: false);
            
            return;
        }
        
        Project = new Project
        {
            Name = "Untitled project"
        };
        
        await LoadInputs();

        await DisplayRows();
    }

    private Task LoadInputs()
    {
        var period = new Period(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc));
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
        _dslContext.GlobalScope.Variables.Add("change_rate", new RuntimeVariableValue.RuntimeTimeSeriesValue(changeRate));
        
        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        if (ProjectId.HasValue && _editor != null)
        {
            try
            {
                await _editor.SetValue(Project.Code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        await DisplayRows();
    }

    private async Task DisplayRows(bool clear = true)
    {
        if (clear)
        {
            _inputsRows.Clear();
            _outputsRows.Clear();
        }

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

        await DisplayRows();
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
        if (string.IsNullOrWhiteSpace(Project.Name))
        {
            await DialogService.ShowErrorAsync("Project name cannot be empty", "Error saving project");
            return;
        }

        if (_editor != null)
        {
            Project.Code = await _editor.GetValue();
        }

        Project.BooleanScalarValues.Clear();
        Project.IntegerScalarValues.Clear();
        Project.DecimalScalarValues.Clear();
        
        Project.DecimalTimeSeriesValues.Clear();
        Project.BooleanTimeSeriesValues.Clear();
        Project.IntegerTimeSeriesValues.Clear();

        foreach (var (key, value) in _dslContext.GlobalScope.Variables)
        {
            switch (value)
            {
                case RuntimeVariableValue.RuntimeScalarValue scalarValue:

                    switch (scalarValue.Value)
                    {
                        case ScalarValueNode.BooleanScalarValue booleanScalarValue:
                            Project.BooleanScalarValues.Add(new BooleanScalarValueEntity { Key = key, Value = booleanScalarValue.Value });
                            break;
                        case ScalarValueNode.DecimalScalarValue decimalScalarValue:
                            Project.DecimalScalarValues.Add(new DecimalScalarValueEntity { Key = key, Value = decimalScalarValue.Value });
                            break;
                        case ScalarValueNode.IntegerScalarValue integerScalarValue:
                            Project.IntegerScalarValues.Add(new IntegerScalarValueEntity { Key = key, Value = integerScalarValue.Value });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                
                case RuntimeVariableValue.RuntimeTimeSeriesValue timeSeriesValue:
                    
                    var firstValue = timeSeriesValue.Value.FirstOrDefault()?.Value;
                    if (firstValue == null) break;

                    switch (firstValue)
                    {
                        case ScalarValueNode.BooleanScalarValue:
                            Project.BooleanTimeSeriesValues.Add(new BooleanTimeSeriesEntity
                            {
                                Key = key,
                                Entries = timeSeriesValue.Value.Select(v => new BooleanTimeSeriesEntry
                                {
                                    Value = ((ScalarValueNode.BooleanScalarValue)v.Value).Value,
                                    Start = v.Period.InclusiveStart,
                                    End = v.Period.ExclusiveEnd
                                }).ToList()
                            });
                            break;
                        case ScalarValueNode.DecimalScalarValue:
                            Project.DecimalTimeSeriesValues.Add(new DecimalTimeSeriesEntity
                            {
                                Key = key,
                                Entries = timeSeriesValue.Value.Select(v => new DecimalTimeSeriesEntry
                                {
                                    Value = ((ScalarValueNode.DecimalScalarValue)v.Value).Value,
                                    Start = v.Period.InclusiveStart,
                                    End = v.Period.ExclusiveEnd
                                }).ToList()
                            });
                            break;
                        case ScalarValueNode.IntegerScalarValue:
                            Project.IntegerTimeSeriesValues.Add(new IntegerTimeSeriesEntity
                            {
                                Key = key,
                                Entries = timeSeriesValue.Value.Select(v => new IntegerTimeSeriesEntry
                                {
                                    Value = ((ScalarValueNode.IntegerScalarValue)v.Value).Value,
                                    Start = v.Period.InclusiveStart,
                                    End = v.Period.ExclusiveEnd
                                }).ToList()
                            });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }


        if (ProjectId == null)
        {
            ProjectId = Project.Id = Guid.NewGuid();
            await ProjectManagement.CreateProjectAsync(Project);
        }
        else
            await ProjectManagement.UpdateProjectAsync(Project);
    }

    private async Task OnEditorDidInit(object arg)
    {
        if (_editor == null) return;
        _ideInitialized = true;
        await _editor.SetValue(Project.Code);
    }
}