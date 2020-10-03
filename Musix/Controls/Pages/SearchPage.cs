using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Core.Models;
using Musix.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Themes;

namespace Musix.Controls.Pages
{
    public partial class SearchPage : UserControl, IStyleableControl
    {
        public MainWindow Main;

        public SearchPage()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Main = MainWindow.Instance;
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
            MusixSongResult Result = Main.Client.CollectByString(txtSearch.Text);
            if (Result.HasTrack && Result.HasVideo)
            {



            }
            else MessageBox.Show("Failed.");
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