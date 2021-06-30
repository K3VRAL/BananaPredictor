using System;
using System.Collections.Generic;

namespace BananaPredictor.Osu
{
    public class GetMusicInfo
    {
        public IEnumerable<String> Path { get; set; }
        public int GetItemLine(String item)
        {
            int lineindex = 0;
            foreach (var line in Path)
            {
                if (line.Contains(item))
                    return lineindex;
                lineindex++;
            }
            return -1;
        }
    }
}
