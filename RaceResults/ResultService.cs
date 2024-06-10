namespace RaceResults
{
    internal class ResultService
    {
        public readonly List<TimeMeasurement> _timeMeasurements;

        public ResultService()
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
                timeMeasurement.AddTime(km, time);
            }
        }

        public void ShowReport()
        {
            var finished = new List<Result>();
            foreach (var timeMeasurement in _timeMeasurements)
            {
                var result = timeMeasurement.GetResult();
                if(result!=null)finished.Add(result);
            }
            finished.Sort(Result.Compare);
            Console.WriteLine("Startnr Tid");
            foreach (var result in finished)
            {
                result.ShowAsTableRow();
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
