using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Models;

namespace Musix.Controls.Pages
{
    public partial class SearchPage : UserControl, IStyleableControl
    {
        public MainWindow Main;

        public SearchPage(MainWindow Main)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.Main = Main;
        }

        private void SearchPage_Load(object sender, EventArgs e)
        {
            FlowEntries.SizeChanged += FlowEntries_SizeChanged;
        }

        private void FlowEntries_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control ct in FlowEntries.Controls)
                ct.Width = (FlowEntries.Width - 30);
        }

        private async void GetIMG(SearchEntry Ent, string URL)
        {
            using (WebClient Web = new WebClient())
            {
                using (MemoryStream MemS = new MemoryStream(Web.DownloadData(URL)))
                {
                    await Task.Delay(1);
                    Ent.pbArtwork.Image = Image.FromStream(MemS);
                }
            }
        }

        private async void pbSearch_Click(object sender, EventArgs e)
        {
            var Tracks = await Main.Client.Spotify.SearchItemsAsync(txtSearch.Text, SpotifyAPI.Web.Enums.SearchType.Track, 5);
            SuspendLayout();
            foreach (var T in Tracks.Tracks.Items)
            {
                SearchEntry Ent = new SearchEntry();
                Ent.lblAlbum.Text = T.Album.Name;
                Ent.lblArtist.Text = T.Artists[0].Name;
                Ent.lblTrackName.Text = T.Name;
                GetIMG(Ent, T.Album.Images[0].Url);
                FlowEntries.Controls.Add(Ent);
            }
            ResumeLayout();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            foreach (Control Ent in FlowEntries.Controls)
                Ent.Dispose();
            FlowEntries.Controls.Clear();
            ResumeLayout();
        }

        public void SendStyle(EStyle Style)
        {
            if (Style == EStyle.Blue)
            {
                pbSearch.Image = Assets.Search_Blue;
            } else if (Style == EStyle.Color)
            {
                pbSearch.Image = Assets.Search_Color;
            }
            foreach(Control ct in FlowEntries.Controls)
            {
                if (typeof(IStyleableControl).IsAssignableFrom(ct.GetType())) ((IStyleableControl)ct).SendStyle(Style);
            }

        }
    }
}