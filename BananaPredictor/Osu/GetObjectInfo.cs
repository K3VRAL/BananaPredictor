using System;
using System.Collections.Generic;

namespace BananaPredictor.Osu
{
    public class GetObjectInfo
    {
        public String Object { get; set; }
        public bool Slider { get; set; }
        public double SliderInheritedPoint { get; set; }
        public double SliderUninheritedPoint { get; set; }
        public bool Banana { get; set; }
        public List<double> BananaShowerTime { get; set; }
        public List<double> BananaShowerXOffset { get; set; }
    }
}
