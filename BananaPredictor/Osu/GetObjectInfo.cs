using System;
using System.Collections.Generic;

namespace BananaPredictor.Osu
{
    public class GetObjectInfo
    {
        public enum Type
        {
            Normal,
            Slider,
            Spinner
        }

        public String Object { get; set; }
        public Type OType { get; set; }
        // TODO: Slider nested
        public List<double> NestedSlider { get; set; }

        // Spinner start to spinner end
        public int BananaStart { get; set; }
        public int BananaEnd { get; set; }

        // Getting the specific time of the spinner
        public List<double> BananaShowerTime { get; set; }
    }
}
