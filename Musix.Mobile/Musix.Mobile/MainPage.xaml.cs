using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Musix.Core.Downloader;
using Musix.Core.Models;
using Musix.Core.Modules;
using System.IO;
using System.Threading;

namespace Musix.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private static Collector CoreCollector;
        private static MusixDownloadClient Downloader;
        public static string DataDIR = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public string GetLocalPath(string File)
        {
            return Path.Combine(DataDIR, File);
        }
        public string GetLocalPath(string Folder, string File)
        {
            return Path.Combine(DataDIR, Folder, File);
        }
        public MainPage()
        {
            InitializeComponent();
            //Environment.CurrentDirectory = DataDIR;
            CoreCollector = new Collector("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef");
            Downloader = new MusixDownloadClient() { Spotify = CoreCollector.Spotify, AudioCache = "AudioCache", ImageCachePath = "ImageCache" };
            Console.WriteLine($"AppData {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}");
            Console.WriteLine($"CommonAppData {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}");
            Console.WriteLine($"LocalAppData {Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}");
            Console.WriteLine($"CommonMusic {Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic)}");
            Console.WriteLine("======================");
            Console.WriteLine("======================" + Environment.CurrentDirectory);
            if (!Directory.Exists(GetLocalPath("ImageCache")))
            {
                Directory.CreateDirectory(GetLocalPath("ImageCache"));
            }
            if (!Directory.Exists(GetLocalPath("AudioCache")))
            {
                Directory.CreateDirectory(GetLocalPath("AudioCache"));
            }
            if (!Directory.Exists(GetLocalPath("Music")))
            {
                Directory.CreateDirectory(GetLocalPath("Music"));
            }
            if (!Directory.Exists(GetLocalPath("FFMPEG")))
            {
                Directory.CreateDirectory(GetLocalPath("FFMPEG"));
            }
            foreach (string f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)))
            {
                Console.WriteLine($"LFILE: " + f);
            }
            foreach (string f in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)))
            {
                Console.WriteLine($"LDIR: " + f);
            }
            Downloader.AudioCache = GetLocalPath("AudioCache");
            Downloader.ImageCachePath = GetLocalPath("ImageCache");
            Console.WriteLine("FFMPATH: " + GetLocalPath("FFMPEG"));
            Xabe.FFmpeg.FFmpeg.SetExecutablesPath(GetLocalPath("FFMPEG"));
            //DLFFM();
        }


        private async void DLFFM()
        {
            Console.WriteLine("Downloading FFMPEG...");
            await Xabe.FFmpeg.Downloader.FFmpegDownloader.GetLatestVersion(Xabe.FFmpeg.Downloader.FFmpegVersion.Official);
            Console.WriteLine("Finished downloading FFMPEG");
        }

        private void btnStartDownload_Clicked(object sender, EventArgs e)
        {
            MusixSongResult Result = CoreCollector.CollectByName(txtSongName.Text);
            DisplayAlert("Song Search", "Found song.", "OK");
            Thread RunT = new Thread(new ParameterizedThreadStart(RunDownload));
            RunT.Start(Result);
            Console.WriteLine("================================================================================================Finish");
        }
        private void RunDownload(object Res)
        {
            Console.WriteLine("Downloader Started.");
             Downloader.DownloadTrack((MusixSongResult)Res, GetLocalPath("Music"));
        }
    }
}
