using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Musix.Controls
{
    public delegate void Callback();

    public partial class FFMPEGDownloader : Form
    {
        public const string DownloadURL = "https://github.com/BtbN/FFmpeg-Builds/releases/download/autobuild-2021-08-31-14-55/ffmpeg-n4.4-80-gbf87bdd3f6-win64-lgpl-4.4.zip";
        public static readonly string[] DownloadFiles = { "ffmpeg.exe", "ffprobe.exe" };
        private long m_DownloadSize { get; set; }
        private long m_Downloaded { get; set; }
        private long m_DownloadedLastSecond { get; set; }
        private byte m_Stage = 0;

        public TaskFactory TaskFactory;

        public FFMPEGDownloader()
        {
            InitializeComponent();
            TaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

            Shown += onWindowOpened;
        }

        private void onWindowOpened(object sender, EventArgs e)
        {
            StartDownload();
        }

        public void StartDownload()
        {
            ThreadPool.QueueUserWorkItem(async (_) => await Monitor());
            ThreadPool.QueueUserWorkItem(async (_) => await Download());
        }

        private async Task Monitor()
        {
            string downloadStr = null;

            while (m_Stage < 3)
            {
                Debug.WriteLine($"Stage: {m_Stage}");
                if (m_Stage == 0)
                {
                    _ = TaskFactory.StartNew(() =>
                    {
                        lblProgress.Text = $"Starting Download...";
                    });
                    await Task.Delay(100);
                }
                else if (m_Stage == 1)
                {
                    if (downloadStr == null)
                    {
                        if (m_DownloadSize > 1000 * 1000)
                        {
                            downloadStr = $"{Math.Round((double)m_DownloadSize / (1000 * 1000), 2)}mb";
                        }
                        else if (m_DownloadSize > 1000)
                        {
                            downloadStr = $"{Math.Round((double)m_DownloadSize / (1000), 2)}kb";
                        }
                        else
                        {
                            downloadStr = $"{m_DownloadSize}b";
                        }
                    }

                    var percentage = Math.Round(((double)m_Downloaded / m_DownloadSize) * 100, 2);
                    var percVal = (int)percentage;

                    double bytesLastSec = m_DownloadedLastSecond * 5;
                    m_DownloadedLastSecond = 0;

                    var lastSec = "";
                    if (bytesLastSec > 1000 * 1000)
                    {
                        lastSec = $"{Math.Round(bytesLastSec / (1000 * 1000), 2)}mbp/s";
                    }
                    else if (bytesLastSec > 1000)
                    {
                        lastSec = $"{Math.Round(bytesLastSec / (1000), 2)}kbp/s";
                    }
                    else
                    {
                        lastSec = $"{bytesLastSec}bp/s";
                    }

                    string downloadedStr;
                    if (m_DownloadSize > 1000 * 1000)
                    {
                        downloadedStr = $"{Math.Round((double)m_Downloaded / (1000 * 1000), 2)}mb";
                    }
                    else if (m_DownloadSize > 1000)
                    {
                        downloadedStr = $"{Math.Round((double)m_Downloaded / (1000), 2)}kb";
                    }
                    else
                    {
                        downloadedStr = $"{m_Downloaded}b";
                    }

                    _ = TaskFactory.StartNew(() =>
                    {
                        lblProgress.Text = $"Downloading {percentage}% completed. ({downloadedStr}/{downloadStr}) {lastSec}";
                        progProgress.Value = percVal;
                    });

                    await Task.Delay(200);
                }
                else if (m_Stage == 2)
                {
                    await TaskFactory.StartNew(() =>
                    {
                        lblProgress.Text = $"Decompressing...";
                        if (progProgress.Value != progProgress.Maximum)
                        {
                            progProgress.Value = progProgress.Maximum;
                        }
                    });
                    await Task.Delay(100);
                }
            }

            Debug.WriteLine("Send restart...");
            await TaskFactory.StartNew(() =>
            {
                lblProgress.Text = $"Restarting App...";
            });
            await Task.Delay(1500);
            Application.Restart();
        }

        private async Task Download()
        {
            var request = WebRequest.CreateHttp(DownloadURL);
            request.Method = "GET";
            using (var response = await request.GetResponseAsync())
            using (var network = response.GetResponseStream())
            {
                m_DownloadSize = response.ContentLength;

                ushort bufferSize = 1000;
                byte[] buffer = new byte[bufferSize];
                m_Stage = 1;
                using (var memory = new MemoryStream())
                {
                    while (memory.Length < m_DownloadSize)
                    {
                        var read = await network.ReadAsync(buffer, 0, bufferSize);
                        await memory.WriteAsync(buffer, 0, read);

                        m_Downloaded += read;
                        m_DownloadedLastSecond += read;
                    }
                    memory.Position = 0;

                    m_Stage = 2;

                    using (var zip = new ZipFile(memory))
                    {
                        foreach (var f in DownloadFiles)
                        {
                            ZipEntry ent = null;
                            for (int i = 0; i < zip.Count; i++)
                            {
                                if (zip[i].Name.EndsWith(f, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    ent = zip[i];
                                    break;
                                }
                            }

                            if (ent != null)
                            {
                                using (var ffmpeg = zip.GetInputStream(ent))
                                using (var disk = new FileStream(f, FileMode.Create, FileAccess.Write))
                                {
                                    await ffmpeg.CopyToAsync(disk);
                                    await disk.FlushAsync();
                                }
                            }
                        }
                    }
                    m_Stage = 3;
                }
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}