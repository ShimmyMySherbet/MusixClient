using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Musix.Core.API;
using Musix.Core.Components;
using Musix.Core.Components.Providers;
using Musix.Core.Helpers;
using Musix.Core.Models;
using Musix.Core.Models.Debug;
using Musix.Core.Modules;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using SpotifyImage = SpotifyAPI.Web.Models.Image;
using TagLibFile = TagLib.File;
using TagLibPicture = TagLib.Picture;

namespace Musix.Core.Client
{
    public class MusixClient
    {
        public SpotifyWebAPI Spotify;
        public YoutubeClient YouTube = new YoutubeClient();
        public string ImageCachePath;
        public string AudioCache;
        public IConversionProvider ConversionsProvider;
        public IDetailsExtrapolator DetailsExtrapolator;
        private Token SpotifyToken;

        public delegate void OnReadyArgs();

        public event OnReadyArgs OnClientReady;

        public delegate void OnDownloadCompleteArgs(MusixSongResult Result);

        public event OnDownloadCompleteArgs OnMusixDownloadComplete;

        public bool Ready { get; protected set; }
        private readonly string SpotifyClientID;
        private readonly string SpotifyClientToken;

        public MusixClient(string SpotifyClientID, string SpotifyClientToken, string ImageCachePath = "", string AudioCachePath = "", IConversionProvider ConversionProvider = null)
        {
            if (ConversionProvider != null) this.ConversionsProvider = ConversionProvider;
            else this.ConversionsProvider = new XabeFFMPEGProvider();
            Spotify = new SpotifyWebAPI();
            AudioCache = AudioCachePath;
            this.ImageCachePath = ImageCachePath;
            this.SpotifyClientID = SpotifyClientID;
            this.SpotifyClientToken = SpotifyClientToken;
            DetailsExtrapolator = new DirectTrackExtrapolator();
        }

        public async void StartClient()
        {
            CredentialsAuth SpotifyAuth = new CredentialsAuth(SpotifyClientID, SpotifyClientToken);
            Token SpotifyToken = await SpotifyAuth.GetToken();
            Spotify.TokenType = SpotifyToken.TokenType;
            Spotify.AccessToken = SpotifyToken.AccessToken;
            this.SpotifyToken = SpotifyToken;
            OnClientReady?.Invoke();
        }

        public MusixSongResult CollectByName(string Term)
        {
            var SW = new StopWatch();
            var Tracks = Spotify.SearchItems(Term, SpotifyAPI.Web.Enums.SearchType.Track, 2);
            SW.PrintDur("Spotify Search");
            if (Tracks.HasError()) return null;
            if (Tracks.Tracks.Items.Count >= 1)
            {
                return Collect(Tracks.Tracks.Items[0]);
            }
            else
            {
                return null;
            }
        }

        public FullTrack FindTrack(ExtrapResult Ext, TimeSpan BaseLength, double MaxDeviation)
        {
            return SpotifyTrackFinder.FindTrack(Spotify, Ext, BaseLength, MaxDeviation);
        }

        public string ExtractSpotifyID(string URL)
        {
            string Modified = URL.RemoveBaseSubStrings("https://", "http://", "www.", "open.", "spotify", ".com", "/", "track", ":");
            if (Modified.Contains('?'))
            {
                Modified = Modified.Split('?')[0];
            }
            return Modified;
        }



        public MusixSongResult CollectByString(string Query)
        {
            
            if (Uri.IsWellFormedUriString(Query, UriKind.RelativeOrAbsolute))
            {
                if (Query.ToLower().Contains("youtu"))
                {
                    return Collect(Query);
                }
            }
            return CollectByName(Query);
        }


        public FullTrack GetTrackByURL(string URL)
        {
            string ID = ExtractSpotifyID(URL);
            return Spotify.GetTrack(ID);
        }

        public FullTrack GetTrackByID(string ID)
        {
            return Spotify.GetTrack(ID);
        }

        public MusixSongResult Collect(Video video)
        {
            MusixSongResult Result = new MusixSongResult();
            ExtrapResult Extrap = DetailsExtrapolator.ExtrapolateDetails(video.Title);
            FullTrack Track = FindTrack(Extrap, video.Duration, 8000);
            Result.Extrap = Extrap;
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            return Result;
        }

        public async Task<Video> GetVideoByURL(string URL)
        {
            return await YouTube.Videos.GetAsync(YoutubeHeleprs.GetVideoID(URL));
        }

        public MusixSongResult Collect(string VideoURL)
        {
            var GetVid = YouTube.Videos.GetAsync(YoutubeHeleprs.GetVideoID(VideoURL));
            GetVid.Wait();
            Video video = GetVid.Result;
            MusixSongResult Result = new MusixSongResult();
            ExtrapResult Extrap = DetailsExtrapolator.ExtrapolateDetails(video.Title);
            Result.Extrap = Extrap;
            FullTrack Track = FindTrack(Extrap, video.Duration, 5000);
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            return Result;
        }

        public MusixSongResult Collect(FullTrack Track)
        {
            MusixSongResult Result = new MusixSongResult
            {
                SpotifyTrack = Track,
                Extrap = new ExtrapResult() { TrackArtist = Track.Artists[0].Name, TrackName = Track.Name, Source = $"{string.Join(", ", Track.Artists)} - {Track.Name}" }
            };
            StopWatch DD = new StopWatch();
            Result.YoutubeVideo = YoutubeTrackFinder.FindYoutubeVideo(Track, 5000, DetailsExtrapolator);
            DD.PrintDur("Collector Youtube GetVid");
            Result.HasLyrics = false;
            return Result;
        }

        public async Task DownloadTrack(MusixSongResult Track, string OutputDirectory, AudioEffectStack Effects = null)
        {
            bool HasEffects = Effects != null;
            if (HasEffects)
            {
                Console.WriteLine("Has Effects");
                if (string.IsNullOrEmpty(Effects.AudioCachePath)) Effects.AudioCachePath = AudioCache;
            }

            Console.WriteLine("Start Download");
            if (!Track.HasVideo) Console.WriteLine("No Vid");
            if (!Track.HasVideo) return;
            string SourceAudio = Path.Combine(AudioCache, $"audio_source_{DateTime.Now.Ticks}");
            string AlbumCover = Path.Combine(ImageCachePath, $"cover_{DateTime.Now.Ticks}.jpg");
            string OutputFile = Path.Combine(OutputDirectory, FileHelpers.ScrubFileName($"{Track.SpotifyTrack.Artists[0].Name} - {Track.SpotifyTrack.Name.Replace("?", "").Trim(' ')}.mp3"));
            string MidConversionFile = Path.Combine(AudioCache, FileHelpers.ScrubFileName($"MidConversion_{DateTime.Now.Ticks}.mp3"));
            StreamManifest StreamData = await YouTube.Videos.Streams.GetManifestAsync(Track.YoutubeVideo.Id);
            List<AudioOnlyStreamInfo> AudioStreams = StreamData.GetAudioOnly().ToList();
            AudioStreams.OrderBy(dat => dat.Bitrate);
            if (AudioStreams.Count() == 0) Console.WriteLine("No Streams");
            if (AudioStreams.Count() == 0) return;
            IAudioStreamInfo SelectedStream = AudioStreams[0];
            Task AudioDownloadTask = YouTube.Videos.Streams.DownloadAsync(SelectedStream, SourceAudio);

            WebClient WebCl = new WebClient();

            SpotifyImage Cover = Track.SpotifyTrack.Album.Images[0];

            var CoverDownloadTask = new Task(() =>
            {
                Console.WriteLine("Downloading Cover");
                WebCl.DownloadFile(new Uri(Cover.Url), AlbumCover);
            }
            );
            CoverDownloadTask.Start();

            if (!AudioDownloadTask.IsCompleted)
            {
                Console.WriteLine("Waiting on artwork...");
                CoverDownloadTask.Wait();
            }
            if (!AudioDownloadTask.IsCompleted)
            {
                Console.WriteLine("Waiting on audio...");
                AudioDownloadTask.Wait();
                Console.WriteLine("Download Complete.");
            }
            Thread.Sleep(100);

            string ConversionFile = OutputFile;
            if (HasEffects) ConversionFile = MidConversionFile;

            if (File.Exists(OutputFile)) File.Delete(OutputFile);
            if (File.Exists(ConversionFile)) File.Delete(ConversionFile);

            //Convert
            Console.WriteLine("Starting Conversion...");
            await ConversionsProvider.Convert(SourceAudio, ConversionFile);
            Console.WriteLine("Conversion Complete.");

            if (HasEffects)
            {
                Effects.ApplyEffects(ConversionFile, OutputFile);
            }

            //Tags
            TagLib.Id3v2.Tag.DefaultVersion = 3;
            TagLib.Id3v2.Tag.ForceDefaultVersion = true;

            TagLibFile TLF = TagLibFile.Create(OutputFile);

            TagLibPicture Pic = new TagLibPicture(AlbumCover);
            TagLib.Id3v2.AttachedPictureFrame Frame = new TagLib.Id3v2.AttachedPictureFrame(Pic)
            {
                MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg
            };
            Pic.Type = TagLib.PictureType.FrontCover;
            TagLib.IPicture[] Pics = { Pic };
            TLF.Tag.Pictures = Pics;

            TLF.Tag.Title = Track.SpotifyTrack.Name.Split('-')[0].Trim(' ');
            TLF.Tag.Album = Track.SpotifyTrack.Album.Name;
            TLF.Tag.AlbumArtists = Track.SpotifyTrack.Album.Artists.CastEnumerable(x => x.Name).ToArray();
            TLF.Tag.Disc = (uint)Track.SpotifyTrack.DiscNumber;
            TLF.Tag.AlbumSort = Track.SpotifyTrack.Album.AlbumType;
            DateTime? DT = GetDate(Track.SpotifyTrack.Album.ReleaseDate);
            if (DT.HasValue) TLF.Tag.Year = (uint)DT.Value.Year;
            TLF.Save();

            // Clean Up
            WebCl.Dispose();
            TLF.Dispose();
            Console.WriteLine("Done.");
            OnMusixDownloadComplete?.Invoke(Track);
        }

        protected DateTime? GetDate(string input)
        {
            if (input.Split('-').Length == 3)
            {
                short Y = Convert.ToInt16(input.Split('-')[0]);
                short M = Convert.ToInt16(input.Split('-')[1]);
                short D = Convert.ToInt16(input.Split('-')[2]);
                return new DateTime(Y, M, D);
            }
            else
            {
                return null;
            }
        }
    }
}