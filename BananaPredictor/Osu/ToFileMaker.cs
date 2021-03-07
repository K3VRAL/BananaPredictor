using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BananaPredictor.Osu
{
    public class ToFileMaker
    {
        public bool OsuToFile(IEnumerable<String> lines, String path, GetMusicInfo MusicInfo, List<GetObjectInfo> AllHitObjects, int bmHitObjects)
        {
            PutTogether pt = new();
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray())
                + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist", lines), lines)
                + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title", lines), lines)
                + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator", lines), lines)
                + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version", lines), lines)
                + " (BananaPredictor)].osu";
            File.Create(filename).Close();
            int num = 0;
            using (StreamWriter file = new(filename))
            {
                foreach (var line in lines)
                {
                    if (num == MusicInfo.GetItemLine("Version", lines))
                    {
                        file.WriteLine(line + " (BananaPredictor)");
                        num++;
                        continue;
                    }
                    if (num.Equals(bmHitObjects))
                    {
                        break;
                    }
                    file.WriteLine(line);
                    num++;
                }

                foreach (var line in AllHitObjects)
                {
                    file.WriteLine(line.Object);
                }
            }
            return true;
        }
    }
}
