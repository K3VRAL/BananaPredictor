﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;
using osu.Game.Rulesets.Catch.Beatmaps;
using osu.Game.Screens.Play;

namespace BananaPredictor.Osu
{
    // Used for debugging if program/initial logic works
    public class BananaSpinPredictor
    {
        public bool SpinnerPredictor(string path, bool debugging, int startPoint, int endPoint)
        {
            // Read file
            IEnumerable<String> lines = File.ReadLines(path);

            // Map info lines (Used for the file name and such)
            GetMusicInfo MusicInfo = new();
            int bmHitObjects = MusicInfo.GetItemLine("[HitObjects]", lines);

            // If not found any
            if (bmHitObjects < 0)
                return false;

            // Storing all spinners and objects found into list
            List<GetObjectInfo> AllHitObjects = new();
            //SliderProcess sp = new();
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length.Equals(7) && Int32.Parse(amount[0]).Equals(256) && Int32.Parse(amount[1]).Equals(192))
                {
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        //Slider = false,
                        Banana = true,
                        BananaShowerTime = new List<double>(),
                        BananaShowerXOffset = new List<double>()
                    });
                } else if (amount.Length > 7)
                {
                    // Checks for sliders; they use the rng class as well like spinners
                    // According to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#sliders; they are pretty dynamic whereas hitobjects and spinners are static, so I hope this line of code doesn't break anything with other beatmaps
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        //Slider = true,
                        // The line of code below is broken; this is because I lack knowledge on how sliders works and they are really confusing to work with
                        //SliderInheritedPoint = sp.GoingThroughTimingPoints(lines.Skip(i).First()[2], MusicInfo.GetItemLine("[TimingPoints]", lines) + 1, true, lines),
                        //SliderUninheritedPoint = sp.GoingThroughTimingPoints(lines.Skip(i).First()[2], MusicInfo.GetItemLine("[TimingPoints]", lines) + 1, false, lines),
                        Banana = false
                    });
                }
                else
                {
                    // Normal objects added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        //Slider = false,
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
            var rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED); // Why is the seed 1337?
            var temp = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
            bool wentwrong = false;
            foreach (var obj in AllHitObjects)
            {
                if (obj.Banana)
                {
                    for (int i = 0; i < obj.BananaShowerTime.Count; i++)
                    {
                        double xOffSetCheck = (float)(rng.NextDouble() * CatchPlayfield.WIDTH);
                        if (xOffSetCheck < startPoint && xOffSetCheck > endPoint)     // TODO: Figure out why this isn't working
                        {
                            rng = temp;
                            obj.BananaShowerXOffset.Clear();
                            wentwrong = true;
                            break;
                        }
                        obj.BananaShowerXOffset.Add(xOffSetCheck);
                        rng.Next();
                        rng.Next();
                        rng.Next();
                    }
                    if (!wentwrong)
                    {
                        for (int i = 0; i < obj.BananaShowerXOffset.Count; i++)
                        {
                            temp.NextDouble();
                            temp.Next();
                            temp.Next();
                            temp.Next();
                        }
                    }
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