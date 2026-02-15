using System.Collections;

namespace FormulaForge.Engine.Time;

public class TimeSeries<T> : IEnumerable<TimeSeriesValue<T>>
{
    private readonly SortedList<Period, T> _data = new(new PeriodStartComparer());

    /// <summary>
    /// Adds a new entry to the TimeSeries for the specified period and value.
    /// If the period already exists in the TimeSeries, the mergeFunction is applied to handle the conflict.
    /// </summary>
    /// <param name="period">The period to associate with the value being added.</param>
    /// <param name="value">The value to add to the TimeSeries for the specified period.</param>
    /// <param name="mergeFunction">The function to merge values if the specified period already exists in the TimeSeries.</param>
    public void Add(Period period, T value, Func<TimeSeriesValue<T>, TimeSeriesValue<T>, T> mergeFunction)
    {
        var overlapping = _data.Keys.Where(p => p.Overlaps(period)).ToList();
        if (overlapping.Count == 0)
        {
            _data.Add(period, value);
            return;
        }

        var boundaries = new SortedSet<DateTimeOffset>
        {
            period.InclusiveStart,
            period.ExclusiveEnd
        };
        foreach (var p in overlapping)
        {
            boundaries.Add(p.InclusiveStart);
            boundaries.Add(p.ExclusiveEnd);
        }

        var oldValues = overlapping.ToDictionary(p => p, p => _data[p]);
        foreach (var p in overlapping)
        {
            _data.Remove(p);
        }

        var boundaryList = boundaries.ToList();
        for (var i = 0; i < boundaryList.Count - 1; i++)
        {
            var segment = new Period(boundaryList[i], boundaryList[i + 1]);
            
            var existingPeriod = overlapping.FirstOrDefault(p => segment.InclusiveStart >= p.InclusiveStart && segment.ExclusiveEnd <= p.ExclusiveEnd);
            bool inNewPeriod = segment.InclusiveStart >= period.InclusiveStart && segment.ExclusiveEnd <= period.ExclusiveEnd;

            if (existingPeriod != null && inNewPeriod)
            {
                var mergedValue = mergeFunction(
                    new TimeSeriesValue<T>(segment, oldValues[existingPeriod]),
                    new TimeSeriesValue<T>(segment, value));
                _data.Add(segment, mergedValue);
            }
            else if (existingPeriod != null)
            {
                _data.Add(segment, oldValues[existingPeriod]);
            }
            else if (inNewPeriod)
            {
                _data.Add(segment, value);
            }
        }
        
        FullPeriod = new Period(boundaries.Min, boundaries.Max);
    }

    public T GetValue(Period period) => _data[period];

    public bool ContainsPeriod(Period period) => _data.ContainsKey(period);


    public IEnumerator<TimeSeriesValue<T>> GetEnumerator()
    {
        return _data.Select(kv => new TimeSeriesValue<T>(kv.Key, kv.Value)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public Period FullPeriod { get; private set; } = Period.Empty;
}

public record TimeSeriesValue<T>(Period Period, T Value);