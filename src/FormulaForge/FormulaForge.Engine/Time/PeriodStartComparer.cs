namespace FormulaForge.Engine.Time;

internal class PeriodStartComparer : IComparer<Period>
{
    public int Compare(Period? x, Period? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (y is null) return 1;
        if (x is null) return -1;
        
        return x.InclusiveStart.CompareTo(y.InclusiveStart);
    }
}