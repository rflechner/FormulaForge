namespace FormulaForge.Engine.Time;

public readonly record struct YearMonth
{
    public YearMonth(int Year, int Month)
    {
        if (Month is < 1 or > 12) throw new ArgumentOutOfRangeException(nameof(Month));
        
        this.Year = Year;
        this.Month = Month;
    }

    public int Year { get; }
    public int Month { get; }

    public static YearMonth FromDate(DateTime date) => new(date.Year, date.Month);

    public YearMonth AddMonths(int delta)
    {
        var total = (Year * 12 + (Month - 1)) + delta;
        var y = total / 12;
        var m = (total % 12) + 1;
        return new YearMonth(y, m);
    }

    public override string ToString() => $"{Year:D4}-{Month:D2}";
}