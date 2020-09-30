using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.API;
using Musix.Core.Models.Debug;
using NAudio.MediaFoundation;
using NAudio.Wave;

namespace Musix.FFMPEG
{
    public class MediaFoundationProvider : IConversionProvider
    {
        private static bool Initialized = false;
        public int Bitrate = 192000;

        public bool CanConvert(string InputFile, string OutputFile)
        {
            FileInfo inf = new FileInfo(OutputFile);
            string l = inf.Extension.ToLower();
            return l.EndsWith("mp3") || l.EndsWith("wma") || l.EndsWith("aac");
        }

        public Task Convert(string Inputfile, string OutputFile)
        {
            StopWatch w = new StopWatch();
            if (!Initialized)
            {
                Initialized = true;
                MediaFoundationApi.Startup();
            }
            return new Task(delegate ()
            {
                FileInfo inf = new FileInfo(OutputFile);
                AudioFileReader FileR = new AudioFileReader(Inputfile);
                string l = inf.Extension.ToLower();
                if (l.EndsWith("mp3"))
                {
                    MediaFoundationEncoder.EncodeToMp3(FileR, OutputFile, Bitrate);
                }
                else if (l.EndsWith("wma"))
                {
                    MediaFoundationEncoder.EncodeToWma(FileR, OutputFile, Bitrate);
                }
                else if (l.EndsWith("aac"))
                {
                    MediaFoundationEncoder.EncodeToAac(FileR, OutputFile, Bitrate);
                } else
                {
                    throw new NotSupportedException("Format not supported");
                }
                w.PrintDur("MediaProvider");
            });
        }
    }
}
