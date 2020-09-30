using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Musix.Core.Client;
using Musix.Core.Components.AudioModifiers;
using Musix.Core.Models;
using SpotifyAPI.Web.Models;
using YoutubeExplode.Videos;
using c = System.Console;

namespace Musix.Console
{
    internal class Program
    {
        private static MusixClient Client;
        private static List<Assembly> Assem = new List<Assembly>();

        private static void Main()
        {
            Client = new MusixClient("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef", "ImageCache", "AudioCache");
            Client.StartClient();
            Client.OnClientReady += Client_OnClientReady;
            c.WriteLine($"Creating client with conversion provider {Client.ConversionsProvider.GetType().FullName}");
            Thread.Sleep(-1);
        }

        private static void Client_OnClientReady()
        {
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
                result = Client.Collect(Client.GetTrackByURL(Video));
            }
            else if (Video.Contains("youtu"))
            {
                c.WriteLine("Collection by URL...");
                result = Client.Collect(Video);
            }
            else if (Video.Contains("!dr"))
            {
                c.Write("Youtube Video: ");
                string yt = c.ReadLine();
                c.Write("Spotify: ");
                string sp = c.ReadLine();

                Video V = await Client.GetVideoByURL(yt);
                FullTrack Track = Client.GetTrackByURL(sp);

                result = new MusixSongResult() { Extrap = new ExtrapResult(), HasLyrics = false, SpotifyTrack = Track, YoutubeVideo = V };
            }
            else
            {
                c.WriteLine("Collection by Term...");
                result = Client.CollectByName(Video);
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

                await Client.DownloadTrack(result, "Music", Stack);
            }
            else
            {
                c.WriteLine("Failed.");
            }
            c.WriteLine();
        }
    }
}