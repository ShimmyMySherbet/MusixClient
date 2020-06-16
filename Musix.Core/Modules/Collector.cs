using Musix.Core.Helpers;
using Musix.Core.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace Musix.Core.Modules
{
    public class Collector
    {
        public SpotifyWebAPI Spotify;
        private bool SpotifyAPIReady = false;
        public bool Ready { get { return SpotifyAPIReady; } }
        public Token SpotifyToken;

        public Collector(string SpotifyClientID, string SpotifyClientToken)
        {
            Spotify = new SpotifyWebAPI();
            StartSpotifyAPI(SpotifyClientID, SpotifyClientToken);
        }
        private async void StartSpotifyAPI(string SpotifyClientID, string SpotifyClientToken)
        {
            CredentialsAuth SpotifyAuth = new CredentialsAuth(SpotifyClientID, SpotifyClientToken);
            Token SpotifyToken = await SpotifyAuth.GetToken();
            Spotify.TokenType = SpotifyToken.TokenType;
            Spotify.AccessToken = SpotifyToken.AccessToken;
            this.SpotifyToken = SpotifyToken;
            SpotifyAPIReady = true;
        }

        public MusixSongResult CollectByName(string Term)
        {
            var SW = new StopWatch();
            var Tracks = Spotify.SearchItems(Term, SpotifyAPI.Web.Enums.SearchType.Track, 2);
            SW.PrintDur("Spotify Search");
            if (Tracks.Tracks.Items.Count >= 1)
            {
                return Collect(Tracks.Tracks.Items[0]);
            }  else
            {
                return null;
            }
        }

        public FullTrack FindTrack(ExtrapResult Ext, TimeSpan BaseLength, double MaxDeviation)
        {
            return SpotifyTrackFinder.FindTrack(Spotify, Ext, BaseLength, MaxDeviation);
        }

        public MusixSongResult Collect(Video video)
        {
            MusixSongResult Result = new MusixSongResult();
            ExtrapResult Extrap = TrackDetailsExtractor.ExtrapolateDetails(video.Title);
            FullTrack Track = FindTrack(Extrap, video.Duration, 5000);
            Result.Extrap = Extrap;
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            return Result;
        }
        public MusixSongResult Collect(string VideoURL)
        {
            YoutubeClient Client = new YoutubeClient();
            var GetVid = Client.Videos.GetAsync(YoutubeHeleprs.GetVideoID(VideoURL));
            GetVid.Wait();
            Video video = GetVid.Result;
            MusixSongResult Result = new MusixSongResult();
            ExtrapResult Extrap = TrackDetailsExtractor.ExtrapolateDetails(video.Title);
            Result.Extrap = Extrap;
            FullTrack Track = FindTrack(Extrap, video.Duration, 5000);
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            return Result;
        }


        public MusixSongResult Collect(FullTrack Track)
        {
           
            MusixSongResult Result = new MusixSongResult();
            Result.SpotifyTrack = Track;
            Result.Extrap = new ExtrapResult() { TrackArtist = Track.Artists[0].Name, TrackName = Track.Name, Source = $"{string.Join(", ", Track.Artists)} - {Track.Name}" };
             StopWatch DD = new StopWatch();
            Result.YoutubeVideo = YoutubeTrackFinder.FindYoutubeVideo(Track, 5000);
            DD.PrintDur("Collector Youtube GetVid");
            Result.HasLyrics = false;
            return Result;
        }
    }
}
