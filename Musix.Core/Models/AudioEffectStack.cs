using Musix.Core.API;
using Musix.Core.Components.AudioModifiers;
using NAudio.Lame;
using NAudio.Wave;
using System.Collections;
using System.Collections.Generic;

namespace Musix.Core.Models
{
    public class AudioEffectStack : IEnumerable<AudioEffect>
    {
        private List<AudioEffect> Effects = new List<AudioEffect>();
        public string AudioCachePath;

        public void AddEffect(AudioEffect Effect, int position = -1)
        {
            if (position == -1)
            {
                if (Effect.GetType() == typeof(AudioTrimmer))
                {
                    Effects.Insert(0, Effect);
                }
                else
                {
                    Effects.Add(Effect);
                }
            }
            else
            {
                Effects.Insert(position, Effect);
            }
        }

        public int EffectCount
        {
            get
            {
                return Effects.Count;
            }
        }

        public void RemoveEffect(AudioEffect Effect)
        {
            Effects.Remove(Effect);
        }

        public IEnumerator<AudioEffect> GetEnumerator()
        {
            return Effects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Effects.GetEnumerator();
        }

        public void ApplyEffects(string InputFile, string OutputFile)
        {
            AudioFileReader Reader = new AudioFileReader(InputFile);
            foreach (AudioEffect Effect in Effects)
            {
                if (string.IsNullOrEmpty(Effect.AudioCachePath)) Effect.AudioCachePath = AudioCachePath;

                Reader.Position = 0;
                System.Console.WriteLine($"Applying effect of type {Effect.GetType().Name}");
                Effect.ApplyEffect(ref Reader);
            }
            using (var writer = new LameMP3FileWriter(OutputFile, Reader.WaveFormat, 128))
            {
                Reader.CopyTo(writer);
            }
            Reader.Dispose();
        }
    }
}