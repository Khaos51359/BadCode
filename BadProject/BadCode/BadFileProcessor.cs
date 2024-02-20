using System;
using System.Collections.Generic;
using System.IO;

namespace BadCode
{
    public class BadFileProcessor
    {
        public bool Write (List<string> strings)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter("BadLN.txt"))
                {
                    foreach(string s in strings)
                    {
                        writer.WriteLine(s);
                    }
                }
                return true;
            }
            catch
            {
                Console.WriteLine("[BadLog] unable to write file");
                return false;
            }
        }
    }
}

