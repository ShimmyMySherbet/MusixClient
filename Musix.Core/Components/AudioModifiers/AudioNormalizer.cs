using Musix.Core.API;
using Musix.Core.Components.Providers;
using NAudio.Wave;
using System;
using System.IO;
using System.Text;

namespace Musix.Core.Components.AudioModifiers
{
    public class AudioNormalizer : AudioEffect
    {
        public override void ApplyEffect(ref AudioFileReader Reader)
        {
            float max = 0;
            float[] buffer = new float[Reader.WaveFormat.SampleRate];
            int read;
            do
            {
                read = Reader.Read(buffer, 0, buffer.Length);
                for (int n = 0; n < read; n++)
                {
                    var abs = Math.Abs(buffer[n]);
                    if (abs > max) max = abs;
                }
            } while (read > 0);
            if (!(max == 0) && !(max > 1.0f))
            {
                Console.WriteLine("Normalized");
                Reader.Position = 0;
                Reader.Volume = 1.0f / max;
            } else
            {
                Console.WriteLine("Cannot Normalize");
            }
        }
    }
}