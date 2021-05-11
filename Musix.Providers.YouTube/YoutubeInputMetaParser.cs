using Musix.Core.Abstractions;
using Musix.Core.Models;

namespace Musix.Providers.YouTube
{
    public class YoutubeInputMetaParser : IInputMetaParser
    {
        public bool DerriveMeta(string input, DownloadContext context)
        {
            var ID = YoutubeHeleprs.GetVideoID(input.Trim(' '));
            if (ID != null)
            {
                context.MetaIndex.Set("YT-ID", ID);
                return true;
            }
            return false;
        }
    }
}