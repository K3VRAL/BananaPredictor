using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;
using osu.Game.Rulesets.Catch.Beatmaps;

namespace BananaPredictor.Osu
{
    // TODO: Fix inefficient method
    public class BananaSpinPredictor
    {
        public bool SpinnerPredictor(string path)
        {
            // Read file
            IEnumerable<String> lines = File.ReadLines(path);

            // Map info lines (Used for the file name and such)
            GetMusicInfo MusicInfo = new();
            MusicInfo.Path = path;
            int bmHitObjects = MusicInfo.GetItemLine("[HitObjects]");

            // If not found any
            if (bmHitObjects < 0)
                return false;

            // Storing all spinners and objects found into list
            var AllHitObjects = new List<GetObjectInfo>();
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length.Equals(7) && Int32.Parse(amount[0]).Equals(256) && Int32.Parse(amount[1]).Equals(192))
                {
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Banana = true,
                        BananaShowerTime = new List<double>(),
                        BananaShowerXOffset = new List<double>()
                    });
                    continue;
                }
                AllHitObjects.Add(new GetObjectInfo { 
                    Object = lines.Skip(i).First(),
                    Banana = false
                });
            }

            // Processing each spinner - The logic for generating the time interval for each banana according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Objects/BananaShower.cs
            foreach (var item in AllHitObjects)
            {
                switch (item.Banana)
                {
                    case true:
                        String[] getitem = item.Object.Split(",");
                        double time = Int32.Parse(getitem[2]);
                        double endtime = Int32.Parse(getitem[5]);
                        int duration = Int32.Parse(getitem[5]) - Int32.Parse(getitem[2]);

                        double spacing = duration;
                        while (spacing > 100)
                            spacing /= 2;
                        if (spacing <= 0)
                            continue;

                        while (time <= endtime)
                        {
                            item.BananaShowerTime.Add(time);

                            time += spacing;
                        }
                        break;
                }
            }

            // How each banana is processed - The logic the banana xoffset according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Beatmaps/CatchBeatmapProcessor.cs
            var rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
            foreach (var obj in AllHitObjects)
            {
                if (obj.Banana)
                    for (int i = 0; i < obj.BananaShowerTime.Count; i++)
                    {
                        obj.BananaShowerXOffset.Add((float)(rng.NextDouble() * CatchPlayfield.WIDTH));
                        rng.Next();
                        rng.Next();
                        rng.Next();
                    }
            }

            // Put all contents as well as processed hitobjects into osu file
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + this.PutTogether(MusicInfo.GetItemLine("Artist"), lines) + " - " + this.PutTogether(MusicInfo.GetItemLine("Title"), lines) + " (" + this.PutTogether(MusicInfo.GetItemLine("Creator"), lines) + ") [" + this.PutTogether(MusicInfo.GetItemLine("Version"), lines) + " BananaPredictor].osu";
            File.Create(filename).Close();
            int num = 0;
            using (StreamWriter file = new(filename))
            {
                foreach (var line in lines)
                {
                    if (num == MusicInfo.GetItemLine("Version"))
                    {
                        file.WriteLine(line + " BananaPredictor");
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
                            /*foreach (var bananaT in line.BananaShowerTime) // Need to get XOffset as well
                                file.WriteLine(bananaX + ",192," + bananaT + ",1,0,0:0:0:0:"); // How do I fix this? Need to use BST and BSXO but can only use one at a time*/
                            // TODO: Inefficient alternative
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

        private String PutTogether(int bmNumber, IEnumerable<String> lines)
        {
            return String.Join("", lines.Skip(bmNumber).Take(1).First().Split(':').Skip(1));
        }
    }
}