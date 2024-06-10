namespace RaceResults
{
    internal class Results
    {
        public readonly List<TimeMeasurement> _timeMeasurements;

        public Results()
        {
            var lines = File.ReadAllLines("timedata.csv");
            _timeMeasurements = new List<TimeMeasurement>();
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var bibNumber = int.Parse(parts[0]);
                var time = TimeOnly.Parse(parts[1]);
                var km = parts[2];
                var timeMeasurement = GetOrCreateTimeMeasurement(bibNumber);
                if (km == "0") timeMeasurement.TimeAtStart = time;
                else if (km == "5") timeMeasurement.TimeAt5K = time;
                else if (km == "10") timeMeasurement.TimeAt10K = time;
            }
        }

        public void ShowReport()
        {
            var finished = new List<TimeMeasurement>();
            foreach (var timeMeasurement in _timeMeasurements)
            {
                if (timeMeasurement.TimeAt10K != null
                && timeMeasurement.TimeAtStart != null)
                {
                    finished.Add(timeMeasurement);
                }
            }
            finished.Sort((tmA, tmB) =>
            {
                var elapsedA = tmA.TimeAt10K.Value.ToTimeSpan() - tmA.TimeAtStart.Value.ToTimeSpan();
                var elapsedB = tmB.TimeAt10K.Value.ToTimeSpan() - tmB.TimeAtStart.Value.ToTimeSpan();
                return Convert.ToInt32(elapsedA.TotalMilliseconds - elapsedB.TotalMilliseconds);
            });
            Console.WriteLine("Startnr Tid");
            foreach (var timeMeasurement in finished)
            {
                var elapsed = timeMeasurement.TimeAt10K.Value.ToTimeSpan()
                              - timeMeasurement.TimeAtStart.Value.ToTimeSpan();
                Console.WriteLine($"{timeMeasurement.BibNumber,7} {elapsed}");
            }
        }

        public TimeMeasurement GetOrCreateTimeMeasurement(int bibNumber)
        {
            foreach (var tm in _timeMeasurements)
            {
                if (tm.BibNumber == bibNumber) return tm;
            }

            var timeMeasurement = new TimeMeasurement { BibNumber = bibNumber };
            _timeMeasurements.Add(timeMeasurement);
            return timeMeasurement;
        }
    }
}
