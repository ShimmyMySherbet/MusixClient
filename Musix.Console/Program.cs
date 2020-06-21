using Musix.Core.Components.AudioModifiers;
using Musix.Core.Components.Providers;
using Musix.Core.Downloader;
using Musix.Core.Models;
using Musix.Core.Models.Debug;
using Musix.Core.Modules;
using System;
using System.Threading.Tasks;
using c = System.Console;

namespace Musix.Console
{
    internal class Program
    {
        private static Collector CoreCollector;
        private static MusixDownloadClient Downloader;

        private static void Main(string[] args)
        {
            CoreCollector = new Collector("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef");
            Downloader = new MusixDownloadClient() { Spotify = CoreCollector.Spotify, AudioCache = "AudioCache", ImageCachePath = "ImageCache" };

            while (true)
            {
                Task T = RunSVC();
                T.Wait();
            }
        }

        private static async Task RunSVC()
        {
            c.Write("Query: ");
            string Video = c.ReadLine();
            MusixSongResult result;
            AudioEffectStack Stack = null;
            if (Video.ToLower().Contains("spotify"))
            {
                result = CoreCollector.Collect(CoreCollector.GetTrackByURL(Video));
            }
            else if (Video.Contains("youtu"))
            {
                c.WriteLine("Collection by URL...");
                result = CoreCollector.Collect(Video);
            }
            else
            {
                c.WriteLine("Collection by Term...");
                result = CoreCollector.CollectByName(Video);
            }


            if (result != null && result.HasTrack)
            {
                c.WriteLine();
                c.WriteLine($"Track URL: {result.SpotifyTrack.Uri}");
                c.WriteLine($"Track: {result.SpotifyTrack.Name}");
                c.WriteLine();

                c.Write("Apply Effects: [Y/N] ");
                string Sres = c.ReadLine();
                if (Sres.ToLower() == "y")
                {
                    Stack = new AudioEffectStack();
                    c.Write("Normalize: [Y/N] ");
                    string Nres = c.ReadLine();
                    if (Nres.ToLower() == "y") Stack.AddEffect(new AudioNormalizer());


                    c.Write("Crop: [Y/N] ");
                    string Cres = c.ReadLine();
                    if (Cres.ToLower() == "y")
                    {
                        c.Write("Start Time (sec): ");
                        int StartT = Convert.ToInt32(c.ReadLine());

                        c.Write("End Time (sec): ");
                        int EndT = Convert.ToInt32(c.ReadLine());
                        TimeSpan? Startd = null;
                        if (StartT != -1) Startd = new TimeSpan(0, 0, StartT);
                        TimeSpan? EndD = null;
                        if (EndT != -1) EndD = new TimeSpan(0, 0, EndT);

                        AudioTrimmer Trimmer = new AudioTrimmer(Startd, EndD);

                        Stack.AddEffect(Trimmer);
                    }

                }

                await Downloader.DownloadTrack(result, "Music", Stack);
            }
            else
            {
                c.WriteLine("Failed.");
            }
            c.WriteLine();
        }
    }
}