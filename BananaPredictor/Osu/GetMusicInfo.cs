using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    public class GetMusicInfo
    {
        public String Path { get; set; }
        public int GetItemLine(String item)
        {
            int bmHitObjects = 0;
            foreach (var line in File.ReadLines(Path))
            {
                // Get map info
                if (line.Contains(item))
                    return bmHitObjects;
                bmHitObjects++;
            }
            return -1;
        }
    }
}
