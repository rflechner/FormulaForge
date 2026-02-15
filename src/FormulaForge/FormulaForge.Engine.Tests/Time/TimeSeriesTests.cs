using FormulaForge.Engine.Time;
using Xunit;

namespace FormulaForge.Engine.Tests.Time;

public class TimeSeriesTests
{
    [Fact]
    public void Add_WhenPeriodDoesNotExist_ShouldAddValue()
    {
        // Arrange
        var series = new TimeSeries<int>();
        var period = new Period(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
        
        // Act
        series.Add(period, 10, (oldVal, newVal) => newVal.Value);

        // Assert
        Assert.True(series.ContainsPeriod(period));
        Assert.Equal(10, series.GetValue(period));
    }

    [Fact]
    public void Add_WhenPeriodExists_ShouldMergeValues()
    {
        // Arrange
        var series = new TimeSeries<int>();
        var start = DateTimeOffset.Now;
        var end = start.AddDays(1);
        var period = new Period(start, end);
        series.Add(period, 10, (oldVal, newVal) => newVal.Value);

        // Act
        series.Add(period, 5, (oldVal, newVal) => {
            Assert.Equal(10, oldVal.Value);
            Assert.Equal(5, newVal.Value);
            Assert.Equal(period, oldVal.Period);
            Assert.Equal(period, newVal.Period);
            return oldVal.Value + newVal.Value;
        });

        // Assert
        Assert.Equal(15, series.GetValue(period));
    }

    [Fact]
    public void Add_MultiplePeriods_ShouldBeSorted()
    {
        // Arrange
        var series = new TimeSeries<int>();
        var p1 = new Period(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 2, 0, 0, 0, TimeSpan.Zero));
        var p2 = new Period(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero));
        
        // Act
        series.Add(p1, 1, (o, n) => n.Value);
        series.Add(p2, 2, (o, n) => n.Value);

        // Assert
        Assert.Equal(1, series.GetValue(p1));
        Assert.Equal(2, series.GetValue(p2));
    }
    
    [Fact]
    public void Add_OverlappingPeriods_ShouldBeMerged()
    {
        // Arrange
        var series = new TimeSeries<int>();
        var p1 = new Period(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero));
        var p2 = new Period(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero));
        var p3 = new Period(new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero));

        int AggregationFunction(TimeSeriesValue<int> o, TimeSeriesValue<int> n) => n.Value + o.Value;

        // Act
        series.Add(p1, 1, AggregationFunction);
        series.Add(p2, 2, AggregationFunction);
        series.Add(p3, 5, AggregationFunction);

        TimeSeriesValue<int>[] seriesValues = series.ToArray();

        // Assert

        var expectedP0 = new Period(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero));
        var expectedP1 = new Period(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero));
        var expectedP2 = new Period(new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero));
        var expectedP3 = new Period(new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero));
        
        Assert.Equal(4, seriesValues.Length);
        Assert.Equal(expectedP0, seriesValues[0].Period);
        Assert.Equal(2, seriesValues[0].Value);
        
        Assert.Equal(expectedP1, seriesValues[1].Period);
        Assert.Equal(1, seriesValues[1].Value);
        
        Assert.Equal(expectedP2, seriesValues[2].Period);
        Assert.Equal(6, seriesValues[2].Value); // 1 + 5
        
        Assert.Equal(expectedP3, seriesValues[3].Period);
        Assert.Equal(1, seriesValues[3].Value);
    }

    [Fact]
    public void Add_PartialOverlap_ShouldFragmentCorrectly()
    {
        // Arrange
        var series = new TimeSeries<int>();
        var p1 = new Period(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 10, 0, 0, 0, TimeSpan.Zero));
        var p2 = new Period(new DateTimeOffset(2023, 1, 5, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero));

        // Act
        series.Add(p1, 10, (o, n) => o.Value + n.Value);
        series.Add(p2, 20, (o, n) => o.Value + n.Value);

        // Assert
        var values = series.ToArray();
        Assert.Equal(3, values.Length);
        
        // 1-5
        Assert.Equal(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), values[0].Period.InclusiveStart);
        Assert.Equal(new DateTimeOffset(2023, 1, 5, 0, 0, 0, TimeSpan.Zero), values[0].Period.ExclusiveEnd);
        Assert.Equal(10, values[0].Value);

        // 5-10
        Assert.Equal(new DateTimeOffset(2023, 1, 5, 0, 0, 0, TimeSpan.Zero), values[1].Period.InclusiveStart);
        Assert.Equal(new DateTimeOffset(2023, 1, 10, 0, 0, 0, TimeSpan.Zero), values[1].Period.ExclusiveEnd);
        Assert.Equal(30, values[1].Value);

        // 10-15
        Assert.Equal(new DateTimeOffset(2023, 1, 10, 0, 0, 0, TimeSpan.Zero), values[2].Period.InclusiveStart);
        Assert.Equal(new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero), values[2].Period.ExclusiveEnd);
        Assert.Equal(20, values[2].Value);
    }
    
}