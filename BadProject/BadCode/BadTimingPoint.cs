namespace BadCode
{
    public class BadTimingPoint
    {
        private int _time;
        private string _beatLength;
        private bool _uninherited;

        public const int METER = 4;
        public const int SAMPLE_SET = 2;
        public const int SAMPLE_INDEX = 0;
        public const int VOLUME = 0;
        public const int EFFECT = 0;

        public BadTimingPoint (int timing, string beatLength, bool uninherited)
        {
            _uninherited = uninherited;
            _beatLength = beatLength;
            _time = timing;
        }

        public void SetTiming(int timing)
        {
            _time = timing;
        }

        public int GetTiming()
        {
            return _time;
        }

        public void SetBeatLength(string beatLength)
        {
            _beatLength = beatLength;
        }

        public string GetBeatLength()
        {
            return _beatLength;
        }

        public string GetBadTimingPointString()
        {
            int uninherited = _uninherited ? 1 : 0;

            return $"{_time},{_beatLength},{METER},{SAMPLE_SET}," +
                   $"{SAMPLE_INDEX},{VOLUME},{uninherited},{EFFECT}";
        }
    }
}
