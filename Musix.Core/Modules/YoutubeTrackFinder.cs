using Musix.Core.API;
using Musix.Core.Models;
using SpotifyAPI.Web.Models;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using Musix;
using Musix.Core;
namespace Musix.Core.Modules
{
    public static class YoutubeTrackFinder
    {
        public static Video FindYoutubeVideo(FullTrack Track, double MaxDeviation, IDetailsExtrapolator Extrapolator)
        {
            YoutubeClient Client = new YoutubeClient();
            Console.WriteLine("StartCheck");
            var task = Task.Run(async () => await Client.Search.GetVideosAsync($"{Track.Artists[0].Name} - {Track.Name}").CollectAsync());
            Console.WriteLine("Wait");
            task.Wait();
            Console.WriteLine("Res ");
            var results = task.Result;
            foreach (var video in results)
            {
                ExtrapResult Extrap = Extrapolator.ExtrapolateDetails(video.Title);
                if (Extrap.TrackName.ToLower().Contains(Track.Name.ToLower()) && (Extrap.TrackArtist == null || Extrap.TrackArtist.ToLower().Contains(Track.Artists[0].Name.ToLower())))
                {
                    if (video.Duration == null || Math.Abs(Track.DurationMs - video.Duration.Value.TotalMilliseconds) <= MaxDeviation)
                    {
                        return Client.Videos.GetAsync(video.Id).GetSync();
                    }
                }
            }
            return null;
        }
        public static async Task<Video> FindYoutubeVideoAsync(FullTrack Track, double MaxDeviation, IDetailsExtrapolator Extrapolator)
        {
            YoutubeClient Client = new YoutubeClient();
            await foreach (var video in Client.Search.GetVideosAsync($"{Track.Artists[0].Name} - {Track.Name}"))
            {
                ExtrapResult Extrap = Extrapolator.ExtrapolateDetails(video.Title);
                if (Extrap.TrackName.ToLower().Contains(Track.Name.ToLower()) && (Extrap.TrackArtist == null || Extrap.TrackArtist.ToLower().Contains(Track.Artists[0].Name.ToLower())))
                {
                    if ( Math.Abs(Track.DurationMs - video.Duration.Value.TotalMilliseconds) <= MaxDeviation)
                    {
                        return Client.Videos.GetAsync(video.Id).GetSync();
                    }
                }
            }
            return null;
        }
    }
}
