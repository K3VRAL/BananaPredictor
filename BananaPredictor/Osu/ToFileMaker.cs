using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BananaPredictor.Osu
{
    public class ToFileMaker
    {
        public bool OsuToFile(IEnumerable<String> lines, String path, GetMusicInfo MusicInfo, List<GetObjectInfo> AllHitObjects, List<GetMapInfo> AllTimingPoints, int bmHitObjects, int bmTimingPoints)
        {
            PutTogether pt = new();
            pt.Path = lines;
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray())
                + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist"))
                + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title"))
                + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator"))
                + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version"))
                + " (BananaPredictor)].osu";
            File.Create(filename).Close();
            int num = 0;
            using (StreamWriter file = new(filename))
            {
                foreach (var line in lines)
                {
                    if (num == MusicInfo.GetItemLine("Version"))
                    {
                        file.WriteLine(line + " (BananaPredictor)");
                        num++;
                        continue;
                    }
                    if (num == MusicInfo.GetItemLine("Tags"))
                    {
                        file.WriteLine(line + " BananaPredictor");
                        num++;
                        continue;
                    }
                    if (num == bmHitObjects)
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
