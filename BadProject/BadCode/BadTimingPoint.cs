namespace BadCode
{
    public class BadTimingPoint
    {
        private int _timing;
        private float _beatLength;
        private bool _uninherited;

        public const int METER = 4;
        public const int SAMPLE_SET = 2;
        public const int VOLUME = 0;
        public const int UNINHERITED = 1;
        public const int INHERITED = 0;
        public const int EFFECT = 0;

        public BadTimingPoint (int timing, float beatLength, bool uninherited)
        {
            _uninherited = uninherited;
            _beatLength = beatLength;
            _timing = timing;
        }

        public void SetTiming(int timing)
        {
            _timing = timing;
        }

        public int GetTiming()
        {
            return _timing;
        }

        public void SetBeatLength(float beatLength)
        {
            _beatLength = beatLength;
        }

        public float GetBeatLength()
        {
            return _beatLength;
        }

        public string GetBadTimingPoint()
        {
            int uninherited = _uninherited ? 1 : 0;

            return $"{_timing},{_beatLength},{METER},{SAMPLE_SET}," +
                   $"{VOLUME},{uninherited},{EFFECT}";
        }
    }
}
