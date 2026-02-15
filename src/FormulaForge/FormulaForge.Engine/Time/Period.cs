namespace FormulaForge.Engine.Time;

/// <summary>
/// Represents a time period with a start and an end. The start of the period is inclusive, while the end is exclusive.
/// </summary>
public record Period(DateTimeOffset InclusiveStart, DateTimeOffset ExclusiveEnd)
{
    /// <summary>
    /// The duration of the period.
    /// </summary>
    public TimeSpan Duration => ExclusiveEnd - InclusiveStart;
    
    /// <summary>
    /// Returns true if the period is empty, i.e. its duration is zero.
    /// </summary>
    public bool IsEmpty => Duration == TimeSpan.Zero;
    
    /// <summary>
    /// Creates an empty period.
    /// </summary>
    public static Period Empty => new(DateTimeOffset.MinValue, DateTimeOffset.MinValue);
    
    /// <summary>
    /// Returns true if the period contains the specified date.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Contains(DateTimeOffset value) => value >= InclusiveStart && value < ExclusiveEnd;

    /// <summary>
    /// Determines whether the current period overlaps with the specified period.
    /// </summary>
    /// <param name="other">The period to check for overlap with the current period.</param>
    /// <returns>True if the periods overlap; otherwise, false.</returns>
    public bool Overlaps(Period other) => InclusiveStart < other.ExclusiveEnd && other.InclusiveStart < ExclusiveEnd;

    /// <summary>
    /// Divides the current period into multiple sub-periods based on the specified function that calculates the next boundary.
    /// </summary>
    /// <param name="moveNext">A function that determines the next boundary for the sub-periods by taking the current boundary as input.</param>
    /// <returns>An enumerable collection of sub-periods created by dividing the current period.</returns>
    public IEnumerable<Period> Divide(Func<DateTimeOffset, DateTimeOffset> moveNext)
    {
        var current = InclusiveStart;
        while (current < ExclusiveEnd)
        {
            var next = moveNext(current);
            yield return new Period(current, next);
            current = next;
        }
    }

    /// <summary>
    /// Divides the current period into daily sub-periods, each representing a single day within the period.
    /// </summary>
    /// <returns>An enumerable collection of daily sub-periods.</returns>
    public IEnumerable<Period> GetDays() => Divide(current => current.AddDays(1));

    /// <summary>
    /// Returns all the months in the period.
    /// </summary>
    /// <returns>An enumerable collection of sub-periods that represent each month within the current period.</returns>
    public IEnumerable<Period> GetMonths() => Divide(current => current.AddMonths(1));

    /// <summary>
    /// Returns all the years in the period.
    /// </summary>
    /// <returns>An enumerable collection of sub-periods that represent each year within the current period.</returns>
    public IEnumerable<Period> GetYears() => Divide(current => current.AddYears(1));
}