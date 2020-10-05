using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Core.Models;
using Musix.Managers;
using Musix.Models;

namespace Musix.Controls
{
    public partial class ActiveDownloadControl : UserControl
    {
        public TaskFactory UITaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        public MusixSongResult Result;

        public ActiveDownloadControl()
        {
            InitializeComponent();
        }

        public ActiveDownloadControl(MusixSongResult result)
        {
            InitializeComponent();
            Result = result;
            lblAlbumName.Text = result.SpotifyTrack.Album.Name;
            lblArtistName.Text = result.SpotifyTrack.Artists[0].Name;
            lblTrackName.Text = result.SpotifyTrack.Name;
            new Thread(LoadArtwork).Start();
            this.Disposed += ActiveDownloadControl_Disposed;
            Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }

        private void ActiveDownloadControl_Disposed(object sender, EventArgs e)
        {
            if (Result != null)
                MainWindow.Instance.UIAssetCache.DeregisterDependant(Result.SpotifyTrack.Id, this);
        }

        private void ActiveDownloadControl_Load(object sender, EventArgs e)
        {
            lblStatus.CentreControl();
            lblStatus.TextChanged += LblStatus_TextChanged;
            SizeChanged += ActiveDownloadControl_SizeChanged;
            Width = Parent.Width - 10;
        }

        public void SetProgress(int Step, int Max, string Status)
        {

            if (pbprogress.Maximum != Max)
                pbprogress.Maximum = Max;
            pbprogress.Value = Step;
            lblStatus.Text = Status;
        }

        private void ActiveDownloadControl_SizeChanged(object sender, EventArgs e)
        {
            lblStatus.CentreControlPreferred();

        }

        private void LblStatus_TextChanged(object sender, EventArgs e)
        {
            lblStatus.CentreControlPreferred();
        }

        private void LoadArtwork()
        {
            if (MainWindow.Instance.UIAssetCache.AssetExists(Result.SpotifyTrack.Id))
            {
                Console.WriteLine("got from cache");
                MainWindow.Instance.UIAssetCache.RegisterDependant(Result.SpotifyTrack.Id, this);
                UITaskFactory.StartNew(() => pbArtwork.Image = MainWindow.Instance.UIAssetCache.GetAsset(Result.SpotifyTrack.Id));
            }
            else
            {
                Console.WriteLine("re-downlaod");
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

        private void btnCancelDownload_Click(object sender, EventArgs e)
        {
            DownloadsManager.CancelDownload(Result);
        }
    }
}