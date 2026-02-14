namespace FormulaForge.Engine.Time;

public interface ITimeSeries<T>
{
    bool TryGet(YearMonth month, out T value);

    IEnumerable<YearMonth> Months { get; }
}