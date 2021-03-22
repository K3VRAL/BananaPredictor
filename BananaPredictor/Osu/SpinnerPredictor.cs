using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;
using osu.Game.Rulesets.Catch.Beatmaps;

namespace BananaPredictor.Osu
{
    // TODO: Need to get inherit and uninherit lines to see how they affect the slider
    // TODO: Need to process how sliders will be placed
    // Used for debugging if program/initial logic works
    public class BananaSpinPredictor
    {
        // File of .osu
        IEnumerable<String> lines;

        // Start line from file of hitobjects
        int bmHitObjects;
        int bmTimingPoints;

        // All hitobjects
        List<GetObjectInfo> AllHitObjects = new();
        List<GetMapInfo> AllTimingPoints = new();

        // Inputted Slider Info
        // Key              -
        // MapRelated       -       [0] = startTime, [1] = endTime, [2] = distanceSpinner, [3] = invertSpinner, [4] = onlySpinner
        // SpinnerRelated   -       [0] = startPos, [1] = endPos    |       (will include [0] = startLeftPos, [1] = startRightPos, [2] = endLeftPos, [3] = endRightPos maybe using enum instead)
        public StoringInfoDict spinnerSpecs;

        private bool flag = false;

        public bool SpinnerPredictor(string path)
        {
            // Read file
            lines = File.ReadLines(path);

            // Map info lines (Used for the file name and such)
            GetMusicInfo MusicInfo = new();
            MusicInfo.Path = lines;
            bmHitObjects = MusicInfo.GetItemLine("[HitObjects]");
            bmHitObjects++;
            bmTimingPoints = MusicInfo.GetItemLine("[TimingPoints]");
            bmTimingPoints++;

            // If not found any
            if (bmHitObjects < 0)
                return false;

            // Storing all spinners and objects found into list
            CheckingObjects();
            if (flag)
                return false;

            // TODO: Attempt to process slider
            //private void ProcessingSlider() { for (int i = 0; i < AllHitObjects.Count; i++) { if (AllHitObjects[i].OType.Equals(GetObjectInfo.Type.Slider)) {}}}

            // Making requested spinners
            AddingSpinners();
            if (flag)
                return false;

            for (int i = 0; i < spinnerSpecs.Count; i++)
            {
                if (Convert.ToBoolean(spinnerSpecs[i].MapRelated[4]))
                {
                    // Adding Timing Points
                    AddingTiming(i);
                    if (flag)
                        return false;

                    // Splitting up spinners in a length of 1
                    SplittingSpinners(i);
                    if (flag)
                        return false;

                    // Processing each spinner - The logic for generating the time interval for each banana according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
                    // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Objects/BananaShower.cs
                    ProcessingSpinners();
                    if (flag)
                        return false;

                    // How each banana is processed - The logic the banana xoffset according to the catch rulesets; all rights go to peppy and his mathematics, just using the important code
                    // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Beatmaps/CatchBeatmapProcessor.cs
                    //if (spinnerSpecs.Count <= 6) XOffsetProcessingDynamic(); else
                    XOffsetProcessing(i);
                    if (flag)
                        return false;
                }
            }

            // Put all contents as well as processed hitobjects into osu file
            ToFileMaker toFile = new();
            return toFile.OsuToFile(lines, path, MusicInfo, AllHitObjects, AllTimingPoints, bmHitObjects, bmTimingPoints);
        }

        private void CheckingObjects()
        {
            for (int i = bmTimingPoints; i < bmHitObjects; i++)
            {
                // Making sure that these are timing points; timing points have 8 different propertise so we make sure that they have it according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#timing-points
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length == 8)
                    if (Int32.Parse(amount[6]) == 1)
                        // Added Timing Point
                        AllTimingPoints.Add(new GetMapInfo
                        {
                            Timing = lines.Skip(i).First(),
                            TType = GetMapInfo.Type.Timing,
                            TimeStart = Int32.Parse(amount[0]),
                            BeatLength = Double.Parse(amount[1])
                        });
                    else if (Int32.Parse(amount[6]) == 0)
                        // Added Inherit Point
                        AllTimingPoints.Add(new GetMapInfo
                        {
                            Timing = lines.Skip(i).First(),
                            TType = GetMapInfo.Type.Inherit,
                            TimeStart = Int32.Parse(amount[0]),
                            BeatLength = Double.Parse(amount[1])
                        });
            }
            
            for (int i = bmHitObjects; i < lines.Count(); i++)
            {
                // Making sure that these are spinners; spinners always have x: 256 and y: 192 according to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#spinners
                String[] amount = lines.Skip(i).First().Split(",");
                if (amount.Length == 7 && Int32.Parse(amount[0]) == 256 && Int32.Parse(amount[1]) == 192)
                    // Spinner added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Spinner,
                        BananaStart = Int32.Parse(amount[2]),
                        BananaEnd = Int32.Parse(amount[5]),
                        BananaShowerTime = new()
                    });
                else if (amount.Length > 7)
                    // Slider added
                    // Checks for sliders; they use the rng class as well like spinners
                    // According to https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osu_%28file_format%29#sliders; they are pretty dynamic whereas hitobjects and spinners are static, so I hope this line of code doesn't break anything with other beatmaps
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Slider,
                        NestedSlider = new()
                    });
                else
                    // Circle added
                    AllHitObjects.Add(new GetObjectInfo
                    {
                        Object = lines.Skip(i).First(),
                        OType = GetObjectInfo.Type.Normal
                    });

                if (flag)
                    return;
            }
        }

        private void AddingTiming(int i)
        {
            AllTimingPoints.Add(new GetMapInfo
            {
                Timing = "",
                TType = GetMapInfo.Type.Timing,
                TimeStart = spinnerSpecs[i].MapRelated[0],
                // TODO: Change this to match with the map's original BPM
                BeatLength = 58.59375
            });
            AllTimingPoints.Add(new GetMapInfo
            {
                Timing = spinnerSpecs[i].MapRelated[0] + ",-100,4,2,1,20,0,0",
                TType = GetMapInfo.Type.Inherit,
                TimeStart = spinnerSpecs[i].MapRelated[0],
                BeatLength = -100
            });
            Console.WriteLine("Making spinner {0} - {1}", AllHitObjects[i].BananaStart, AllHitObjects[i].BananaEnd);

            if (flag)
                return;
        }

        private void AddingSpinners()
        {
            for (int i = 0; i < spinnerSpecs.Count; i++)
            {
                AllHitObjects.Add(new GetObjectInfo
                {
                    Object = "256,192," + spinnerSpecs[i].MapRelated[0] + ",12,0," + spinnerSpecs[i].MapRelated[1] + ",0:0:0:0:",
                    OType = GetObjectInfo.Type.Spinner,
                    BananaStart = spinnerSpecs[i].MapRelated[0],
                    BananaEnd = spinnerSpecs[i].MapRelated[1],
                    BananaShowerTime = new()
                });
                Console.WriteLine("Making spinner {0} - {1}", AllHitObjects[i].BananaStart, AllHitObjects[i].BananaEnd);

                if (flag)
                    return;
            }
        }

        private void SplittingSpinners(int j)
        {
            int num = AllHitObjects.Count;
            for (int i = num - 1; i >= 0; i--)
            {
                if (AllHitObjects[i].OType.Equals(GetObjectInfo.Type.Spinner)
                    && AllHitObjects[i].BananaStart == spinnerSpecs[j].MapRelated[0]
                    && AllHitObjects[i].BananaEnd == spinnerSpecs[j].MapRelated[1])
                {
                    for (int k = AllHitObjects[i].BananaStart; k < AllHitObjects[i].BananaEnd - 2; k += spinnerSpecs[j].MapRelated[2])
                        AllHitObjects.Add(new GetObjectInfo
                        {
                            Object = "256,192," + k + ",12,0," + (k + 1) + ",0:0:0:0:",
                            OType = GetObjectInfo.Type.Spinner,
                            BananaStart = k,
                            BananaEnd = k + 1,
                            BananaShowerTime = new()
                        });

                    AllHitObjects.RemoveAt(i);

                    if (flag)
                        return;
                }

                if (flag)
                    return;
            }
        }

        private void ProcessingSpinners()
        {
            foreach (var obj in AllHitObjects)
            {
                if (obj.OType.Equals(GetObjectInfo.Type.Spinner))
                {
                    Console.WriteLine("Processing Spinner {0}", obj.Object);
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
                        Console.WriteLine("Added BananaShowerTime {0}", obj.BananaShowerTime.Count);
                        obj.BananaShowerTime.Add(time);
                        time += spacing;

                        if (flag)
                            return;
                    }
                }

                if (flag)
                    return;
            }
        }

        private void XOffsetProcessing(int i)
        {
            // Mergesort
            MergeSort ms = new();
            
            // Seeded RNG
            FastRandom rng = new(CatchBeatmapProcessor.RNG_SEED); // Why is the seed 1337?
            //FastRandom temp = new(CatchBeatmapProcessor.RNG_SEED);
            
            int indx = 0;
            bool restart;
            while (indx < AllHitObjects.Count)
            {
                restart = false;
                Console.WriteLine("Identifying {0} in indx {1}", AllHitObjects[indx].Object, indx);
                if (AllHitObjects[indx].OType.Equals(GetObjectInfo.Type.Spinner))
                {
                    for (int j = 0; j < AllHitObjects[indx].BananaShowerTime.Count; j++)
                    {
                        double xOffSetCheck = (float)(rng.NextDouble() * CatchPlayfield.WIDTH);
                        Console.WriteLine("xOffset {0} | Adding new slider {1}", xOffSetCheck, !(xOffSetCheck < spinnerSpecs[i].SpinnerRelated[0] || xOffSetCheck > spinnerSpecs[i].SpinnerRelated[1]));
                        if (AllHitObjects[indx].BananaStart > spinnerSpecs[i].MapRelated[0]
                        && AllHitObjects[indx].BananaEnd < spinnerSpecs[i].MapRelated[1]
                        && Invert(spinnerSpecs[i].MapRelated[3], xOffSetCheck, i))
                        {
                            AllHitObjects.Insert(indx, new GetObjectInfo
                            {
                                Object = "256,144," + AllHitObjects[indx].BananaStart + ",6,0,L|256:166,1,20",
                                OType = GetObjectInfo.Type.Slider,
                                NestedSlider = new()
                            });
                            //rng = temp;
                            // TODO: IT WORKS. IT ACTUALLY WORKS. BUT ITS SO FUCKING INEFFICIENT. USE TEMPS INSTEAD OF RESETTING; HUGE MEMORY LEAKS
                            // NOTE: If I try using temp, the whole thing breaks and I get different values.
                            rng = new FastRandom(CatchBeatmapProcessor.RNG_SEED);
                            indx = 0;
                            AllHitObjects = ms.Merge(AllHitObjects);
                            restart = true;
                            break;
                        }
                        rng.Next();
                        rng.Next();
                        rng.Next();
                    }

                    if (!restart)
                        indx++;
                }
                else if (AllHitObjects[indx].OType.Equals(GetObjectInfo.Type.Slider))
                {
                    // I am so stupid, it's insane. Went through the source code for https://github.com/ppy/osu/blob/70342897637e9dd630d778daab062e7a7022d240/osu.Game.Rulesets.Catch/MathUtils/FastRandom.cs#L47
                    // but what I failed to realize is that TinyDroplet and Droplet will always just normally use the same function to change the rng. I thought this wasn't the case and now I feel stupid
                    // TODO: Use slider nested only to see if the rng should change if slider is long enough to generate a drop/droplet as well as repeated sliders
                    //foreach(var drop in AllHitObjects[indx].nestedSlider)
                    //{
                    //if (drop.TinyDroplet)
                    //rng.Next(20, 20);
                    //else if (drop.Droplet)
                    //rng.Next();
                    //else
                    //return false;
                    //}
                    rng.Next();
                    indx++;
                }
                else
                    indx++;

                if (flag)
                    return;

                //temp = rng;
            }
        }

        private void XOffsetProcessingDynamic()
        {
            // TODO: Work on this
        }

        private bool Invert(int isRequested, double offset, int index)
        {
            if (Convert.ToBoolean(isRequested))
                return (offset < spinnerSpecs[index].SpinnerRelated[0] || offset > spinnerSpecs[index].SpinnerRelated[1]);
            else
                return !(offset < spinnerSpecs[index].SpinnerRelated[0] || offset > spinnerSpecs[index].SpinnerRelated[1]);
        }

        public void Stop()
        {
            Console.WriteLine("Stop flag triggered");
            flag = true;
        }

        public bool GetFlag()
        {
            flag = !flag;
            return !flag;
        }
    }
}