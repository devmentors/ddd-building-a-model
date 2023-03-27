namespace DDD.BuildingBlocks.Core.V6.SpecializedAggregate.Shared;

public class YearQuarter : IEquatable<YearQuarter>
{
    public YearQuarter(DateTime date)
    {
        QuarterNumber = (int)Math.Floor((date.Month - 1) / 3d) + 1;
        QuarterYear = date.Year;
    }

    public int QuarterNumber { get; }
    public int QuarterYear { get; }
    public int QuarterStartMonth => (QuarterNumber - 1) * 3 + 1;

    public DateTime GetQuarterStartDate()
    {
        return new DateTime(QuarterYear, QuarterStartMonth, 1);
    }
    
    public YearQuarter CreateFollowingQuarter() => new(GetQuarterStartDate().AddMonths(3));

    public bool Equals(YearQuarter? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return QuarterNumber == other.QuarterNumber && QuarterYear == other.QuarterYear;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((YearQuarter)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(QuarterNumber, QuarterYear);
    }
}