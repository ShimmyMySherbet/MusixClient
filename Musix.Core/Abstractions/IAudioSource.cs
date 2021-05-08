using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IAudioSource
    {
        string Name { get; }

        Task<AudioDownloadResult> DownloadAudio(DownloadContext context, Stream stream);

        Task<bool> VerifyAvailable(DownloadContext context);
    }
}