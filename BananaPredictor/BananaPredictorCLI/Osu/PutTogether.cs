using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaPredictorCLI.Osu
{
    public class PutTogether
    {
        public IEnumerable<String> Path { get; set; }
        public String PutLineTogether(int bmNumber) =>
            String.Join("", Path.Skip(bmNumber).Take(1).First().Split(':').Skip(1));
    }
}
