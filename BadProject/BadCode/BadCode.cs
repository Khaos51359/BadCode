using System.Collections.Generic;

namespace BadCode
{
    public class BadCode
    {
        public static void Main()
        {
            BadVideo badVideo = new BadVideo("BadApple.mp4");

            BadAppleFrame[] badAppleFrames = badVideo.GetBadAppleFrames(
                BadTimingPointsFactory.FRAME_TIME);

            for (int i = 0; i < BadAppleFrame.WIDTH; i++)
            {
                string s = string.Empty;

                for (int j = 0; j < BadAppleFrame.HEIGHT; j++)
                {
                    if (badAppleFrames[3740].GetFrame()[j,i] == (byte)255)
                    {
                        s += "O";
                    }
                    else
                    {
                        s += " ";
                    }
                }
                System.Console.WriteLine(s);
            }

            System.Console.WriteLine("video length = " + badVideo.GetVideoLength());

            List<BadTimingPoint> timings = new BadTimingPointsFactory().Build(2000, 3000);

            List<string> output = new List<string>();

            foreach( BadTimingPoint timingPoint in timings)
            {
                output.Add(timingPoint.GetBadTimingPointString());
            }
            BadFileProcessor fp = new BadFileProcessor();
            fp.Write(output);
        }
    }
}
