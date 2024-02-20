using System;
using System.IO;
using OpenCvSharp;

namespace BadCode
{
    public class BadVideo
    {
        public BadVideo(string fileName)
        {
            using (VideoCapture vid = new VideoCapture(fileName))
            {
                if(!vid.IsOpened())
                {
                    Console.WriteLine("[BadLog] cannot open file named" +
                        fileName + " in " + Directory.GetCurrentDirectory()
                    );
                    return;
                }
                Console.WriteLine(vid.FrameWidth + "x" + vid.FrameHeight);
            }
        }
    }
}
