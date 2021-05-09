using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IAudioEffect
    {
        string Name { get; }

        Task<bool> ApplyEffect(Stream input, Stream output, DownloadContext context);
    }
}