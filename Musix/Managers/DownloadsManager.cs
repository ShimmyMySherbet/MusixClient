using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Musix.Core.Models;

namespace Musix.Managers
{
    public static class DownloadsManager
    {
        private static List<KeyValuePair<MusixSongResult, CancellationTokenSource>> Downloads = new List<KeyValuePair<MusixSongResult, CancellationTokenSource>>();

        public delegate void DownloadsChangedArgs();

        public static event DownloadsChangedArgs DownloadsChanged;

        public delegate void DownloadStartedArgs(MusixSongResult result);

        public delegate void DownloadFinishedArgs(MusixSongResult result);

        public static event DownloadStartedArgs DownloadStarted;

        public static event DownloadFinishedArgs DownloadFinished;

        public static int ActiveDownloads
        {
            get
            {
                lock (Downloads)
                {
                    return Downloads.Count;
                }
            }
        }

        public static void RegisterDownload(MusixSongResult Download, CancellationTokenSource cancellationToken = null)
        {
            lock (Downloads)
            {
                Downloads.Add(new KeyValuePair<MusixSongResult, CancellationTokenSource>(Download, cancellationToken));
                new Thread(x => { DownloadStarted?.Invoke(Download); DownloadsChanged?.Invoke(); }).Start();
            }
        }

        public static bool IsDownloading(MusixSongResult Download)
        {
            lock (Downloads)
            {
                return Downloads.Where(x => x.Key == Download).Count() != 0;
            }
        }

        public static void CancelDownload(MusixSongResult Download)
        {
            lock (Downloads)
            {
                Downloads.Where(x => x.Key == Download).FirstOrDefault().Value?.Cancel();
            }
        }

        public static void TryReleaseDownload(MusixSongResult Download)
        {
            lock (Downloads)
            {
                int st = Downloads.Count;
                Downloads.RemoveAll(x => x.Key == Download);
                if (Downloads.Count != st)
                {
                    new Thread(x => { DownloadFinished?.Invoke(Download); DownloadsChanged?.Invoke(); }).Start();
                }
            }
        }
    }
}