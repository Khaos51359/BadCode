using System;
using System.Collections.Generic;

namespace BadCode
{
    public class BadTimingPointsFactory
    {
        private List<BadTimingPoint> _badTimingPoints;

        private int _frameTime = 100;
        private int _deccelerateTime = 41;
        private int _accelerateTime = 41;
        private int _maxLNMillisecond = 18;
        private int _maxKeys = 18;
        private float _accelerateSpeed = 0.69f;
        private int _deccelerateSpeed = 696969699;
        private float _normalSpeed = 800.0f;

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

            int[] frameTimings = GetFrameTimings(frameCount, startTime);

            int[] normalFrameTimings = GetFrameTimings(frameCount, startTime, _accelerateTime);

            List<BadTimingPoint> badTimingPoints =
                GetTimingPoints(frameTimings, _accelerateSpeed);

            List<BadTimingPoint> badNormalTimingPoints =
                GetTimingPoints(normalFrameTimings, _normalSpeed);

            finalTimingPoints.AddRange(badTimingPoints);
            finalTimingPoints.AddRange(badNormalTimingPoints);

            return finalTimingPoints;
        }

        private List<BadTimingPoint> GetTimingPoints(int[] timingPoints, float beatLength)
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
                frameTimings[i] = i * _frameTime + startTime;
            }

            return frameTimings;
        }

        private int[] GetFrameTimings(int frameCount, int startTime, int offset)
        {
            int[] frameTimings = new int[frameCount];

            for(int i = 0; i < frameCount; i++)
            {
                frameTimings[i] = i * _frameTime + startTime + offset;
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
                timer += _frameTime;
            }

            return frameCounter;
        }
    }
}
