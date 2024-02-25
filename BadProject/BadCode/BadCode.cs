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

            System.Console.WriteLine("video length = " + badVideo.GetVideoLength());

            PlayInConsole(badAppleFrames);

            List<BadTimingPoint> timings = new BadTimingPointsFactory().Build(2000, 3000);

            List<string> output = new List<string>();

            foreach( BadTimingPoint timingPoint in timings)
            {
                output.Add(timingPoint.GetBadTimingPointString());
            }
            BadFileProcessor fp = new BadFileProcessor();
            fp.Write(output);
        }

        private static void PlayInConsole(BadAppleFrame[] badAppleFrames)
        {
            for (int i = 0; i < badAppleFrames.Length; i++)
            {

                System.Console.Clear();

                System.Console.WriteLine(GetFrameString(badAppleFrames[i]));

                System.Console.WriteLine("render frame " + i);
                System.Threading.Thread.Sleep(30);
            }
        }

        private static string GetFrameString(BadAppleFrame frame)
        {
            string s = string.Empty;

            for (int i = 0; i < BadAppleFrame.WIDTH; i++)
            {

                for (int j = 0; j < BadAppleFrame.HEIGHT; j++)
                {
                    if (frame.GetFrame()[j,i] == (byte)255)
                    {
                        s += "O";
                    }
                    else
                    {
                        s += " ";
                    }
                }
                s += "\n";
            }

            return s;
        }
    }
}
