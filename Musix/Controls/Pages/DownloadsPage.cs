using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Core.Models;
using Musix.Managers;
using Musix.Models;

namespace Musix.Controls.Pages
{
    public partial class DownloadsPage : UserControl
    {
        public TaskFactory UITaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

        public DownloadsPage()
        {
            InitializeComponent();
            DownloadsManager.DownloadStarted += DownloadsManager_DownloadStarted;
            DownloadsManager.DownloadFinished += DownloadsManager_DownloadFinished;
            DownloadsManager.DownloadsChanged += DownloadsManager_DownloadsChanged;
            MainWindow.Instance.Client.ProgressChangedCallback = ClientProgressCallback;
            SizeChanged += DownloadsPage_SizeChanged;
        }

        private void DownloadsManager_DownloadsChanged()
        {
            UITaskFactory.StartNew(() =>
            {
                Console.WriteLine("downloads changed.");
                Console.WriteLine($"Active Downloads: {DownloadsManager.ActiveDownloads}");
                if (DownloadsManager.ActiveDownloads != 0)
                {
                    Console.WriteLine("HIDE!");
                    lblNoActiveDownloads.Hide();
                }
                else
                {
                    Console.WriteLine("SHOW!");
                    lblNoActiveDownloads.Show();
                }
            });
        }

        private void DownloadsPage_SizeChanged(object sender, EventArgs e)
        {
            lblNoActiveDownloads.CentreControlFull();
            foreach (Control ct in FlowDownloads.Controls)
                ct.Width = (FlowDownloads.Width - 13);
        }

        public void ClientProgressCallback(int step, int stepMax, string status, MusixSongResult download)
        {
            UITaskFactory.StartNew(() =>
            {
                ActiveDownloadControl ACD = GetControl(download);
                if (ACD != null)
                {
                    ACD.SetProgress(step, stepMax, status);
                }
            });
        }

        private void DownloadsManager_DownloadFinished(MusixSongResult result)
        {
            UITaskFactory.StartNew(() =>
            {
                ActiveDownloadControl adc = GetControl(result);
                if (adc != null)
                {
                    FlowDownloads.Controls.Remove(adc);
                    adc.Dispose();
                }
            });
        }

        private void DownloadsManager_DownloadStarted(Core.Models.MusixSongResult result)
        {
            UITaskFactory.StartNew(() =>
            {
                ActiveDownloadControl activeDownloadControl = new ActiveDownloadControl(result);
                FlowDownloads.Controls.Add(activeDownloadControl);
            });
        }

        private void DownloadsPage_Load(object sender, EventArgs e)
        {
            lblNoActiveDownloads.Visible = DownloadsManager.ActiveDownloads == 0;
        }

        public ActiveDownloadControl GetControl(MusixSongResult result)
        {
            foreach (ActiveDownloadControl adc in FlowDownloads.Controls.OfType<ActiveDownloadControl>())
            {
                if (adc.Result == result)
                {
                    return adc;
                }
            }
            return null;
        }
    }
}