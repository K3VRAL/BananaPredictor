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
        public bool debug = false, newSpinner = false;
        public int startTime, endTime, startPos, endPos;

        private bool flag = false;

        public bool SpinnerPredictor(string path)
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
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length.Equals(7)
                    && Int32.Parse(amount[0]).Equals(256)
                    && Int32.Parse(amount[1]).Equals(192))
                {
                    // Spinner added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Spinner,
                        BananaStart = Int32.Parse(amount[2]),
                        BananaEnd = Int32.Parse(amount[5])
                    });
                } // TODO: For sliders
                /* else if (amount.Length > 7)
                {
                    // Checks for sliders; they use the rng class as well like spinners
                    // According to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#sliders; they are pretty dynamic whereas hitobjects and spinners are static, so I hope this line of code doesn't break anything with other beatmaps
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Slider
                    });
                }*/
                else
                {
                    // Hitobject added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Normal
                    });
                }

                if (flag)
                    return false;
            }

            // TODO: Fix this later
            if (newSpinner)
            {
                AllHitObjects.Add(new GetObjectInfo
                {
                    Object = "256,192," + startTime + ",12,0," + endTime + ",0:0:0:0:",
                    OType = GetObjectInfo.Type.Spinner,
                    BananaStart = startTime,
                    BananaEnd = endTime,
                    BananaShowerTime = new()
                });
            }

            int num = AllHitObjects.Count;
            for (int i = num - 1; i >= 0; i--)
            {
                if (AllHitObjects[i].OType.Equals(GetObjectInfo.Type.Spinner)
                    && AllHitObjects[i].BananaStart.Equals(startTime)
                    && AllHitObjects[i].BananaEnd.Equals(endTime))
                {
                    for (int j = AllHitObjects[i].BananaStart; j < AllHitObjects[i].BananaEnd - 1; j += 60)
                    {
                        AllHitObjects.Add(new GetObjectInfo
                        {
                            Object = "256,192," + j + ",12,0," + (j + 1) + ",0:0:0:0:",
                            OType = GetObjectInfo.Type.Spinner,
                            BananaStart = j,
                            BananaEnd = j + 1,
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
                if (obj.OType.Equals(GetObjectInfo.Type.Spinner)
                    && obj.BananaStart.Equals(startTime)
                    && obj.BananaEnd.Equals(endTime))
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
            FastRandom rng = new(CatchBeatmapProcessor.RNG_SEED); // Why is the seed 1337?
            //FastRandom temp = new(CatchBeatmapProcessor.RNG_SEED);
            int indx = 0;
            bool restart = false;
            while (indx < AllHitObjects.Count)
            {
                restart = false;
                Console.WriteLine("Processing {0} in indx {1}", AllHitObjects[indx].Object, indx);
                if (AllHitObjects[indx].OType.Equals(GetObjectInfo.Type.Spinner)
                    && AllHitObjects[indx].BananaStart.Equals(startTime)
                    && AllHitObjects[indx].BananaEnd.Equals(endTime))
                {
                    for (int i = 0; i < AllHitObjects[indx].BananaShowerTime.Count; i++)
                    {
                        double xOffSetCheck = (float)(rng.NextDouble() * CatchPlayfield.WIDTH);
                        Console.WriteLine("xOffset {0} | Adding new slider {1}", xOffSetCheck, !(xOffSetCheck < startPos || xOffSetCheck > endPos));
                        if (!(xOffSetCheck < startPos || xOffSetCheck > endPos))
                        {
                            AllHitObjects.Insert(indx, new GetObjectInfo
                            {
                                Object = "256,144," + AllHitObjects[indx].BananaStart + ",6,0,L|256:166,1,20",
                                OType = GetObjectInfo.Type.Slider
                            });
                            //rng = temp;
                            // TODO: IT WORKS. IT ACTUALLY WORKS. BUT ITS SO FUCKING INEFFICIENT. USE TEMPS INSTEAD OF RESETTING. HUGE MEMORY LEAKS
                            // If I try using temp, the whole thing breaks and I get different values.
                            rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
                            indx = 0;
                            AllHitObjects = ms.Merge(AllHitObjects);
                            restart = true;
                            break;
                        }
                        // TODO: For debugging
                        //AllHitObjects[indx].BananaShowerXOffset.Add(xOffSetCheck);
                        rng.Next();
                        rng.Next();
                        rng.Next();
                    }
                    if (!restart)
                        indx++;
                } else if (AllHitObjects[indx].OType.Equals(GetObjectInfo.Type.Slider))
                {
                    // TODO: For sliders
                    //foreach(var drop in obj.SliderList)
                    //{
                    //else if (drop.TinyDroplet)
                    //rng.Next(20, 20);
                    //else if (drop.Droplet)
                    //rng.Next();
                    //}
                    rng.Next();
                    indx++;
                } else
                    indx++;

                if (flag)
                    return false;

                //temp = rng;
            }

            // Put all contents as well as processed hitobjects into osu file
            ToFileMaker toFile = new();
            return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, bmHitObjects);

            // TODO: For debugging
            //if (debugging)
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