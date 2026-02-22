using System.Collections;
using System.Numerics;

namespace FormulaForge.Engine.Time;

public static class TimeSeries
{
    /// <summary>
    /// Creates an empty TimeSeries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static TimeSeries<T> Empty<T>() => new();
    
    /// <summary>
    /// Creates a TimeSeries from the specified values.
    /// </summary>
    /// <param name="values"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TimeSeries<T> CreateTimeSeries<T>(this IEnumerable<TimeSeriesValue<T>> values)
    {
        var timeSeries = new TimeSeries<T>();
        
        foreach (var value in values)
        {
            timeSeries.Add(value.Period, value.Value, (o, n) => throw new Exception("Cannot merge values in a TimeSeries during creation."));
        }
        
        return timeSeries;
    }

    /// <summary>
    /// Aggregates the specified TimeSeries values using the specified merge function.
    /// </summary>
    /// <param name="sources"></param>
    /// <param name="mergeFunction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static TimeSeries<T> Aggregate<T>(this IReadOnlyCollection<TimeSeries<T>> sources, 
        Func<TimeSeriesValue<T>, TimeSeriesValue<T>, T> mergeFunction)
    {
        var timeSeries = new TimeSeries<T>();
        
        foreach (var series in sources)
        {
            foreach (var value in series)
            {
                timeSeries.Add(value.Period, value.Value, mergeFunction);
            }
        }
        
        return timeSeries;
    }

    /// <summary>
    /// Multiplies the values of two TimeSeries instances element-wise over their common periods.
    /// </summary>
    /// <param name="a">The first TimeSeries instance.</param>
    /// <param name="b">The second TimeSeries instance.</param>
    /// <typeparam name="T">The numeric type of the values in the TimeSeries.</typeparam>
    /// <returns>A new TimeSeries instance containing the element-wise multiplied values of the two input TimeSeries.</returns>
    public static TimeSeries<T> Multiply<T>(this TimeSeries<T> a, TimeSeries<T> b) where T : INumber<T>
    {
        TimeSeries<T>[] sources = [a, b];
        return sources.Aggregate((x,y) => x.Value * y.Value);
    }

    /// <summary>
    /// Divides the corresponding values of two TimeSeries instances.
    /// </summary>
    /// <param name="a">The first TimeSeries instance.</param>
    /// <param name="b">The second TimeSeries instance.</param>
    /// <typeparam name="T">The numeric type of the values in the TimeSeries.</typeparam>
    /// <returns>A new TimeSeries containing the result of the division for each corresponding value in the input TimeSeries instances.</returns>
    public static TimeSeries<T> Divide<T>(this TimeSeries<T> a, TimeSeries<T> b) where T : INumber<T>
    {
        TimeSeries<T>[] sources = [a, b];
        return sources.Aggregate((x,y) => x.Value / y.Value);
    }

    /// <summary>
    /// Adds the values of two TimeSeries instances element-wise for matching periods using the specified numeric type T.
    /// </summary>
    /// <param name="a">The first TimeSeries instance to add.</param>
    /// <param name="b">The second TimeSeries instance to add.</param>
    /// <typeparam name="T">The numeric type of the values in the TimeSeries, which must implement INumber&lt;T&gt;.</typeparam>
    /// <returns>A new TimeSeries instance containing the summed values for matching periods from the input TimeSeries instances.</returns>
    public static TimeSeries<T> Add<T>(this TimeSeries<T> a, TimeSeries<T> b) where T : INumber<T>
    {
        TimeSeries<T>[] sources = [a, b];
        return sources.Aggregate((x,y) => x.Value + y.Value);
    }

    /// <summary>
    /// Subtracts the values of one TimeSeries from another.
    /// </summary>
    /// <param name="a">The first TimeSeries.</param>
    /// <param name="b">The second TimeSeries to subtract from the first.</param>
    /// <typeparam name="T">The numeric type used in the TimeSeries.</typeparam>
    /// <returns>A new TimeSeries representing the result of subtraction.</returns>
    public static TimeSeries<T> Subtract<T>(this TimeSeries<T> a, TimeSeries<T> b) where T : INumber<T>
    {
        TimeSeries<T>[] sources = [a, b];
        return sources.Aggregate((x,y) => x.Value - y.Value);
    }

    /// <summary>
    /// Casts a TimeSeries from one type to another type, ensuring compatibility between the types.
    /// </summary>
    /// <param name="timeSeries">The input TimeSeries to be cast.</param>
    /// <typeparam name="TIn">The type of the input TimeSeries values.</typeparam>
    /// <typeparam name="TOut">The type to which the TimeSeries values will be cast.</typeparam>
    /// <returns>A new TimeSeries with values cast to the specified type.</returns>
    public static TimeSeries<TOut> Cast<TOut, TIn>(this TimeSeries<TIn> timeSeries) where TIn : TOut
    {
        return CreateTimeSeries(timeSeries.Select(value => new TimeSeriesValue<TOut>(value.Period, value.Value)));
    }
}

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