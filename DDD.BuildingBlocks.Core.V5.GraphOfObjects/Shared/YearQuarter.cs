namespace DDD.BuildingBlocks.Core.V5.GraphOfObjects.Shared;

internal class YearQuarter
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
}