using System.Collections.Immutable;

namespace FormulaForge.Engine.Time;

public sealed class MonthlySeries<T>
{
    private readonly ImmutableDictionary<YearMonth, T> _data;

    public MonthlySeries(IEnumerable<KeyValuePair<YearMonth, T>> data)
        => _data = data.ToImmutableDictionary();

    public bool TryGet(YearMonth month, out T value) => _data.TryGetValue(month, out value);

    public T GetOrThrow(YearMonth month, string seriesName)
        => _data.TryGetValue(month, out var v)
            ? v
            : throw new KeyNotFoundException($"Missing {seriesName} for month {month}.");

    public IEnumerable<(YearMonth Month, T Value)> Items()
    {
        foreach (var kv in _data)
            yield return (kv.Key, kv.Value);
    }
    
    public IEnumerable<YearMonth> Months => _data.Keys.OrderBy(x => x.Year).ThenBy(x => x.Month);
}