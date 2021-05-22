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
using Musix.Core.Models.Interfaces;
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
        public IMusixCollector Collector;
        private Token SpotifyToken;

        public Delegates.DownloadProgressChangedCallback ProgressChangedCallback;

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

        public async Task<MusixSongResult> CollectByName(string Term)
        {
            var SW = new StopWatch();
            var Tracks = Spotify.SearchItems(Term, SpotifyAPI.Web.Enums.SearchType.Track, 2);
            SW.PrintDur("Spotify Search");
            if (Tracks.HasError()) return null;
            if (Tracks.Tracks.Items.Count >= 1)
            {
                return await Collect(Tracks.Tracks.Items[0]);
            }
            else
            {
                return null;
            }
        }


        public async Task<MusixSongResult> CollectByNameAsync(string Term)
        {
            var SW = new StopWatch();
            var Tracks = Spotify.SearchItems(Term, SpotifyAPI.Web.Enums.SearchType.Track, 2);
            SW.PrintDur("Spotify Search");
            if (Tracks.HasError()) return null;
            if (Tracks.Tracks.Items.Count >= 1)
            {
                return await CollectAsync(Tracks.Tracks.Items[0]);
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

        public async Task<MusixSongResult> CollectByString(string Query)
        {
            if (Uri.IsWellFormedUriString(Query, UriKind.RelativeOrAbsolute))
            {
                if (Query.ToLower().Contains("youtu"))
                {
                    return await Collect(Query);
                }
            }
            return await CollectByName(Query);
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
            FullTrack Track = FindTrack(Extrap, (video.Duration.HasValue ? video.Duration.Value : TimeSpan.FromSeconds(0)), 8000);
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

        public async Task<MusixSongResult> Collect(string VideoURL)
        {
            Console.WriteLine("get id");
            var video = await YouTube.Videos.GetAsync(YoutubeHeleprs.GetVideoID(VideoURL));
            MusixSongResult Result = new MusixSongResult();
            Console.WriteLine("run extrap");
            ExtrapResult Extrap = DetailsExtrapolator.ExtrapolateDetails(video.Title);
            Result.Extrap = Extrap;
            FullTrack Track = FindTrack(Extrap, (video.Duration.HasValue ? video.Duration.Value : TimeSpan.Zero), 5000);
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            Console.WriteLine("ret.");
            return Result;
        }

        public async Task<MusixSongResult> CollectAsync(string VideoURL)
        {
            string ID = YoutubeHeleprs.GetVideoID(VideoURL);
            Console.WriteLine($"VID: {ID}");
            Video video = await YouTube.Videos.GetAsync(ID);
            MusixSongResult Result = new MusixSongResult();
            Console.WriteLine("run extrap");
            ExtrapResult Extrap = DetailsExtrapolator.ExtrapolateDetails(video.Title);
            Result.Extrap = Extrap;
            FullTrack Track = FindTrack(Extrap, (video.Duration.HasValue ? video.Duration.Value : TimeSpan.Zero), 5000);
            Result.HasLyrics = false;
            Result.SpotifyTrack = Track;
            Result.YoutubeVideo = video;
            Console.WriteLine("ret.");
            return Result;
        }

        public async Task<MusixSongResult> Collect(FullTrack Track)
        {
            MusixSongResult Result = new MusixSongResult
            {
                SpotifyTrack = Track,
                Extrap = new ExtrapResult() { TrackArtist = Track.Artists[0].Name, TrackName = Track.Name, Source = $"{string.Join(", ", Track.Artists)} - {Track.Name}" }
            };
            StopWatch DD = new StopWatch();
            Result.YoutubeVideo = await YoutubeTrackFinder.FindYoutubeVideoAsync(Track, 5000, DetailsExtrapolator);
            DD.PrintDur("Collector Youtube GetVid");
            Result.HasLyrics = false;
            return Result;
        }

        public async Task<MusixSongResult> CollectAsync(FullTrack Track)
        {
            MusixSongResult Result = new MusixSongResult
            {
                SpotifyTrack = Track,
                Extrap = new ExtrapResult() { TrackArtist = Track.Artists[0].Name, TrackName = Track.Name, Source = $"{string.Join(", ", Track.Artists)} - {Track.Name}" }
            };
            StopWatch DD = new StopWatch();
            Result.YoutubeVideo = await YoutubeTrackFinder.FindYoutubeVideoAsync(Track, 5000, DetailsExtrapolator);
            Result.HasLyrics = false;
            return Result;
        }

        private void TryCallback(int step, int max, string status, MusixSongResult download)
        {
            ProgressChangedCallback?.Invoke(step, max, status, download);
        }

        public async Task DownloadTrack(MusixSongResult Track, string OutputDirectory, AudioEffectStack Effects = null, CancellationToken cancellationToken = default)
        {
            int Steps;
            int Step = 0;
            if (Effects == null)
            {
                Steps = 9;
            }
            else
            {
                Steps = 9 + Effects.EffectCount;
            }
            TryCallback(Step, Steps, "Starting Download", Track);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            bool HasEffects = Effects != null;
            if (HasEffects)
            {
                Console.WriteLine("Has Effects");
                if (string.IsNullOrEmpty(Effects.AudioCachePath)) Effects.AudioCachePath = AudioCache;
            }
            // Step 1
            Step++;
            TryCallback(Step, Steps, "Preparing Download", Track);

            Console.WriteLine("Start Download");
            if (!Track.HasVideo) Console.WriteLine("No Vid");
            if (!Track.HasVideo) return;
            string SourceAudio = Path.Combine(AudioCache, $"audio_source_{DateTime.Now.Ticks}");
            string AlbumCover = Path.Combine(ImageCachePath, $"cover_{DateTime.Now.Ticks}.jpg");
            string OutputFile = Path.Combine(OutputDirectory, FileHelpers.ScrubFileName($"{Track.SpotifyTrack.Artists[0].Name} - {Track.SpotifyTrack.Name.Replace("?", "").Trim(' ')}.mp3"));
            string MidConversionFile = Path.Combine(AudioCache, FileHelpers.ScrubFileName($"MidConversion_{DateTime.Now.Ticks}.mp3"));
            // Step 2
            Step++;
            TryCallback(Step, Steps, "Aquiring streams", Track);
            StreamManifest StreamData = await YouTube.Videos.Streams.GetManifestAsync(Track.YoutubeVideo.Id);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            // Step 3
            Step++;
            TryCallback(Step, Steps, "Sorting Streams", Track);
            List<AudioOnlyStreamInfo> AudioStreams = StreamData.GetAudioOnlyStreams().ToList();
            AudioStreams.OrderBy(dat => dat.Bitrate);
            if (AudioStreams.Count() == 0) Console.WriteLine("No Streams");
            if (AudioStreams.Count() == 0) return;
            IAudioStreamInfo SelectedStream = AudioStreams[0];
            // Step 4
            Step++;
            TryCallback(Step, Steps, "Starting downloads", Track);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            //Task AudioDownloadTask = new Task(async () => await YouTube.Videos.Streams.DownloadAsync(SelectedStream, SourceAudio));

            var req = WebRequest.CreateHttp(SelectedStream.Url);
            req.Method = "GET";
            using(var resp = req.GetResponse()) 
                using(var network = resp.GetResponseStream())
                using(var fs = new FileStream(SourceAudio, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                Console.WriteLine("Downloading");
                await network.CopyToAsync(fs);
                Console.WriteLine("flushing");

                await fs.FlushAsync();
                Console.WriteLine("done");

            }

            WebClient WebCl = new WebClient();

            Step++;
            TryCallback(Step, Steps, "Starting", Track);
            SpotifyImage Cover = Track.SpotifyTrack.Album.Images[0];
            var CoverDownloadTask = new Task(() =>
            {
                Console.WriteLine("Downloading Cover");
                WebCl.DownloadFile(new Uri(Cover.Url), AlbumCover);
            }
            );
            CoverDownloadTask.Start();
            Step++;
            TryCallback(Step, Steps, "Waiting for downloads", Track);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            //if (!AudioDownloadTask.IsCompleted)
            //{
            //    Console.WriteLine("Waiting on artwork...");
            //    CoverDownloadTask.Wait();
            //}
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            //if (!AudioDownloadTask.IsCompleted)
            //{
            //    Console.WriteLine("Waiting on audio...");
            //    AudioDownloadTask.Wait();
            //    Console.WriteLine("Download Complete.");
            //}
            Thread.Sleep(100);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            string ConversionFile = OutputFile;
            if (HasEffects) ConversionFile = MidConversionFile;

            if (File.Exists(OutputFile)) File.Delete(OutputFile);
            if (File.Exists(ConversionFile)) File.Delete(ConversionFile);

            Step++;
            TryCallback(Step, Steps, "Transcoding audio to mp3", Track);
            // Step 8
            Console.WriteLine("Starting Conversion...");
            await ConversionsProvider.Convert(SourceAudio, ConversionFile);
            Console.WriteLine("Conversion Complete.");
            // Step 9
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            if (HasEffects)
            {
                Step++;
                int InternalStep = Step;
                TryCallback(Step, Steps, "Applying audio effects", Track);
                Effects.ApplyEffects(ConversionFile, OutputFile, (step, stepmax, status, download) =>
                {
                    step++;
                    TryCallback(Step, Steps, status, Track);
                }, cancellationToken);
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Step++;
            TryCallback(Step, Steps, "Applying ID3 metadata tags", Track);
            // Step 10
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
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            TLF.Save();

            // Clean Up
            // Step 11

            Step++;
            TryCallback(Step, Steps, "Cleaning up", Track);
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