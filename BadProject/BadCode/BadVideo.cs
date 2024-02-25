using System;
using System.Collections.Generic;
using System.IO;
using OpenCvSharp;

namespace BadCode
{
    public class BadVideo
    {
        private  VideoCapture _badVideo;
        private int _badVideoFrameCount;
        private float _badVideoFPS;
        private int _badVideoWidth;
        private int _badVideoHeight;
        private int _badVideoLength;
        public const float FRAME_CONVERT_PERCENTAGE_TREESHOLD = 0.3f;

        public BadVideo(string fileName)
        {
            VideoCapture vid = new VideoCapture(fileName);

            if(!vid.IsOpened())
            {
                Console.WriteLine("[BadLog] cannot open " +
                        Directory.GetCurrentDirectory() + fileName
                        );
                return;
            }

            _badVideo = vid;
            _badVideoFrameCount = (int)vid.Get(VideoCaptureProperties.FrameCount);
            _badVideoFPS = (int)vid.Get(VideoCaptureProperties.Fps);
            _badVideoWidth = (int)vid.Get(VideoCaptureProperties.FrameWidth);
            _badVideoHeight = (int)vid.Get(VideoCaptureProperties.FrameHeight);
            _badVideoLength = (int)(_badVideoFrameCount / _badVideoFPS);

        }

        ~BadVideo()
        {
             _badVideo.Release();
        }

        public Mat[] GetBadVideoMats()
        {
            Mat[] allMats = new Mat[_badVideoFrameCount];

            for(int i = 0; i < allMats.Length; i++)
            {
                allMats[i] = new Mat();
                _badVideo.Read(allMats[i]);
            }

            _badVideo.Set(VideoCaptureProperties.PosFrames, 0);

            return allMats;
        }

        public BadAppleFrame[] GetBadAppleFrames(int frameTime)
        {
            Mat[] mat = GetBadVideoMats();

            float originalFrameTime = 1000 / _badVideoFPS;


            BadAppleFrame[] badAppleFrames =
                new BadAppleFrame[GetBadFrameCount(frameTime)];

            for (int i = 0; i < badAppleFrames.Length; i++)
            {

                int nearestOriginalFrame = GetNearestFrame(i, frameTime, 1000 / _badVideoFPS);
                badAppleFrames[i] = ConvertRawFrame(mat[nearestOriginalFrame]);
            }

            return badAppleFrames;
        }

        private int GetNearestFrame(int index, float badTiming, float originalTiming)
        {
            if (index == 0)
            {
                return 0;
            }

            int target = (int)MathF.Round(index * badTiming);

            float smallerDiff = float.MaxValue;

            int i = 0;

            while (MathF.Abs(smallerDiff) >= MathF.Abs((i * originalTiming) - target))
            {
                smallerDiff = (i * originalTiming) - target;
                i++;
            }

            return i - 1;

        }


        private BadAppleFrame ConvertRawFrame(Mat frame)
        {
            int badFrameWidth = BadAppleFrame.WIDTH;
            int badFrameHeight = BadAppleFrame.HEIGHT;

            short[,] badFrame = new short[badFrameWidth, badFrameHeight];

            int totalPixelPerBadFrameWidth = (int)(_badVideoWidth / badFrameWidth);
            int totalPixelPerBadFrameHeight = (int)(_badVideoHeight / badFrameHeight);
            int totalPixelPerBadFrame = totalPixelPerBadFrameWidth +
                totalPixelPerBadFrameHeight;

            for (int i = 0; i < badFrameWidth; i++)
            {
                for (int j = 0; j < badFrameHeight; j++)
                {
                    int[] pixelAddress = new int[]{i,j};

                    badFrame[i,j] = ConvertToBadPixel(pixelAddress, frame,
                        totalPixelPerBadFrameWidth, totalPixelPerBadFrameHeight);
                }
            }

            return new BadAppleFrame(badFrame);
        }

        private byte ConvertToBadPixel(int[] pixelAddress, Mat frame, int width,
                int height)
        {
            int pixelColumnStart = pixelAddress[0] * width;
            int pixelColumnEnd = pixelColumnStart + width;
            int pixelRowStart = pixelAddress[1] * height;
            int pixelRowEnd = pixelRowStart + height - 1;

            int originalPixelPerBadFrame = width * height;
            int whitePixelCounter = 0;

            for (int i = pixelRowStart; i < pixelRowEnd ; i++)
            {
                for (int j = pixelColumnStart; j < pixelColumnEnd ; j++)
                {
                    if (frame.At<byte>(i,j) >= 100)
                        whitePixelCounter++;
                }
            }

            int pixelTreeshold =
                (int)(FRAME_CONVERT_PERCENTAGE_TREESHOLD * originalPixelPerBadFrame);

            bool isWhite = whitePixelCounter > pixelTreeshold;

            return isWhite ? (byte)255:(byte)0;
        }

        private int GetBadFrameCount(int frameTime)
        {
            return _badVideoLength * 1000 / frameTime;
        }


        public int GetFrameCount()
        {
            return _badVideoFrameCount;
        }

        public int GetVideoLength()
        {
            return _badVideoLength;
        }


        public float GetFPS()
        {
            return _badVideoFPS;
        }

    }
}
