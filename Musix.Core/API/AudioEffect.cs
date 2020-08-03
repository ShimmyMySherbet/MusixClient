using Musix.Core.Models;
using NAudio.Wave;
using System;
using System.IO;

namespace Musix.Core.API
{
    public abstract class AudioEffect
    {
        public string AudioCachePath = null;

        public abstract AudioEffectResult ApplyEffect(ref AudioFileReader Reader);

        protected string GetTempFile(string ext = "")
        {
            if (AudioCachePath == null) return Path.GetTempFileName();
            if (ext.Length != 0)
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


        protected string GetCacheFile(string filename)
        {
            return Path.Combine(AudioCachePath, filename);
        }
    }
}