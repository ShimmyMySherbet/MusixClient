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
        public AudioEffectStack AudioEffects = new AudioEffectStack();
        public SearchEntry()
        {
            InitializeComponent();
        }

        public SearchEntry(MusixSongResult result)
        {
            InitializeComponent();
            AudioEffects.AddEffect(new AudioNormalizer());
            Result = result;
            UITaskFactory = new TaskFactory(TaskScheduler);
            lblTrackName.Text = result.SpotifyTrack.Name;
            lblArtist.Text = result.SpotifyTrack.Album.Artists[0].Name;
            lblAlbum.Text = result.SpotifyTrack.Album.Name;
            new Thread(LoadArtwork).Start();
            Disposed += SearchEntry_Disposed;
        }

        private void SearchEntry_Disposed(object sender, EventArgs e)
        {
            MainWindow.Instance.UIAssetCache.DeregisterDependant(Result.SpotifyTrack.Id, this);
        }

        private void LoadArtwork()
        {
            if (MainWindow.Instance.UIAssetCache.AssetExists(Result.SpotifyTrack.Id))
            {
                MainWindow.Instance.UIAssetCache.RegisterDependant(Result.SpotifyTrack.Id, this);
                UITaskFactory.StartNew(() => pbArtwork.Image = MainWindow.Instance.UIAssetCache.GetAsset(Result.SpotifyTrack.Id));
            }
            else
            {
                using (WebClient webc = new WebClient())
                {
                    MemoryStream dlStream = new MemoryStream(webc.DownloadData(Result.SpotifyTrack.Album.Images[0].Url));
                    Image asset = pbArtwork.Image = Image.FromStream(dlStream);
                    UITaskFactory.StartNew(() => pbArtwork.Image = asset);
                    MainWindow.Instance.UIAssetCache.TryRegisterAsset(asset, Result.SpotifyTrack.Id);
                    MainWindow.Instance.UIAssetCache.RegisterDependant(Result.SpotifyTrack.Id, this);

                }
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
            Width = (Parent.Width - 10);
            Anchor = AnchorStyles.Left | AnchorStyles.Right;
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
            CancellationTokenSource source = new CancellationTokenSource();
            DownloadsManager.RegisterDownload(Result, source);
            await MainWindow.Instance.Client.DownloadTrack(Result, "Music", AudioEffects, source.Token);
            DownloadsManager.TryReleaseDownload(Result);
        }

        private void btnTrim_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowPopup(new TrimTrackPopup(TrimCallback));
        }
        private void TrimCallback(AudioTrimmer trimmer)
        {
            AudioEffects.RemoveEffectsOfType<AudioTrimmer>();
            AudioEffects.AddEffect(trimmer, 0);
        }
    }
}