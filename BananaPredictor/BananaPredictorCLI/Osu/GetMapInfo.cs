using System;

namespace BananaPredictorCLI.Osu
{
    public class GetMapInfo
    {
        // TODO: Use this to work out the slider's propertise
        public enum Type
        {
            Timing,
            Inherit
        }

        public String Timing { get; set; }
        public Type TType { get; set; }
        public int TimeStart { get; set; }
        public double BeatLength { get; set; }

        // TODO: Find a way to use the map's slider velocity/tick rate as well
        public double MapSliderVelocity { get; set; }
        public double MapSliderTickRate { get; set; }
    }
}
