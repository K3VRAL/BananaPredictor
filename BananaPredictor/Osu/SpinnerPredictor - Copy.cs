﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using osu.Game.Rulesets.Catch.MathUtils;
using osu.Game.Rulesets.Catch.UI;

namespace BananaPredictor.Osu
{
    public class BananaPredictorCopy
    {
        // Apologies for the terrible code/not using other libraries such as OsuPrasers to get the job done
        // TODO: Clean up code and make more functions instead of hard coding; Fix the major bug that is preventing this code from working
        private IEnumerable<String> lines;
        public bool SpinnerPredictorCopy(string path)
        {
            // Read file
            lines = File.ReadLines(path);

            // Map info lines
            int bmHitObjects = 0, bmTitle = 0, bmArtist = 0, bmCreator = 0, bmVersion = 0;
            foreach (var line in lines)
            {
                // Get map info
                if (line.Contains("Title") && bmTitle.Equals(0))
                    bmTitle = bmHitObjects;
                if (line.Contains("Artist") && bmArtist.Equals(0))
                    bmArtist = bmHitObjects;
                if (line.Contains("Creator") && bmCreator.Equals(0))
                    bmCreator = bmHitObjects;
                if (line.Contains("Version") && bmVersion.Equals(0))
                    bmVersion = bmHitObjects;

                // Get start line for hitobjects
                if (line.Equals("[HitObjects]"))
                {
                    System.Diagnostics.Debug.WriteLine("Found [HitObjects]");
                    break;
                }
                bmHitObjects++;
            }

            // If not found any
            if (lines.Count().Equals(bmHitObjects))
            {
                System.Diagnostics.Debug.WriteLine("Couldn't Find [HitObjects] | Count: {0} File: {1}", bmHitObjects, lines.Count());
                return false;
            }

            // Storing all spinners and objects found into dictionary/map
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
                        Index = i
                    });
                    continue;
                }
                HitObjects.Add(new GetObjectInfo { 
                    Object = lines.Skip(i).First(),
                    Banana = false,
                    Index = i
                });
            }
            System.Diagnostics.Debug.WriteLine("Amount of added spinners: {0}", HitObjects.Count);

            // Processing each spinner - The logic according to the catch rulesets; all rights go to peppy and his mathematics, just trying to specifically get the important code
            // Used according to https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/Objects/BananaShower.cs
            for (int i = 0; i < HitObjects.Count; i++)
            {
                switch (HitObjects[i].Banana)
                {
                    case true:
                        String[] getitem = HitObjects[i].Object.Split(",");
                        double time = Int32.Parse(getitem[2]);
                        double endtime = Int32.Parse(getitem[5]);
                        int duration = Int32.Parse(getitem[5]) - Int32.Parse(getitem[2]);

                        double spacing = duration;
                        while (spacing > 100)
                            spacing /= 2;
                        if (spacing <= 0)
                            continue;

                        //System.Diagnostics.Debug.WriteLine("time: {0} endtime: {1} duration: {2} spacing: {3}", time, endtime, duration, spacing);

                        while (time <= endtime)
                        {
                            //System.Diagnostics.Debug.WriteLine("Time = {0}, index = {1}", time, i);
                            HitObjects[i].BananaShowerTime.Add(time);

                            time += spacing;
                        }
                        break;
                }
            }

            // TODO: How each banana is processed
            var rng = new FastRandom((int)1337);     // Must be an error here
            foreach (var obj in HitObjects)
            {
                if (obj.Banana)
                    obj.BananaShowerXOffset.Add((float)(rng.NextDouble() * CatchPlayfield.WIDTH));     // Must be an error here
            }

            //System.Diagnostics.Debug.WriteLine("Amount indepth = {0}", inDepthSpinner.Count);
            //inDepthSpinner.Select(x => "xOffset = " + x.Value + " Time = " + x.Key).ToList().ForEach(x => System.Diagnostics.Debug.WriteLine(x));

            // To file - This is an analyzer so instead we make a seperate textfile in which we place beats to test if we get the same generated x offset of beats
            // Note: This is a file only for the beats, not the entire osu file which is why it is a txt file
            // TODO: Make so that it uses all the info as well to make a .osu file
            String filename = String.Join("\\", path.Split('\\').Reverse().Skip(1).Reverse().ToArray()) + "\\" + this.PutTogether(bmArtist) + " - " + this.PutTogether(bmTitle) + " (" + this.PutTogether(bmCreator) + ") [" + this.PutTogether(bmVersion) + " BananaPredictor].osu";
            File.Create(filename).Close();      // Create and override the file
            int num = 0;
            using (StreamWriter file = new StreamWriter(filename))
            {
                foreach (var line in lines)
                {
                    if (num == bmVersion)
                    {
                        file.WriteLine(line + " BananaPredictor");
                        num++;
                        continue;
                    }
                    if (num.Equals(bmHitObjects + 1))
                    {
                        break;
                    }
                    file.WriteLine(line);
                    num++;
                }
                
                foreach (var line in HitObjects)
                {
                    file.WriteLine(line.BananaShowerXOffset + ",192," + line.BananaShowerTime + ",1,0,0:0:0:0:");
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
