using Musix.Core.API;
using Musix.Core.Models;
using NAudio.Wave;
using System;
using System.IO;

namespace Musix.Core.Components.AudioModifiers
{
    public class AudioTrimmer : AudioEffect
    {
        public TimeSpan? StartTime;
        public TimeSpan? EndTime;

        public AudioTrimmer(TimeSpan? StartTime = null, TimeSpan? EndTime = null)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        public override AudioEffectResult ApplyEffect(ref AudioFileReader Reader)
        {
            if (!StartTime.HasValue) StartTime = new TimeSpan(0);
            if (!EndTime.HasValue) EndTime = Reader.TotalTime;
            string FileN = GetCacheFile($"mid_audio_trim_{DateTime.Now.Ticks}.mp3");
            string CurFile = Reader.FileName;
            Console.WriteLine("Reader File: " + CurFile);
            Console.WriteLine("Closing current reader...");
            Reader.Dispose();
            if (TrimAudio(CurFile, FileN, StartTime, EndTime))
            {
                Console.WriteLine("Trimmed");
                Reader = new AudioFileReader(FileN);
                return AudioEffectResult.Completed | AudioEffectResult.PreEncoded;
            }
            else
            {
                Console.WriteLine("Failed, re-creating reader...");
                Reader = new AudioFileReader(FileN);
                Console.WriteLine("Cannot trim");
                return AudioEffectResult.Failed;
            }
        }

        public bool TrimAudio(string Input, string Output, TimeSpan? StartT, TimeSpan? EndT)
        {
            if (File.Exists(Output)) File.Delete(Output);
            using (Mp3FileReader Reader = new Mp3FileReader(Input))
            using (FileStream Writer = new FileStream(Output, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Mp3Frame NextFrame = Reader.ReadNextFrame();
                while (NextFrame != null)
                {
                    bool WriteFrame = true;
                    bool EndFrames = false;
                    if (StartT.HasValue)
                    {
                        if (Reader.CurrentTime < StartT.Value) WriteFrame = false;
                    }
                    if (EndT.HasValue)
                    {
                        if (Reader.CurrentTime > EndT.Value)
                        {
                            WriteFrame = false;
                            EndFrames = true;
                        }
                    }
                    if (WriteFrame)
                    {
                        Writer.Write(NextFrame.RawData, 0, NextFrame.FrameLength);
                    }
                    if (EndFrames)
                    {
                        NextFrame = null;
                        continue;
                    }
                    NextFrame = Reader.ReadNextFrame();
                }
                return Writer.Length != 0;
            }
        }
    }
}