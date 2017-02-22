using System;

namespace CBS.Logic.Handlers.Implementations
{
    public static class DayCalculator
    {
        public static int CalculateDateDifferenceCountStartedDay(DateTime formerDate, DateTime laterDate)
        {
            // Counts full days plus days that are started.
            var difference = laterDate - formerDate;
            return (int)Math.Ceiling(difference.TotalDays);
        }

        public static int CalculateDateDifference(DateTime formerDate, DateTime laterDate)
        {
            // Alternative method in which only full days are counted. 
            // Specification is unclear which one should be used. Third option is to count days as decimal.
            return (laterDate - formerDate).Days;
        }
    }
}
