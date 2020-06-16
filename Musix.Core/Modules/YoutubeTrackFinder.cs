using Musix.Core.Models;
using SpotifyAPI.Web.Models;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace Musix.Core.Modules
{
    public static class YoutubeTrackFinder
    {
        public static Video FindYoutubeVideo(FullTrack Track, double MaxDeviation)
        {
            YoutubeClient Client = new YoutubeClient();
            Console.WriteLine("StartCheck");
            var task = Task.Run(async () => await Client.Search.GetVideosAsync($"{Track.Artists[0].Name} - {Track.Name}"));
            Console.WriteLine("Wait");
            task.Wait();
            Console.WriteLine("Res ");
            var results = task.Result;
            foreach (Video video in results)
            {
                ExtrapResult Extrap = TrackDetailsExtractor.ExtrapolateDetails(video.Title);
                if (Extrap.TrackName.ToLower().Contains(Track.Name.ToLower()) && (Extrap.TrackArtist == null || Extrap.TrackArtist.ToLower().Contains(Track.Artists[0].Name.ToLower())))
                    {
                    if (Math.Abs(Track.DurationMs - video.Duration.TotalMilliseconds) <= MaxDeviation)
                    {
                        return video;
                    }
                }
            }
            return null;
        }
    }
}
