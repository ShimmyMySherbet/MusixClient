using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Managers
{
    public static class DownloadsManager
    {
        private static List<MusixSongResult> Downloads = new List<MusixSongResult>();
        public delegate void DownloadsChangedArgs();
        public static event DownloadsChangedArgs DownloadsChanged;

        public static int ActiveDownloads
        {
            get
            {
                lock(Downloads)
                {
                    return Downloads.Count;
                }
            }
        }
        public static void RegisterDownload(MusixSongResult Download)
        {
            lock(Downloads)
            {
                Downloads.Add(Download);
                new Thread(x => DownloadsChanged?.Invoke()).Start();
            }
        }

        public static void TryReleaseDownload(MusixSongResult Download)
        {
            lock(Downloads)
            {
                if (Downloads.Contains(Download))
                {
                    Downloads.Remove(Download);
                    new Thread(x => DownloadsChanged?.Invoke()).Start();
                }
            }
        }

    }
}
