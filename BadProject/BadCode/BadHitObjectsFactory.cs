using System.Collections.Generic;

namespace BadCode
{
    public class BadHitObjectsFactory
    {
        private BadAppleFrame[] _badAppleFrames;

        public BadHitObjectsFactory(BadAppleFrame[] badAppleFrames)
        {
            _badAppleFrames = badAppleFrames;
        }

        /*public List<string> Build()
        {
            List<string> result = new List<string>();
            foreach(BadAppleFrame frame in _badAppleFrames)
            {
                result.Add(frame.get());
            }
        }*/
    }
}
