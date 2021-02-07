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
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist"), lines) + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title"), lines) + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator"), lines) + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version"), lines) + " (BananaPredictor Debugger)].osu";
            File.Create(filename).Close();
            int num = 0;
            using (StreamWriter file = new(filename))
            {
                foreach (var line in lines)
                {
                    if (num == MusicInfo.GetItemLine("Version"))
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
                    switch (line.Banana)
                    {
                        case true:
                            //foreach (var bananaT in line.BananaShowerTime) // Need to get XOffset as well
                                //file.WriteLine(bananaX + ",192," + bananaT + ",1,0,0:0:0:0:");*/
                            // Inefficient alternative, figure out how to use one above - I'll just have to cut my losses if I am unable to solve this
                            List<int> store = new();
                            foreach (var bananaT in line.BananaShowerTime)
                                store.Add(Convert.ToInt32(Math.Floor(bananaT)));        // Not sure if it should use Floor or Ceiling
                            foreach (var bananaX in line.BananaShowerXOffset)
                                store.Add(Convert.ToInt32(Math.Floor(bananaX)));        // Not sure if it should use Floor or Ceiling
                            for (int i = 0; i < store.Count / 2; i++)
                                file.WriteLine(store[(store.Count / 2) + i] + ",192," + store[i] + ",1,0,0:0:0:0:");
                            break;
                        default:
                            file.WriteLine(line.Object);
                            break;
                    }
                }
            }
            return true;
        }
    }
}
