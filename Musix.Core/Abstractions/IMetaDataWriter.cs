using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IMetaDataWriter
    {
        Task WriteMeta(Stream stream, DownloadContext context);
    }
}