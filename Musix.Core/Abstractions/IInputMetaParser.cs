using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IInputMetaParser
    {
        void DerriveMeta(string input, DownloadContext context);
    }
}