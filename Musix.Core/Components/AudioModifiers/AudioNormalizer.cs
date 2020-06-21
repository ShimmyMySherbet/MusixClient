using Musix.Core.API;
using Musix.Core.Components.Providers;
using Musix.Core.Models;
using NAudio.Wave;
using System;
using System.IO;
using System.Text;

namespace Musix.Core.Components.AudioModifiers
{
    public class AudioNormalizer : AudioEffect
    {
        /// <summary>
        /// Set to 1f for full normalization.
        /// Having a small buffer can help reduce playback distortion at full volume.
        /// </summary>
        public float NormalizerLevel = 0.95f;
        public override AudioEffectResult ApplyEffect(ref AudioFileReader Reader)
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
            if (!(max == 0) && !(max > NormalizerLevel))
            {
                Console.WriteLine("Normalized");
                Reader.Position = 0;
                Reader.Volume = NormalizerLevel / max;
                return AudioEffectResult.Completed;
            } else
            {
                return AudioEffectResult.Failed;
            }
        }
    }
}