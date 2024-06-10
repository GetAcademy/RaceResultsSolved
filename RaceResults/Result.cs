namespace RaceResults
{
    internal class Result
    {
        private readonly int _bibNumber;
        private readonly TimeSpan _elapsedTime;

        public Result(int bibNumber, TimeSpan elapsedTime)
        {
            _bibNumber = bibNumber;
            _elapsedTime = elapsedTime;
        }

        public void ShowAsTableRow()
        {
            Console.WriteLine($"{_bibNumber,7} {_elapsedTime}");
        }

        public static int Compare(Result a, Result b)
        {
            return Convert.ToInt32(a._elapsedTime.TotalMilliseconds - b._elapsedTime.TotalMilliseconds);
        }
    }
}
