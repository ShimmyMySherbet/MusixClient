using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Controls.Popups;
using Musix.Core.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Themes;

namespace Musix.Controls.Pages
{
    public partial class SearchPage : UserControl, IStyleableControl, IAcceptListener
    {
        public MainWindow Main;
        public TaskFactory UITaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        public bool CanDragDrop = true;

        public SearchPage()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Main = MainWindow.Instance;

            FlowEntries.AllowDrop = true;
            FlowEntries.DragEnter += FlowEntries_DragEnter;
            FlowEntries.DragOver += FlowEntries_DragOver;
            FlowEntries.DragDrop += FlowEntries_DragDrop;
            FlowEntries.DragLeave += FlowEntries_DragLeave;
        }

        private void FlowEntries_DragLeave(object sender, EventArgs e)
        {
            CanDragDrop = true;
        }

        private void FlowEntries_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("DROP!");
        }

        private void FlowEntries_DragOver(object sender, DragEventArgs e)
        {
            if (CanDragDrop)
            {
                e.Effect = e.AllowedEffect;
                MainWindow.Instance.ShowPopup(new DragDropPopup(DragDropCallback));
            }
        }

        private void FlowEntries_DragEnter(object sender, DragEventArgs e)
        {
            if (CanDragDrop)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void SearchPage_Load(object sender, EventArgs e)
        {
            FlowEntries.SizeChanged += FlowEntries_SizeChanged;
        }

        public void DragDropCallback(string URL, bool Cancelled)
        {
            CanDragDrop = true;
            if (!Cancelled && !string.IsNullOrEmpty(URL))
            {
                txtSearch.Text = URL;
                TriggerSearch();
            }
        }

        private void FlowEntries_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control ct in FlowEntries.Controls)
                ct.Width = (FlowEntries.Width - 13);
        }

        public void OnPageAccept()
        {
            TriggerSearch();
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

        private void pbSearch_Click(object sender, EventArgs e)
        {
            TriggerSearch();
        }

        public void TriggerSearch()
        {
            UITaskFactory.StartNew(() =>
            {
                if (txtSearch.Enabled)
                {
                    txtSearch.Enabled = false;
                    new Thread(() => RunSearch(txtSearch.Text, true)).Start();
                }
            });
        }

        private async void RunSearch(string query, bool OpenSearchOnConplete = false)
        {
            MusixSongResult result = null;
            string YT = "";
            string SP = "";
            bool Passed = false;
            if (query.ToLower().Contains("spotify"))
            {
                SP = query;
                Console.WriteLine("Collect via spotify");
                result = Main.Client.Collect(Main.Client.GetTrackByURL(query));
            }
            else if (query.Contains("youtu"))
            {
                YT = query;
                Console.WriteLine("Collect via Youtube");
                result = await Main.Client.CollectAsync(query);
            }
            else
            {
                Console.WriteLine("Collect via Query");

                result = Main.Client.CollectByName(query);
            }

            if (result != null && result.HasTrack && result.HasVideo)
            {
                Passed = true;
                Console.WriteLine("Creating UI entry");
                AddMusixEntry(result);
            }
            else
            {
                Console.WriteLine("FAILED!");
                if (YT.Length != 0 || SP.Length != 0)
                {
                  await  UITaskFactory.StartNew(() => MainWindow.Instance.ShowPopup(new ManualResolvePopup(YT, SP, onMaualPromptCancelled, onManualPrompt)));

                    //MainWindow.Instance.ShowPopup(new ManualResolvePopup(YT, SP, onMaualPromptCancelled, onManualPrompt));
                }
                else
                {
                    await UITaskFactory.StartNew(() => lblCFail.Visible = true);
                    new Thread(() =>
                    {
                        Thread.Sleep(1000);
                        UITaskFactory.StartNew(() => lblCFail.Visible = false);
                    }).Start();
                }
            }
            if (OpenSearchOnConplete)
            {
                await UITaskFactory.StartNew(() =>
                {
                    if (Passed)
                    {
                        txtSearch.Text = "";
                    }
                    txtSearch.Enabled = true;
                });
            }
            Console.WriteLine("Finished.");
        }

        public void AddMusixEntry(MusixSongResult result)
        {
            UITaskFactory.StartNew(() => FlowEntries.Controls.Add(new SearchEntry(result)));
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
            }
            else if (Style == EStyle.Color)
            {
                pbSearch.Image = Assets.Search_Color;
            }
            foreach (Control ct in FlowEntries.Controls)
            {
                if (typeof(IStyleableControl).IsAssignableFrom(ct.GetType())) ((IStyleableControl)ct).SendStyle(Style);
            }
        }

        private void btnTestPopup_Click(object sender, EventArgs e)
        {
            ManualResolvePopup popup = new ManualResolvePopup("", "", onMaualPromptCancelled, onManualPrompt);
            MainWindow.Instance.ShowPopup(popup);
        }

        private void onMaualPromptCancelled()
        {
            Console.WriteLine("popup cancelled");
        }

        private async void onManualPrompt(string SpotifyTrackID, string YoutubeTrackID)
        {
            MusixSongResult result = new MusixSongResult() { SpotifyTrack = MainWindow.Instance.Client.GetTrackByID(SpotifyTrackID), YoutubeVideo = await MainWindow.Instance.Client.YouTube.Videos.GetAsync(new YoutubeExplode.Videos.VideoId(YoutubeTrackID)) };
            AddMusixEntry(result);
        }
    }
}