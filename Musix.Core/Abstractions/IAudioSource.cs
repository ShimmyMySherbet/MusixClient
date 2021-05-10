using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IAudioSource
    {
        string Name { get; }

        string OutputFormat { get; }

        Task<AudioDownloadResult> DownloadAudio(DownloadContext context, Stream stream);

        Task<AudioSourceAvailability> VerifyAvailable(DownloadContext context);
    }
}