using FormulaForge.Engine.Time;

namespace FormulaForge.Engine.Tests.Time;

public class PeriodTests
{
    [Fact]
    public void Overlaps_WhenPeriodsDoNotOverlap_ReturnsFalse()
    {
        var period1 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)
        );
        var period2 = new Period(
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 20, 0, 0, 0, TimeSpan.Zero)
        );

        Assert.False(period1.Overlaps(period2));
        Assert.False(period2.Overlaps(period1));
    }

    [Fact]
    public void Overlaps_WhenPeriodStartsInsideOther_ReturnsTrue()
    {
        var period1 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)
        );
        var period2 = new Period(
            new DateTimeOffset(2024, 1, 5, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero)
        );

        Assert.True(period1.Overlaps(period2));
        Assert.True(period2.Overlaps(period1));
    }

    [Fact]
    public void Overlaps_WhenPeriodIsContainedWithinOther_ReturnsTrue()
    {
        var period1 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 20, 0, 0, 0, TimeSpan.Zero)
        );
        var period2 = new Period(
            new DateTimeOffset(2024, 1, 5, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)
        );

        Assert.True(period1.Overlaps(period2));
        Assert.True(period2.Overlaps(period1));
    }

    [Fact]
    public void Overlaps_WhenPeriodsAreIdentical_ReturnsTrue()
    {
        var period1 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)
        );
        var period2 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)
        );

        Assert.True(period1.Overlaps(period2));
        Assert.True(period2.Overlaps(period1));
    }

    [Fact]
    public void Overlaps_WhenPeriodEndsBeforeOtherStarts_ReturnsFalse()
    {
        var period1 = new Period(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 5, 0, 0, 0, TimeSpan.Zero)
        );
        var period2 = new Period(
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero)
        );

        Assert.False(period1.Overlaps(period2));
        Assert.False(period2.Overlaps(period1));
    }

    
    [Fact]
    public void GetDays_ReturnsCorrectNumberOfDays()
    {
        var start = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 1, 4, 0, 0, 0, TimeSpan.Zero);
        var period = new Period(start, end);

        var days = period.GetDays().ToList();

        Assert.Equal(3, days.Count);
        Assert.Equal(start, days[0].InclusiveStart);
        Assert.Equal(start.AddDays(1), days[0].ExclusiveEnd);
        Assert.Equal(start.AddDays(1), days[1].InclusiveStart);
        Assert.Equal(start.AddDays(2), days[1].ExclusiveEnd);
        Assert.Equal(start.AddDays(2), days[2].InclusiveStart);
        Assert.Equal(end, days[2].ExclusiveEnd);
    }
}