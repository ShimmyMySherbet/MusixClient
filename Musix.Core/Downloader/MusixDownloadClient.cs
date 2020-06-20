using Musix.Core.API;
using Musix.Core.Components.Providers;
using Musix.Core.Helpers;
using Musix.Core.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using SpotifyImage = SpotifyAPI.Web.Models.Image;
using TagLibFile = TagLib.File;
using TagLibPicture = TagLib.Picture;

namespace Musix.Core.Downloader
{
    public class MusixDownloadClient
    {
        public SpotifyWebAPI Spotify;
        public YoutubeClient Client = new YoutubeClient();
        public string ImageCachePath;
        public string AudioCache;
        public ConversionProvider ConversionsProvider;

        public MusixDownloadClient()
        {
            ConversionsProvider = new XabeFFMPEGProvider();
        }

        public async Task DownloadTrack(MusixSongResult Track, string OutputDirectory, AudioEffectStack Effects = null)
        {
            bool HasEffects = Effects != null;
            if (HasEffects)
            {
                if (string.IsNullOrEmpty(Effects.AudioCachePath)) Effects.AudioCachePath = AudioCache;
            }


            Console.WriteLine("Start Download");
            if (!Track.HasVideo) Console.WriteLine("No Vid");
            if (!Track.HasVideo) return;
            string SourceAudio = Path.Combine(AudioCache, $"audio_{DateTime.Now.Ticks}");
            string AlbumCover = Path.Combine(ImageCachePath, $"cover_{DateTime.Now.Ticks}.jpg");
            string OutputFile = Path.Combine(OutputDirectory, FileHelpers.ScrubFileName($"{Track.SpotifyTrack.Artists[0].Name} - {Track.SpotifyTrack.Name}.mp3"));
            string MidConversionFile = Path.Combine(AudioCache, FileHelpers.ScrubFileName($"{DateTime.Now.Ticks}.mp3"));
            StreamManifest StreamData = await Client.Videos.Streams.GetManifestAsync(Track.YoutubeVideo.Id);
            List<AudioOnlyStreamInfo> AudioStreams = StreamData.GetAudioOnly().ToList();
            AudioStreams.OrderBy(dat => dat.Bitrate);
            if (AudioStreams.Count() == 0) Console.WriteLine("No Streams");
            if (AudioStreams.Count() == 0) return;
            IAudioStreamInfo SelectedStream = AudioStreams[0];
            Task AudioDownloadTask = Client.Videos.Streams.DownloadAsync(SelectedStream, SourceAudio);

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

            if (File.Exists(OutputFile)) File.Delete(ConversionFile);

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
            TagLib.Id3v2.AttachedPictureFrame Frame = new TagLib.Id3v2.AttachedPictureFrame(Pic);
            Frame.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
            Pic.Type = TagLib.PictureType.FrontCover;
            TagLib.IPicture[] Pics = { Pic };
            TLF.Tag.Pictures = Pics;

            TLF.Tag.Title = Track.SpotifyTrack.Name;
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
        }

        public DateTime? GetDate(string input)
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