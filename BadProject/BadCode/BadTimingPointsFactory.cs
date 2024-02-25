using System.Linq;
using System.Collections.Generic;

namespace BadCode
{
    public class BadTimingPointsFactory
    {
        private class X
        {
            public int x;
        }
        private List<BadTimingPoint> _badTimingPoints;

        public const int FRAME_TIME = 50;
        private int _decelerateTimeOffset = 20;
        private int _accelerateTimeOffset = 19;
        private int _maxLNMillisecond = 18;
        private int _maxKeys = 18;
        private string _accelerateSpeed = "1E-69";
        private string _decelerateSpeed = "1E+69";
        private string _normalSpeed = "10000";

        private Dictionary<int, int> _xFromPos = new Dictionary<int, int>()
        {
            {1, 14},
            {2, 42},
            {3, 71},
            {4, 99},
            {5, 128},
            {6, 156},
            {7, 184},
            {8, 213},
            {9, 241},
            {10, 270},
            {11, 298},
            {12, 327},
            {13, 355},
            {14, 384},
            {15, 412},
            {16, 440},
            {17, 469},
            {18, 497}
        };

        public BadTimingPointsFactory()
        {
            _badTimingPoints = new List<BadTimingPoint>();
        }

        public List<BadTimingPoint> Build(int startTime, int endTime)
        {
            List<BadTimingPoint> finalTimingPoints =
                new List<BadTimingPoint>();

            int frameCount = GetFrameCounts(startTime, endTime);

            int[] normalFrameTimings = GetFrameTimings(frameCount, startTime);

            int[] accelerateFrameTimings = GetFrameTimings(frameCount, startTime, _accelerateTimeOffset);

            int[] decelerateFrameTimings = GetFrameTimings(frameCount, startTime, _decelerateTimeOffset);

            List<BadTimingPoint> badDecelerateTimingPoints =
                GetTimingPoints(decelerateFrameTimings, _decelerateSpeed);

            List<BadTimingPoint> badNormalTimingPoints =
                GetTimingPoints(normalFrameTimings, _normalSpeed);

            List<BadTimingPoint> badAccelerateTimingPoints =
                GetTimingPoints(accelerateFrameTimings, _accelerateSpeed);

            finalTimingPoints.AddRange(badDecelerateTimingPoints);
            finalTimingPoints.AddRange(badNormalTimingPoints);
            finalTimingPoints.AddRange(badAccelerateTimingPoints);

            var sorted =
                finalTimingPoints.
                OrderBy(point => point.GetTiming()).ToList();

            return sorted;
        }

        private List<BadTimingPoint> GetTimingPoints(int[] timingPoints, string beatLength)
        {
            List<BadTimingPoint> badTimingPoints = new List<BadTimingPoint>();

            foreach (int frameTime in timingPoints)
            {
                BadTimingPoint badTimingPoint = new BadTimingPoint(frameTime,
                        beatLength, true);
                badTimingPoints.Add(badTimingPoint);
            }

            return badTimingPoints;
        }


        private int[] GetFrameTimings(int frameCount, int startTime)
        {
            int[] frameTimings = new int[frameCount];

            for(int i = 0; i < frameCount; i++)
            {
                frameTimings[i] = i * FRAME_TIME + startTime;
            }

            return frameTimings;
        }

        private int[] GetFrameTimings(int frameCount, int startTime, int offset)
        {
            int[] frameTimings = new int[frameCount];

            for(int i = 0; i < frameCount; i++)
            {
                frameTimings[i] = i * FRAME_TIME + startTime + offset;
            }

            return frameTimings;
        }

        private int GetFrameCounts(int startTime, int endTime)
        {
            int frameCounter = 0;
            int timer = startTime;
            while(timer <= endTime)
            {
                frameCounter++;
                timer += FRAME_TIME;
            }

            return frameCounter;
        }
    }
}
