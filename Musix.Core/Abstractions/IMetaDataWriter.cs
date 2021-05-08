using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IMetaDataWriter
    {
        Task PrepareAssets(DownloadContext context);

        Task WriteMeta(Stream fileStream, DownloadContext context);
    }
}