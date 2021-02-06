using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    public class GetObjectInfo
    {
        public String Object { get; set; }
        public bool Slider { get; set; }
        public List<double> SliderList { get; set; }
        public bool Banana { get; set; }
        public List<double> BananaShowerTime { get; set; }
        public List<double> BananaShowerXOffset { get; set; }
    }
}
