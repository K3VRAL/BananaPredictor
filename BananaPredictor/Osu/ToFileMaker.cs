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
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + pt.PutLineTogether(MusicInfo.GetItemLine("Artist", lines), lines) + " - " + pt.PutLineTogether(MusicInfo.GetItemLine("Title", lines), lines) + " (" + pt.PutLineTogether(MusicInfo.GetItemLine("Creator", lines), lines) + ") [" + pt.PutLineTogether(MusicInfo.GetItemLine("Version", lines), lines) + " (BananaPredictor)].osu";
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
                    /*if (line.Banana)
                    {
                        // TODO: Figure this out with sliders as well
                        //List<int> store = new();
                        //foreach (var bananaT in line.BananaShowerTime)
                        //    store.Add(Convert.ToInt32(Math.Floor(bananaT)));        // Not sure if it should use Floor or Ceiling
                        //foreach (var bananaX in line.BananaShowerXOffset)
                        //    store.Add(Convert.ToInt32(Math.Floor(bananaX)));        // Not sure if it should use Floor or Ceiling
                        file.WriteLine(line.Object);
                    } else
                    {
                        file.WriteLine(line.Object);
                    }*/
                    file.WriteLine(line.Object);
                }
                // TODO: Add in final hitobject for platter reset
                //file.WriteLine("256,192," + AllHitObjects[AllHitObjects.Count - 1].BananaEnd + ",5,0,0:0:0:0:");
            }
            return true;
        }
    }
}
