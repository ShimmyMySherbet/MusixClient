using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IInputMetaParser
    {
        bool DerriveMeta(string input, DownloadContext context);
    }
}