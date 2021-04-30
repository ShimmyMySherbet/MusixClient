using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Client;
using Musix.Core.Helpers;
using Musix.Core.Models.Interfaces;
using SpotifyAPI.Web.Models;
using YoutubeExplode.Videos;

namespace Musix.Core.Models.Components
{
    public class MusixDataCollector : IMusixCollector
    {
        public MusixClient Client;
        public MusixDataCollector(MusixClient client) => Client = client;


        public MusixSongResult Collect(string VideoURL)
        {
            Console.WriteLine("get id");
            var GetVid = Client.YouTube.Videos.GetAsync(YoutubeHeleprs.GetVideoID(VideoURL));
            Console.WriteLine("get wait");
            GetVid.Wait();
            Console.WriteLine("got vid");
            Video video = GetVid.Result;
            MusixSongResult Result = new MusixSongResult();
            Console.WriteLine("run extrap");
            ExtrapResult Extrap = Client.DetailsExtrapolator.ExtrapolateDetails(video.Title);
            Result.Extrap = Extrap;
            FullTrack Track = Client.FindTrack(Extrap, video.Duration, 5000);
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            Console.WriteLine("ret.");
            return Result;
        }

        public MusixSongResult Collect(FullTrack track)
        {
            throw new NotImplementedException();
        }

        public Task<MusixSongResult> CollectAsync(string VideoURL)
        {
            throw new NotImplementedException();
        }

        public Task<MusixSongResult> CollectAsync(Video video)
        {
            throw new NotImplementedException();
        }

        public Task<MusixSongResult> CollectAsync(FullTrack track)
        {
            throw new NotImplementedException();
        }
        public MusixSongResult Collect(Video video)
        {
            MusixSongResult Result = new MusixSongResult();
            ExtrapResult Extrap = Client. DetailsExtrapolator.ExtrapolateDetails(video.Title);
            FullTrack Track = Client.FindTrack(Extrap, video.Duration, 8000);
            Result.Extrap = Extrap;
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            return Result;
        }

    }
}
