using Musix.Core.Downloader;
using Musix.Core.Models;
using Musix.Core.Modules;
using System;
using System.Threading.Tasks;
using TagLib.IFD.Entries;
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
            if (Video.Contains("http"))
            {
                c.WriteLine("Collection by URL...");
                result = CoreCollector.Collect(Video);
            }
            else
            {
                c.WriteLine("Collection by Term...");
                result = CoreCollector.CollectByName(Video);
            }
            if (result.HasTrack)
            {
                c.WriteLine();
                c.WriteLine($"Track URL: {result.SpotifyTrack.Uri}");
                c.WriteLine($"Track: {result.SpotifyTrack.Name}");
                await Downloader.DownloadTrack(result, "Music");
            }
            else
            {
                c.WriteLine("Failed.");
            }
            c.WriteLine();
        }
    }
}