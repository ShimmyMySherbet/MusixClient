//using Musix.Core.API;
//using NAudio.MediaFoundation;
//using NAudio.Wave;
//using System.IO;
//using System.Threading.Tasks;



// Moved to Musix.Providers.Windows





//namespace Musix.Core.Components.Providers
//{
//    public class MediaFoundationProvider : IConversionProvider
//    {
//        private static bool Initialized = false;
//        public int Bitrate = 192000;

//        public bool CanConvert(string InputFile, string OutputFile)
//        {
//            FileInfo inf = new FileInfo(OutputFile);
//            string l = inf.Extension.ToLower();
//            return l.EndsWith("mp3") || l.EndsWith("wma") || l.EndsWith("aac");
//        }

//        public Task Convert(string Inputfile, string OutputFile)
//        {
//            if (!Initialized)
//            {
//                Initialized = true;
//                MediaFoundationApi.Startup();
//            }
//            return new Task(delegate ()
//            {
//                FileInfo inf = new FileInfo(OutputFile);
//                AudioFileReader FileR = new AudioFileReader(Inputfile);
//                string l = inf.Extension.ToLower();
//                if (l.EndsWith("mp3"))
//                {
//                    MediaFoundationEncoder.EncodeToMp3(FileR, OutputFile, Bitrate);
//                }
//                else if (l.EndsWith("wma"))
//                {
//                    MediaFoundationEncoder.EncodeToWma(FileR, OutputFile, Bitrate);
//                }
//                else if (l.EndsWith("aac"))
//                {
//                    MediaFoundationEncoder.EncodeToAac(FileR, OutputFile, Bitrate);
//                }
//            });
//        }
//    }
//}