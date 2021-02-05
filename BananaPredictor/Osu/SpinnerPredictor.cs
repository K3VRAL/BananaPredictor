using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;
using osu.Game.Rulesets.Catch.Beatmaps;

namespace BananaPredictor.Osu
{
    // Apologies for the terrible code/not using other libraries such as OsuPrasers to get the job done
    public class BananaSpinPredictor
    {
        private IEnumerable<String> lines;
        public bool SpinnerPredictor(string path)
        {
            // Read file
            lines = File.ReadLines(path);

            // Map info lines (Used for the file name and such)
            GetMusicInfo MusicInfo = new();
            MusicInfo.Path = path;
            int bmHitObjects = MusicInfo.GetItemLine("[HitObjects]");

            // If not found any
            if (bmHitObjects < 0)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't Find [HitObjects] | Count: {0} File: {1}", bmHitObjects, lines.Count());
                return false;
            }

            // Storing all spinners and objects found into list
            var HitObjects = new List<GetObjectInfo>();
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length.Equals(7) && Int32.Parse(amount[0]).Equals(256) && Int32.Parse(amount[1]).Equals(192))
                {
                    HitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Banana = true,
                        BananaShowerTime = new List<double>(),
                        BananaShowerXOffset = new List<double>()
                    });
                    continue;
                }
                HitObjects.Add(new GetObjectInfo { 
                    Object = lines.Skip(i).First(),
                    Banana = false
                });
            }

            // Processing each spinner - The logic for generating the time interval for each banana according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Objects/BananaShower.cs
            foreach (var item in HitObjects)
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
            foreach (var obj in HitObjects)
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
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + this.PutTogether(MusicInfo.GetItemLine("Artist")) + " - " + this.PutTogether(MusicInfo.GetItemLine("Title")) + " (" + this.PutTogether(MusicInfo.GetItemLine("Creator")) + ") [" + this.PutTogether(MusicInfo.GetItemLine("Version")) + " BananaPredictor].osu";
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
                
                foreach (var line in HitObjects)
                {
                    switch (line.Banana)
                    {
                        case true:
                            /*foreach (var bananaT in line.BananaShowerTime) // Need to get XOffset as well
                                file.WriteLine(bananaX + ",192," + bananaT + ",1,0,0:0:0:0:"); // How do I fix this? Need to use BST and BSXO but can only use one at a time*/
                            // Inefficient alternative
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

        private String PutTogether(int bmNumber)
        {
            return String.Join("", lines.Skip(bmNumber).Take(1).First().Split(':').Skip(1));
        }
    }
}