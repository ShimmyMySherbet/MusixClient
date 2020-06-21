using Musix.Core.API;
using Musix.Core.Components.AudioModifiers;
using NAudio.Lame;
using NAudio.Wave;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

        public bool HasEffectOfType<T>() where T : AudioEffect
        {
            foreach (AudioEffect Effect in Effects)
            {
                if (Effect.GetType() == typeof(T)) return true;
            }
            return false;
        }

        public T GetEffectOfType<T>() where T : AudioEffect
        {
            foreach (AudioEffect Effect in Effects)
            {
                if (Effect.GetType() == typeof(T)) return (T)Effect;
            }
            return null;
        }

        public void RemoveEffectsOfType<T>()
        {
            var copy = Effects.ToArray();
            foreach (var ent in copy)
            {
                if (ent.GetType() == typeof(T)) Effects.Remove(ent);
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
            if (Effects.Count != 0)
            {
                bool RequiresRencoding = false;
                int Succeeded = 0;
                AudioFileReader Reader = new AudioFileReader(InputFile);
                foreach (AudioEffect Effect in Effects)
                {
                    if (string.IsNullOrEmpty(Effect.AudioCachePath)) Effect.AudioCachePath = AudioCachePath;

                    Reader.Position = 0;
                    System.Console.WriteLine($"Applying effect of type {Effect.GetType().Name}");
                    AudioEffectResult Result = Effect.ApplyEffect(ref Reader);
                    if (Result == AudioEffectResult.Completed)
                    {
                        System.Console.WriteLine("Returned S");
                        RequiresRencoding = true;
                        Succeeded += 1;
                    }  else if (Result == AudioEffectResult.Failed)
                    {
                        System.Console.WriteLine("Effect Failed.");
                    } else if (Result == (AudioEffectResult.Completed | AudioEffectResult.PreEncoded))
                    {
                        System.Console.WriteLine("Effect Completed + Encoded.");
                        RequiresRencoding = false;
                        Succeeded += 1;
                    }
                }
                bool ReEncode = true;
                
                if (Succeeded == 0)
                {
                    System.Console.WriteLine("Re encoding; No effects passed");
                    ReEncode = false;
                } else if (RequiresRencoding)
                {
                    System.Console.WriteLine("Re encoding; Re-encoding declared as required");
                    ReEncode = true;
                } else
                {
                    System.Console.WriteLine("Copying Stream; Stream declared as pre-encoded");

                    ReEncode = false;
                }
                if (ReEncode)
                {
                    System.Console.WriteLine("Re-Encoding...");
                    using (var writer = new LameMP3FileWriter(OutputFile, Reader.WaveFormat, 128))
                    {
                        Reader.CopyTo(writer);
                    }
                    Reader.Dispose();
                } else
                {
                    System.Console.WriteLine("Copy Out");
                    File.Copy(Reader.FileName, OutputFile);
                }
            }
            else
            {
                File.Copy(InputFile, OutputFile);
            }
        }
    }
}