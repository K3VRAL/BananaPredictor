using System;
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
    public class BananaSpinPredictorRedo
    {
        private bool flag = false;

        public bool SpinnerPredictor(string path, bool debugging, int startPoint, int endPoint)
        {
            // Read file
            IEnumerable<String> lines = File.ReadLines(path);

            // Map info lines (Used for the file name and such)
            GetMusicInfo MusicInfo = new();
            int bmHitObjects = MusicInfo.GetItemLine("[HitObjects]", lines);
            bmHitObjects++;

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
                        Slider = false,
                        Banana = true,
                        BananaStart = Int32.Parse(amount[2]),
                        BananaEnd = Int32.Parse(amount[5])
                    });
                }/* else if (amount.Length > 7)
                {
                    // Checks for sliders; they use the rng class as well like spinners
                    // According to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#sliders; they are pretty dynamic whereas hitobjects and spinners are static, so I hope this line of code doesn't break anything with other beatmaps
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Slider = true,
                        // The line of code below is broken; this is because I lack knowledge on how sliders works and they are really confusing to work with
                        //SliderInheritedPoint = sp.GoingThroughTimingPoints(lines.Skip(i).First()[2], MusicInfo.GetItemLine("[TimingPoints]", lines) + 1, true, lines),
                        //SliderUninheritedPoint = sp.GoingThroughTimingPoints(lines.Skip(i).First()[2], MusicInfo.GetItemLine("[TimingPoints]", lines) + 1, false, lines),
                        Banana = false
                    });
                }*/
                else
                {
                    // TODO: Fix why normal objects arn't being processed normally
                    // Normal objects added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        Slider = false,
                        Banana = false
                    });
                }

                if (flag)
                    return false;
            }

            int num = AllHitObjects.Count;
            for (int i = num - 1; i >= 0; i--)
            {
                if (AllHitObjects[i].Banana)
                {
                    for (int j = AllHitObjects[i].BananaStart; j < AllHitObjects[i].BananaEnd - 1; j += 30)
                    {
                        AllHitObjects.Add(new GetObjectInfo
                        {
                            Object = "256,192," + j + ",12,0," + (j + 1) + ",0:0:0:0:",
                            Banana = true,
                            Slider = false,
                            BananaStart = j,
                            BananaEnd = j + 1,
                            BananaShowerXOffset = new(),
                            BananaShowerTime = new()
                        });

                        if (flag)
                            return false;
                    }

                    AllHitObjects.RemoveAt(i);
                }

                if (flag)
                    return false;
            }

            // Processing each spinner - The logic for generating the time interval for each banana according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Objects/BananaShower.cs
            foreach (var obj in AllHitObjects)
            {
                if (obj.Banana)
                {
                    String[] getitem = obj.Object.Split(",");
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
                        obj.BananaShowerTime.Add(time);
                        time += spacing;

                        if (flag)
                            return false;
                    }
                }

                if (flag)
                    return false;
            }

            // Mergesort
            MergeSort ms = new();
            AllHitObjects = ms.Merge(AllHitObjects);

            if (AllHitObjects.Equals(null))
                return false;

            // How each banana is processed - The logic the banana xoffset according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Beatmaps/CatchBeatmapProcessor.cs
            var rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED); // Why is the seed 1337?
            //var temp = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
            int indx = 0;
            bool restart = false;
            while (indx < AllHitObjects.Count)
            {
                restart = false;
                Console.WriteLine("Processing {0} in indx {1}", AllHitObjects[indx].Object, indx);
                if (AllHitObjects[indx].Banana)
                {
                    for (int i = 0; i < AllHitObjects[indx].BananaShowerTime.Count; i++)
                    {
                        //temp = rng;
                        double xOffSetCheck = (float)(rng.NextDouble() * CatchPlayfield.WIDTH);
                        //Console.WriteLine("{0} {1}", xOffSetCheck, (xOffSetCheck < startPoint || xOffSetCheck > endPoint));
                        Console.WriteLine("xOffset {0} | Adding new slider {1}", xOffSetCheck, !(xOffSetCheck < startPoint || xOffSetCheck > endPoint));
                        //Console.WriteLine("Banana in {0} with {1}", AllHitObjects[indx].BananaShowerTime[i], xOffSetCheck);
                        if (!(xOffSetCheck < startPoint || xOffSetCheck > endPoint))     // TODO: Figure out why this isn't working
                        {
                            AllHitObjects.Insert(indx, new GetObjectInfo
                            {
                                Object = "256,144," + AllHitObjects[indx].BananaStart + ",6,0,L|256:166,1,20",
                                Banana = false,
                                Slider = true
                            });
                            //rng = temp;
                            // TODO: IT WORKS. IT ACTUALLY WORKS. BUT ITS SO FUCKING INEFFICIENT
                            rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
                            indx = 0;
                            AllHitObjects = ms.Merge(AllHitObjects);
                            restart = true;
                            // TODO: For debugging
                            //for (int k = 0; k < indx; k++)
                            //    if (AllHitObjects[k].Banana && AllHitObjects[k].BananaShowerXOffset.Count > 0)
                            //        AllHitObjects[k].BananaShowerXOffset.Clear();
                            break;
                        }
                        //AllHitObjects[indx].BananaShowerXOffset.Add(xOffSetCheck);
                        rng.Next();
                        rng.Next();
                        rng.Next();
                    }
                    if (!restart)
                        indx++;
                } else if (AllHitObjects[indx].Slider)
                {
                    rng.Next();
                    indx++;
                } else
                    indx++;
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

                if (flag)
                    return false;
            }

            // Put all contents as well as processed hitobjects into osu file
            if (debugging)
            {
                // For debugging
                //ToFilePredictor toFile = new();
                //return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, bmHitObjects);
                return false;
            } else
            {
                // For Final
                ToFileMaker toFile = new();
                return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, bmHitObjects);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stop flag triggered");
            flag = true;
        }

        public bool getFlag()
        {
            return flag;
        }
    }
}