using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    public class SliderProcess
    {
        public void ProcessingSliders()
        {
            // TODO: Do this
        }

        // TODO: Use BPM and Velocity to determine how far the slider will go
        public double GoingThroughTimingPoints(double sliderTime, int timingpointsStart, bool inherited, IEnumerable<String> Path)
        {
            if (inherited)
            {
                double lowest = 0, highest = 0;
                for (int i = timingpointsStart; i < Path.Count(); i++)
                {
                    if (!Path.Skip(i).First().Equals(""))
                    {
                        int current = Int32.Parse(Path.Skip(i).First().Split(",")[0]);
                        // Get map info
                        if (Path.Skip(i).First().Split(',')[6].Equals("1"))
                            if (current < sliderTime)
                                lowest = current;
                            else if (current > sliderTime)
                            {
                                highest = current;
                                if (lowest.Equals(0))
                                    return -1;
                                else if (lowest < highest && sliderTime > lowest && sliderTime < highest && Convert.ToDouble(Path.Skip(i).First().Split(",")[1]) > 0)
                                    return Convert.ToDouble(Path.Skip(i).First().Split(",")[1]);
                            }
                    }
                    else
                        return -1;
                }
            }
            else
            {
                double lowest = 0, highest = 0;
                for (int i = timingpointsStart; i < Path.Count(); i++)
                {
                    if (!Path.Skip(i).First().Equals(""))
                    {
                        int current = Int32.Parse(Path.Skip(i).First().Split(",")[0]);
                        // Get map info
                        if (Path.Skip(i).First().Split(',')[6].Equals(0))
                            if (current < sliderTime)
                                lowest = current;
                            else if (current > sliderTime)
                            {
                                highest = current;
                                if (lowest.Equals(0))
                                    return -1;
                                else if (lowest < highest && sliderTime > lowest && sliderTime < highest && Convert.ToDouble(Path.Skip(i).First().Split(",")[1]) > 0)
                                    return Convert.ToDouble(Path.Skip(i).First().Split(",")[1]);
                            }
                    }
                    else
                        return -1;
                }
            }
            return -1;
        }
    }
}
