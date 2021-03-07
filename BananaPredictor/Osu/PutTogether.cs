using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaPredictor.Osu
{
    public class PutTogether
    {
        public String PutLineTogether(int bmNumber, IEnumerable<String> lines) =>
            String.Join("", lines.Skip(bmNumber).Take(1).First().Split(':').Skip(1));
    }
}
