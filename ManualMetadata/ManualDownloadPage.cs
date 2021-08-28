using Musix;
using Musix.Core.Components.AudioModifiers;
using Musix.Core.Models;
using Musix.Managers;
using SpotifyAPI.Web.Models;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos;
using Image = System.Drawing.Image;

namespace ManualMetadata
{
    public partial class ManualDownloadPage : UserControl
    {
        public Stream CoverStream;

        public ManualDownloadPage()
        {
            InitializeComponent();
        }

        private void onDownload(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            try
            {
                AudioTrimmer trimmer = null;
                var txtS = txtStart.Text;
                var txtE = txtEnd.Text;

                if (!string.IsNullOrEmpty(txtS) || !string.IsNullOrEmpty(txtE))
                {
                    TimeSpan? start = null;
                    TimeSpan? end = null;

                    if (txtS.Contains(":"))
                    {
                        var prt1 = txtS.Split(':')[0];
                        var prt2 = txtS.Remove(0, prt1.Length + 1);

                        if (int.TryParse(prt1, out var min) && int.TryParse(prt2, out var sec))
                        {
                            start = TimeSpan.FromSeconds((min * 60) + sec);
                        }
                        else
                        {
                            MessageBox.Show("Invalid start time.");
                            return;
                        }
                    }
                    else if (int.TryParse(txtS, out var c))
                    {
                        start = TimeSpan.FromSeconds(c);
                    }
                    else
                    {
                        MessageBox.Show("Invalid start time.");
                        return;
                    }

                    if (txtS.Contains(":"))
                    {
                        var prt1 = txtE.Split(':')[0];
                        var prt2 = txtE.Remove(0, prt1.Length + 1);

                        if (int.TryParse(prt1, out var min) && int.TryParse(prt2, out var sec))
                        {
                            end = TimeSpan.FromSeconds((min * 60) + sec);
                        }
                        else
                        {
                            MessageBox.Show("Invalid end time.");
                            return;
                        }
                    }
                    else if (int.TryParse(txtE, out var c))
                    {
                        end = TimeSpan.FromSeconds(c);
                    }
                    else
                    {
                        MessageBox.Show("Invalid end time.");
                        return;
                    }

                    trimmer = new AudioTrimmer(start, end);
                }

                var id = VideoId.TryParse(txtYoutube.Text);

                if (id == null)
                {
                    MessageBox.Show("Invalid Youtube URL");
                    return;
                }

                var context = new DLContext()
                {
                    YTID = id.Value,
                    AlbumName = txtAlbum.Text,
                    Artwork = CoverStream,
                    ArtistName = txtArtist.Text,
                    TrackName = txtTitle.Text,
                    CoverStream = CoverStream,
                    Trimmer = trimmer
                };

                ThreadPool.QueueUserWorkItem(async (_) => await RunDownloader(context));
            }
            finally
            {
                btnDownload.Enabled = true;
                txtAlbum.Text = "";
                txtArtist.Text = "";
                txtEnd.Text = "";
                txtStart.Text = "";
                txtTitle.Text = "";
                txtYoutube.Text = "";
                lblArtowork.Show();
                pbArtwork.Image = null;
                CoverStream = null;
            }
        }

        private async Task RunDownloader(DLContext context)
        {
            var client = new YoutubeClient();
            var video = await client.Videos.GetAsync(context.YTID);

            var dur = 300;
            if (video.Duration != null)
            {
                dur = (int)video.Duration.Value.TotalMilliseconds;
            }

            var track = new FullTrack() // Fake spotify track for old downloader
            {
                DiscNumber = 1,
                DurationMs = dur,
                Album = new SimpleAlbum()
                {
                    ReleaseDate = DateTime.Now.ToShortDateString(),
                    Artists = new System.Collections.Generic.List<SimpleArtist>() { new SimpleArtist() { Name = context.ArtistName } },
                    Name = context.AlbumName,
                    Images = new System.Collections.Generic.List<SpotifyAPI.Web.Models.Image>()
                    {
                        new SpotifyAPI.Web.Models.Image() { Height = 500, Width = 500, Url = "https://i.ibb.co/Khm4k4z/Default-Album.png" }
                    },
                    TotalTracks = 1
                },
                Artists = new System.Collections.Generic.List<SimpleArtist>()
                {
                    new SimpleArtist() { Name = context.ArtistName }
                },
                Explicit = true,
                Name = context.TrackName,
                IsPlayable = false,
                TrackNumber = 1,
                AvailableMarkets = new System.Collections.Generic.List<string>(),
                Error = null,
                ExternalIds = new System.Collections.Generic.Dictionary<string, string>(),
                ExternUrls = new System.Collections.Generic.Dictionary<string, string>(),
                Href = "",
                Id = "N/A",
                Popularity = 1,
                PreviewUrl = "",
                Restrictions = new System.Collections.Generic.Dictionary<string, string>(),
                Type = "Emulated",
                Uri = ""
            };

            var result = new MusixSongResult()
            {
                HasLyrics = false,
                Lyrics = null,
                SpotifyTrack = track,
                YoutubeVideo = video,
                Extrap = new ExtrapResult() { Source = "YouTube", TrackArtist = context.ArtistName, TrackName = context.TrackName }
            };

            var effects = new AudioEffectStack();
            if (context.Trimmer != null)
            {
                effects.AddEffect(context.Trimmer);
            }
            effects.AddEffect(new AudioNormalizer());

            CancellationTokenSource source = new CancellationTokenSource();
            DownloadsManager.RegisterDownload(result, source);
            await MainWindow.Instance.Client.DownloadTrack(result, "Music", effects, source.Token, context.CoverStream);
            context?.CoverStream?.Dispose();
            DownloadsManager.TryReleaseDownload(result);
        }

        private void selectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "Images|*.jpg;*.png;*.jpeg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    CoverStream = new FileStream(ofd.FileName, FileMode.Open);

                    var img = Image.FromStream(CoverStream);

                    CoverStream.Position = 0;
                    pbArtwork.Image = img;
                    lblArtowork.Hide();
                }
            }
        }

        [STAThread]
        private void pasteArtworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clip = Clipboard.GetImage();
            pbArtwork.Image = clip;
            lblArtowork.Hide();
            CoverStream = new MemoryStream();
            clip.Save(CoverStream, ImageFormat.Jpeg);
        }
    }
}