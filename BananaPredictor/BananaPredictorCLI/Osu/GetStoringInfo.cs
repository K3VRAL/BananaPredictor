using System.Collections.Generic;

namespace BananaPredictorCLI.Osu
{
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
        
        public void Remov(int key)
        {
            Remove(key);
        }
    }
}
