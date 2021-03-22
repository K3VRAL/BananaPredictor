using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    // TODO: Use this for a better storage system

    // Thanks stackoverflow https://stackoverflow.com/questions/569903/multi-value-dictionary
    public struct StoringInfo
    {
        public List<int> MapRelated;
        public List<int> SpinnerRelated;
    }

    public class StoringInfoDict : Dictionary<int, StoringInfo>
    {
        public void Add(int key, List<int> map, List<int> spin)
        {
            StoringInfo si;
            si.MapRelated = map;
            si.SpinnerRelated = spin;
            Add(key, si);
        }
    }
}
