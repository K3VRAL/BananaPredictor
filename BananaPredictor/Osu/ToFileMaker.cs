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
            // TODO: Remove this and add end with true - return true;
            return false;
            PutTogether pt = new();
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist"), lines) + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title"), lines) + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator"), lines) + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version"), lines) + " (BananaPredictor)].osu";
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
                    if (num.Equals(bmHitObjects))
                    {
                        break;
                    }
                    file.WriteLine(line);
                    num++;
                }

                foreach (var line in AllHitObjects)
                {
                    switch (line.Banana)
                    {
                        case true:
                            // TODO: Figure this out
                            break;
                        default:
                            file.WriteLine(line.Object);
                            break;
                    }
                }
            }
        }
    }
}
