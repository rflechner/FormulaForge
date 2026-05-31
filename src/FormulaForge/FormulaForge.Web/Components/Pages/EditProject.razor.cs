using FormulaForge.Domain.Entities;
using FormulaForge.Domain.Services;
using FormulaForge.Engine.DomainSpecificLanguage.Ast;
using FormulaForge.Engine.Runtime;
using FormulaForge.Engine.Time;
using FormulaForge.Web.Components.Shared;
using FormulaForge.Web.Contexts;
using FormulaForge.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace FormulaForge.Web.Components.Pages;

public partial class EditProject
{
    private List<DateTime> _months = new();

    private readonly List<ComputationGrid.RowSeriesData> _inputsSeries = new();
    private readonly List<ComputationGrid.RowSeriesData> _outputsSeries = new();

    private readonly List<ComputationGrid.RowScalarData> _outputsScalars = new();
    private readonly List<ComputationGrid.RowScalarData> _inputsScalars = new();

    private readonly DynamicDslContext _dslContext = new(new DynamicDataContext());
    private ScriptInterpreter? _interpreter;
    private MonacoEditor? _editor;
    private bool _ideInitialized;

    private string? _errorMessage;
    private bool _isRunning;
    private int? _lastRunMs;
    private string _activeTab = "console";
    private int _invalidLineCount;
    private bool _isDark;
    private DateTime? _savedAt;

    private readonly List<(string Level, string Message)> _consoleLogs = new();

    public CodeRunResult? ScriptRunResult { get; set; }

    public Project Project { get; set; } = new Project
    {
        Name = "Nouveau projet",
        UserId = string.Empty,
    };

    [Inject] public required IProjectManagementService ProjectManagement { get; init; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; init; }
    [Inject] public required IJSRuntime JS { get; init; }

    [Parameter] public Guid? ProjectId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var userId = await AuthenticationStateProvider.GetUserIdAsync();

        if (userId != null && ProjectId.HasValue &&
            await ProjectManagement.GetProjectAsync(ProjectId.Value, userId) is { } project)
        {
            Project = project;

            if (_ideInitialized && _editor != null)
                await TrySetEditorValue(project.Code);

            InitializeContext(project);

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
            UserId = userId ?? string.Empty,
            Name = "Nouveau projet"
        };

        await LoadInputs();
        await DisplayRows();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        _isDark = await JS.InvokeAsync<bool>("eval", "document.documentElement.classList.contains('dark')");

        if (ProjectId.HasValue && _editor != null)
            await TrySetEditorValue(Project.Code);

        await DisplayRows();
    }

    private async Task TrySetEditorValue(string code)
    {
        try { await _editor!.SetValue(code); }
        catch (Exception e) { Console.WriteLine(e); }
    }

    private void InitializeContext(Project project)
    {
        ClearContext();

        foreach (var s in project.BooleanScalarValues)
            _dslContext.GlobalScope.Variables.TryAdd(s.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.BooleanScalarValue(s.Value)));

        foreach (var s in project.IntegerScalarValues)
            _dslContext.GlobalScope.Variables.TryAdd(s.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(s.Value)));

        foreach (var s in project.DecimalScalarValues)
            _dslContext.GlobalScope.Variables.TryAdd(s.Key, new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.DecimalScalarValue(s.Value)));

        foreach (var ts in project.IntegerTimeSeriesValues)
        {
            var serie = ts.Entries
                .Select(e => new TimeSeriesValue<ScalarValueNode.IntegerScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.IntegerScalarValue(e.Value)))
                .CreateTimeSeries().Cast<ScalarValueNode, ScalarValueNode.IntegerScalarValue>().CreateTimeSeries();
            _dslContext.GlobalScope.Variables.TryAdd(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(serie));
        }

        foreach (var ts in project.DecimalTimeSeriesValues)
        {
            var serie = ts.Entries
                .Select(e => new TimeSeriesValue<ScalarValueNode.DecimalScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.DecimalScalarValue(e.Value)))
                .CreateTimeSeries().Cast<ScalarValueNode, ScalarValueNode.DecimalScalarValue>().CreateTimeSeries();
            _dslContext.GlobalScope.Variables.TryAdd(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(serie));
        }

        foreach (var ts in project.BooleanTimeSeriesValues)
        {
            var serie = ts.Entries
                .Select(e => new TimeSeriesValue<ScalarValueNode.BooleanScalarValue>(new Period(e.Start, e.End), new ScalarValueNode.BooleanScalarValue(e.Value)))
                .CreateTimeSeries().Cast<ScalarValueNode, ScalarValueNode.BooleanScalarValue>().CreateTimeSeries();
            _dslContext.GlobalScope.Variables.TryAdd(ts.Key, new RuntimeVariableValue.RuntimeTimeSeriesValue(serie));
        }
    }

    private void ClearContext()
    {
        _dslContext.ClearFunctions();
        _dslContext.GlobalScope.Variables.Clear();
        _dslContext.GlobalScope.BuiltInVariables.Clear();
    }

    private Task LoadInputs()
    {
        _inputsSeries.Clear();
        _inputsScalars.Clear();

        var period = new Period(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        var months = period.GetMonths().ToArray();
        _months = months.Select(m => m.InclusiveStart.DateTime).ToList();

        _dslContext.GlobalScope.Variables.TryAdd("x",
            new RuntimeVariableValue.RuntimeScalarValue(new ScalarValueNode.IntegerScalarValue(10)));
        _inputsScalars.Add(new ComputationGrid.RowScalarData { Name = "x", ScalarValue = new ScalarValueNode.IntegerScalarValue(10) });

        var random = new Random();
        var balance = new TimeSeries<ScalarValueNode>();
        var changeRate = new TimeSeries<ScalarValueNode>();

        for (var i = 0; i < months.Length; i++)
        {
            balance.Add(months[i], new ScalarValueNode.IntegerScalarValue(i * 100), (a, _) => a.Value);
            changeRate.Add(months[i], new ScalarValueNode.DecimalScalarValue((decimal)random.NextDouble()), (a, _) => a.Value);
        }

        _dslContext.GlobalScope.Variables.TryAdd("balance", new RuntimeVariableValue.RuntimeTimeSeriesValue(balance));
        _dslContext.GlobalScope.Variables.TryAdd("change_rate", new RuntimeVariableValue.RuntimeTimeSeriesValue(changeRate));

        _inputsSeries.Add(new ComputationGrid.RowSeriesData { Name = "balance", Values = MapSeries(new RuntimeVariableValue.RuntimeTimeSeriesValue(balance)) });
        _inputsSeries.Add(new ComputationGrid.RowSeriesData { Name = "change_rate", Values = MapSeries(new RuntimeVariableValue.RuntimeTimeSeriesValue(changeRate)) });

        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task DisplayRows(bool clear = true)
    {
        if (clear)
        {
            _outputsSeries.Clear();
            _outputsScalars.Clear();
        }

        foreach (var (name, value) in _dslContext.GlobalScope.Variables)
        {
            if (value is RuntimeVariableValue.RuntimeTimeSeriesValue ts)
                _outputsSeries.Add(new ComputationGrid.RowSeriesData { Name = name, Values = MapSeries(ts) });
            if (value is RuntimeVariableValue.RuntimeScalarValue sc)
                _outputsScalars.Add(new ComputationGrid.RowScalarData { Name = name, ScalarValue = sc.Value });
        }

        StateHasChanged();
    }

    private async Task RunScript()
    {
        if (_editor == null) return;

        _isRunning = true;
        _consoleLogs.Clear();
        _lastRunMs = null;
        _invalidLineCount = 0;
        _errorMessage = null;
        StateHasChanged();

        var sw = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            ClearContext();
            await LoadInputs();

            AddLog("info", $"Chargement des sources… {_inputsSeries.Count} time series, {_inputsScalars.Count} scalaires");

            _interpreter = new ScriptInterpreter(_dslContext);
            var code = await _editor.GetValue();
            var nodes = _interpreter.ParseScriptCode(code);

            var invalidLines = nodes.OfType<InvalidLine>().ToArray();
            _invalidLineCount = invalidLines.Length;

            if (invalidLines.Any())
            {
                var markers = invalidLines.Select(l => new MonacoEditor.ErrorMarker(l.PositionRange, "Syntaxe non reconnue")).ToArray();
                await _editor.HighlightErrors(markers);
                AddLog("warn", $"{invalidLines.Length} ligne(s) non reconnue(s) ignorée(s)");
            }
            else
            {
                await _editor.ClearErrorHighlights();
            }

            AddLog("info", $"Compilation FormulaScript — {nodes.Length - invalidLines.Length} instruction(s)");

            ScriptRunResult = _interpreter.Run(nodes);

            if (ScriptRunResult != CodeRunResult.Success)
            {
                AddLog("err", ScriptRunResult?.ToString() ?? "Erreur inconnue");
                _errorMessage = $"Erreur d'exécution : {ScriptRunResult}";
            }
            else
            {
                AddLog("ok", $"{_dslContext.GlobalScope.Variables.Count} variable(s) produite(s)");
                _activeTab = "variables";
            }
        }
        catch (Exception e)
        {
            AddLog("err", e.Message);
            _errorMessage = e.Message;
        }
        finally
        {
            sw.Stop();
            _lastRunMs = (int)sw.ElapsedMilliseconds;
            _isRunning = false;
        }

        await DisplayRows();
    }

    private void AddLog(string level, string message) => _consoleLogs.Add((level, message));

    private async Task SaveProject()
    {
        if (string.IsNullOrWhiteSpace(Project.Name))
        {
            _errorMessage = "Le nom du projet ne peut pas être vide.";
            return;
        }

        if (_editor != null)
            Project.Code = await _editor.GetValue();

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
                case RuntimeVariableValue.RuntimeScalarValue { Value: ScalarValueNode.BooleanScalarValue bv }:
                    Project.BooleanScalarValues.Add(new BooleanScalarValueEntity { Key = key, Value = bv.Value });
                    break;
                case RuntimeVariableValue.RuntimeScalarValue { Value: ScalarValueNode.DecimalScalarValue dv }:
                    Project.DecimalScalarValues.Add(new DecimalScalarValueEntity { Key = key, Value = dv.Value });
                    break;
                case RuntimeVariableValue.RuntimeScalarValue { Value: ScalarValueNode.IntegerScalarValue iv }:
                    Project.IntegerScalarValues.Add(new IntegerScalarValueEntity { Key = key, Value = iv.Value });
                    break;
                case RuntimeVariableValue.RuntimeTimeSeriesValue tsv:
                    var first = tsv.Value.FirstOrDefault()?.Value;
                    if (first == null) break;
                    switch (first)
                    {
                        case ScalarValueNode.BooleanScalarValue:
                            Project.BooleanTimeSeriesValues.Add(new BooleanTimeSeriesEntity
                            {
                                Key = key,
                                Entries = tsv.Value.Select(v => new BooleanTimeSeriesEntry
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
                                Entries = tsv.Value.Select(v => new DecimalTimeSeriesEntry
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
                                Entries = tsv.Value.Select(v => new IntegerTimeSeriesEntry
                                {
                                    Value = ((ScalarValueNode.IntegerScalarValue)v.Value).Value,
                                    Start = v.Period.InclusiveStart,
                                    End = v.Period.ExclusiveEnd
                                }).ToList()
                            });
                            break;
                    }
                    break;
            }
        }

        if (ProjectId == null)
        {
            ProjectId = Project.Id = Guid.NewGuid();
            await ProjectManagement.CreateProjectAsync(Project);
        }
        else
        {
            await ProjectManagement.UpdateProjectAsync(Project);
        }

        _savedAt = DateTime.Now;
        StateHasChanged();
    }

    private async Task OnEditorDidInit(object _)
    {
        if (_editor == null) return;
        _ideInitialized = true;
        await TrySetEditorValue(Project.Code);
    }

    private Dictionary<int, decimal> MapSeries(RuntimeVariableValue.RuntimeTimeSeriesValue ts) =>
        ts.Value
            .Select((v, i) => (Index: i, Value: MapScalar(v.Value)))
            .ToDictionary(x => x.Index, x => x.Value);

    private static decimal MapScalar(ScalarValueNode n) => n switch
    {
        ScalarValueNode.BooleanScalarValue b => b.Value ? 1 : 0,
        ScalarValueNode.DecimalScalarValue d => d.Value,
        ScalarValueNode.IntegerScalarValue i => i.Value,
        _ => throw new ArgumentOutOfRangeException()
    };
}
