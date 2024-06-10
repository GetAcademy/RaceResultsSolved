namespace RaceResults
{
    internal class TimeMeasurement
    {
        private int _bibNumber;
        private TimeOnly? _timeAtStart;
        private TimeOnly? _timeAt5K;
        private TimeOnly? _timeAt10K;

        public TimeMeasurement(int bibNumber)
        {
            _bibNumber = bibNumber;
        }

        public bool Matches(int bibNumber)
        {
            return bibNumber == _bibNumber;
        }

        public Result GetResult()
        {
            if (_timeAt10K == null || _timeAtStart == null) return null;
            return new Result(_bibNumber, Elapsed(_timeAtStart, _timeAt10K));
        }

        public void AddTime(string km, TimeOnly time)
        {
            if (km == "0") _timeAtStart = time;
            else if (km == "5") _timeAt5K = time;
            else if (km == "10") _timeAt10K = time;
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
