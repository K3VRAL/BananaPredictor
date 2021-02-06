using System;
using System.IO;

namespace BananaPredictor.Osu
{
    public class GetMusicInfo
    {
        public String Path { get; set; }
        public int GetItemLine(String item)
        {
            int lineindex = 0;
            foreach (var line in File.ReadLines(Path))
            {
                // Get map info
                if (line.Contains(item))
                    return lineindex;
                lineindex++;
            }
            return -1;
        }
    }
}
