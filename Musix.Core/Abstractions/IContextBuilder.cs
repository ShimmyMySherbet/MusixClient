using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IContextBuilder
    {
        DownloadContext CreateContext();
    }
}