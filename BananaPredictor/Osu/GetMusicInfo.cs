﻿using System;
using System.Collections.Generic;
using System.IO;

namespace BananaPredictor.Osu
{
    public class GetMusicInfo
    {
        public int GetItemLine(String item, IEnumerable<String> Path)
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
