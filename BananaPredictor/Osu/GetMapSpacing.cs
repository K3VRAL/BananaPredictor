using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaPredictor.Osu
{
    // TODO: Do this for slider spacing to prevent lag
    // Need to get inheritage and uninheritage to influence how to process the slider
    public class GetMapSpacing
    {
        public int Spacing(String spinnerStart, IEnumerable<String> Path)
        {
            GetMusicInfo gmi = new();
            int store = gmi.GetItemLine("[TimingPoints]", Path);

            //int minimum = 0;
            //int maximum = 0;

            for (int i = store + 1; i < Path.Count(); i++)
            {
                //if ()
                //return i;
            }

            return -1;
        }
    }
}
