using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaPredictor.Osu
{
    public class GetMusicInfo        // TODO; to clean up and make the code more readable/organized
    {
        private String path;
        private int bmHitObjects, bmTitle, bmArtist, bmCreator, bmVersion;

        public GetMusicInfo(String path)
        {
            this.path = path;
            this.setInfo();
            bmHitObjects = 0; bmTitle = 0; bmArtist = 0; bmCreator = 0; bmVersion = 0;
        }

        private void setInfo()
        {
            
        }

        private String GettingFromLine(int num, String name)
        {
            foreach (var line in File.ReadLines(path))
            {
                if (line.Contains(name))
                {
                    return line;
                }
            }
            return null;
        }

            public String GetBmHitObject()
            {
                foreach (var line in File.ReadLines(path))
                {
                    // Get map info
                    if (line.Contains("Title") && bmTitle.Equals(0))
                        bmTitle = bmHitObjects;
                    if (line.Contains("Artist") && bmArtist.Equals(0))
                        bmArtist = bmHitObjects;
                    if (line.Contains("Creator") && bmCreator.Equals(0))
                        bmCreator = bmHitObjects;
                    if (line.Contains("Version") && bmVersion.Equals(0))
                        bmVersion = bmHitObjects;

                    // Get start line for hitobjects
                    if (line.Equals("[HitObjects]"))
                    {
                        System.Diagnostics.Debug.WriteLine("Found [HitObjects]");
                        return line;
                    }
                    bmHitObjects++;
                }
                return null;
        }
    }
}
