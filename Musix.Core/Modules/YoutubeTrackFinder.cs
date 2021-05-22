using Musix.Core.API;
using Musix.Core.Models;
using SpotifyAPI.Web.Models;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;

namespace Musix.Core.Modules
{
    public static class YoutubeTrackFinder
    {
        //public static async Task<Video> FindYoutubeVideo(FullTrack Track, double MaxDeviation, IDetailsExtrapolator Extrapolator)
        //{
        //    YoutubeClient Client = new YoutubeClient();

        //    var results = await Client.Search.GetVideosAsync($"{Track.Artists[0].Name} - {Track.Name}").CollectAsync();

        //    foreach (var video in results)
        //    {
        //        ExtrapResult Extrap = Extrapolator.ExtrapolateDetails(video.Title);
        //        if (Extrap.TrackName.ToLower().Contains(Track.Name.ToLower()) && (Extrap.TrackArtist == null || Extrap.TrackArtist.ToLower().Contains(Track.Artists[0].Name.ToLower())))
        //        {
        //            if (Math.Abs(Track.DurationMs - video.Duration.Value.TotalMilliseconds) <= MaxDeviation)
        //            {
        //                return await Client.Videos.GetAsync(video.Id);
        //            }
        //        }
        //    }
        //    return null;
        //}
        public static async Task<Video> FindYoutubeVideoAsync(FullTrack Track, double MaxDeviation, IDetailsExtrapolator Extrapolator)
        {
            YoutubeClient Client = new YoutubeClient();
            var results = await Client.Search.GetVideosAsync($"{Track.Artists[0].Name} - {Track.Name}").CollectAsync();
            foreach (var video in results)
            {
                ExtrapResult Extrap = Extrapolator.ExtrapolateDetails(video.Title);
                if (Extrap.TrackName.ToLower().Contains(Track.Name.ToLower()) && (Extrap.TrackArtist == null || Extrap.TrackArtist.ToLower().Contains(Track.Artists[0].Name.ToLower())))
                {
                    if (Math.Abs(Track.DurationMs - video.Duration.Value.TotalMilliseconds) <= MaxDeviation)
                    {
                        return await Client.Videos.GetAsync(video.Id);
                    }
                }
            }
            return null;
        }
    }
}
