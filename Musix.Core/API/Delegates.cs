using Musix.Core.Models;

namespace Musix.Core.API
{
    public class Delegates
    {
        public delegate void DownloadProgressChangedCallback(int step, int stepMax, string status, MusixSongResult download);
    }
}