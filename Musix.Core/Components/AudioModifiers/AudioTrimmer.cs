using Musix.Core.API;
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

        public override void ApplyEffect(ref AudioFileReader Reader)
        {
            Console.WriteLine($"!>{Reader.FileName}");
            if (!StartTime.HasValue) StartTime = new TimeSpan(0);
            if (!EndTime.HasValue) EndTime = Reader.TotalTime;
            string FileN = GetTempFile("mp3");
            Reader.Dispose();
            try
            {
                if (TrimMp3(Reader.FileName, FileN, StartTime, EndTime))
                {
                    Console.WriteLine("Trimmed");
                    Console.WriteLine("Switch");
                    Reader = new AudioFileReader(FileN);
                }
                else
                {
                    Console.WriteLine("Cannot trim");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private bool TrimMp3(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                return false;
            var reader = new Mp3FileReader(inputPath);
            var writer = File.Create(outputPath);
            Mp3Frame frame;
            while ((frame = reader.ReadNextFrame()) != null)
            {
                if (reader.CurrentTime >= begin || !begin.HasValue)
                {
                    if (reader.CurrentTime <= end || !end.HasValue)
                    {
                        writer.Write(frame.RawData, 0, frame.RawData.Length);
                    }
                    else
                    {
                        Console.WriteLine("break");
                        break;
                    }
                }
            }
            reader.Close();
            Console.WriteLine($"W len: {writer.Length}");
            writer.Close();
            return true;
        }

        private bool BaseTrimMp3(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                return false;

            using (var reader = new Mp3FileReader(inputPath))
            using (var writer = File.Create(outputPath))
            {
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                    if (reader.CurrentTime >= begin || !begin.HasValue)
                    {
                        if (reader.CurrentTime <= end || !end.HasValue)
                            writer.Write(frame.RawData, 0, frame.RawData.Length);
                        else break;
                    }
            }
            return true;
        }
    }
}