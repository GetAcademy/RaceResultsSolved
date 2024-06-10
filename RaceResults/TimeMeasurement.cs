namespace RaceResults
{
    internal class TimeMeasurement
    {
        private int BibNumber;
        private TimeOnly? TimeAtStart;
        private TimeOnly? TimeAt5K;
        private TimeOnly? TimeAt10K;

        public Result GetResult()
        {
            if (TimeAt10K == null || TimeAtStart == null) return null;
            return new Result(BibNumber, Elapsed(TimeAtStart, TimeAt10K));
        }

        public void AddTime(string km, TimeOnly time)
        {
            if (km == "0") TimeAtStart = time;
            else if (km == "5") TimeAt5K = time;
            else if (km == "10") TimeAt10K = time;
        }

        private static TimeSpan Elapsed(TimeOnly? a, TimeOnly? b)
        {
            return ToTimeSpan(b) - ToTimeSpan(a);
        }

        private static TimeSpan ToTimeSpan(TimeOnly? timeOnly)
        {
            return timeOnly.Value.ToTimeSpan();
        }
    }
}
