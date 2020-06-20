using NAudio.Wave;
using System;
using System.IO;

namespace Musix.Core.API
{
    public abstract class AudioEffect
    {
        public string AudioCachePath = null;

        public abstract void ApplyEffect(ref AudioFileReader Reader);

        protected string GetTempFile(string ext = "")
        {
            if (AudioCachePath == null) return Path.GetTempFileName();
            if (ext != "")
            {
                string res = Path.Combine(AudioCachePath, $"TMP_Effect_{DateTime.Now.Ticks}.{ext}");
                Console.WriteLine($">{res}");
                return res;
            }
            else
            {
                string res = Path.Combine(AudioCachePath, $"TMP_Effect_{DateTime.Now.Ticks}.tmp");
                Console.WriteLine($">{res}");
                return res;
            }
        }
    }
}