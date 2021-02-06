using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;
using osu.Game.Rulesets.Catch.Beatmaps;

namespace BananaPredictor.Osu
{
    // Used for debugging if program/initial logic works
    public class BananaSpinPredictor
    {
        public bool SpinnerPredictor(string path, bool debugging)
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
            List<GetObjectInfo> AllHitObjects = new();
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length.Equals(7) && Int32.Parse(amount[0]).Equals(256) && Int32.Parse(amount[1]).Equals(192))
                {
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Slider = false,
                        Banana = true,
                        BananaShowerTime = new List<double>(),
                        BananaShowerXOffset = new List<double>()
                    });
                } else if (amount.Length > 8 && amount[5].Contains("|"))    // TODO: JuiceStream has RNG as well, need to fix
                {
                    // Checks for sliders; they use the rng class as well like spinners
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Slider = true,
                        Banana = false
                    });
                }
                else
                {
                    // Normal objects added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Slider = false,
                        Banana = false
                    });
                }
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
                // TODO: Need to code in how it works
                //else if (obj.Slider)
                //{
                    //foreach(var item in obj.SliderList)
                    //{
                        //else if (item.TinyDroplet)
                            //rng.Next(20, 20);
                        //else if (item.Droplet)
                            //rng.Next();
                    //}
                //}
            }

            // Put all contents as well as processed hitobjects into osu file
            if (debugging)
            {
                // For debugging
                ToFilePredictor toFile = new();
                return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, bmHitObjects);
            } else
            {
                // For Final
                ToFileMaker toFile = new();
                return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, bmHitObjects);
            }
        }
    }
}