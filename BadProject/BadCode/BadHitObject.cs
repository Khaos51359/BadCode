using System.Collections.Generic;

namespace BadCode
{
    public class BadHitObject
    {
        private Dictionary<int, int> _xFromPos = new Dictionary<int, int>()
        {
            {0, 14},
            {1, 42},
            {2, 71},
            {3, 99},
            {4, 128},
            {5, 156},
            {6, 184},
            {7, 213},
            {8, 241},
            {9, 270},
            {10, 298},
            {11, 327},
            {12, 355},
            {13, 384},
            {14, 412},
            {15, 440},
            {16, 469},
            {17, 497}
        };

        private int _time;
        private int _endTime;
        private string _hitSample = "0:0:0:0:";
        private string _hitSound = "0";
        private string _type = "128";
        private int _x;
        private const int _y = 192;

        public BadHitObject(int column, int time, int endTime)
        {
           _x = _xFromPos[column];
           _time = time;
           _endTime = endTime;

        }

        public string GetString()
        {
            return _x + "," + _y + "," + _time + "," +
                _type + "," + _hitSound + "," + _endTime + ":"
                + _hitSample;
        }
    }
}
