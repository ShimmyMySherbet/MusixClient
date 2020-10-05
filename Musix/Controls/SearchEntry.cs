using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Core.Components.AudioModifiers;
using Musix.Core.Models;
using Musix.Managers;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Themes;

namespace Musix.Controls
{
    public partial class SearchEntry : UserControl, IStyleableControl
    {
        public TaskScheduler TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        public TaskFactory UITaskFactory;
        public MusixSongResult Result;

        public SearchEntry()
        {
            InitializeComponent();
        }

        public SearchEntry(MusixSongResult result)
        {
            InitializeComponent();
            Result = result;
            UITaskFactory = new TaskFactory(TaskScheduler);
            lblTrackName.Text = result.SpotifyTrack.Name;
            lblArtist.Text = result.SpotifyTrack.Album.Artists[0].Name;
            lblAlbum.Text = result.SpotifyTrack.Album.Name;
            new Thread(LoadArtwork).Start();
        }

        private void LoadArtwork()
        {
            using (WebClient webc = new WebClient())
            {
                MemoryStream dlStream = new MemoryStream(webc.DownloadData(Result.SpotifyTrack.Album.Images[0].Url));
                UITaskFactory.StartNew(() => pbArtwork.Image = Image.FromStream(dlStream));
            }
        }

        public void SendStyle(EStyle Style)
        {
            if (Style == EStyle.Blue)
            {
                pbDownload.Image = Assets.DownloadMain_Blue;
            }
            else if (Style == EStyle.Color)
            {
                pbDownload.Image = Assets.DownloadMain_Color;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void SearchEntry_Load(object sender, EventArgs e)
        {
            Width = (Parent.Width - 30);
        }

        private void pbDownload_Click(object sender, EventArgs e)
        {
            if (!DownloadsManager.IsDownloading(Result))
            {
                new Thread(() => Download()).Start();
            }
        }

        private async void Download()
        {
            DownloadsManager.RegisterDownload(Result);
            AudioEffectStack stack = new AudioEffectStack();
            stack.AddEffect(new AudioNormalizer());
            await MainWindow.Instance.Client.DownloadTrack(Result, "Music", stack);
            DownloadsManager.TryReleaseDownload(Result);
        }
    }
}