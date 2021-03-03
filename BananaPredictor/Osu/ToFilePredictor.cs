using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BananaPredictor.Osu
{
    // Places beats - used for debugging with Predictor
    public class ToFilePredictor
    {
        public bool OsuToFile(IEnumerable<String> lines, String path, GetMusicInfo MusicInfo, List<GetObjectInfo> AllHitObjects, int bmHitObjects)
        {
            PutTogether pt = new();
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist", lines), lines) + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title", lines), lines) + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator", lines), lines) + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version", lines), lines) + " (BananaPredictor Debugger)].osu";
            File.Create(filename).Close();
            int num = 0;
            using (StreamWriter file = new(filename))
            {
                foreach (var line in lines)
                {
                    if (num == MusicInfo.GetItemLine("Version", lines))
                    {
                        file.WriteLine(line + " (BananaPredictor Debugger)");
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
                    if (line.Banana)
                    {
                        // TODO: Fix this
                        //foreach (var bananaT in line.BananaShowerTime) // Need to get XOffset as well
                        //file.WriteLine(bananaX + ",192," + bananaT + ",1,0,0:0:0:0:");*/
                        // Inefficient alternative, figure out how to use one above - I'll just have to cut my losses if I am unable to solve this
                        List<int> storeX = new();
                        List<int> storeTime = new();
                        foreach (var bananaT in line.BananaShowerTime)
                            storeTime.Add(Convert.ToInt32(Math.Floor(bananaT)));        // Not sure if it should use Floor or Ceiling
                        foreach (var bananaX in line.BananaShowerXOffset)
                            storeX.Add(Convert.ToInt32(Math.Floor(bananaX)));        // Not sure if it should use Floor or Ceiling
                        for (int i = 0; i < storeX.Count; i++)
                            file.WriteLine(storeX[i] + ",192," + storeTime[i] + ",1,0,0:0:0:0:");
                    } else
                        file.WriteLine(line.Object);
                }
            }
            return true;
        }
    }
}
