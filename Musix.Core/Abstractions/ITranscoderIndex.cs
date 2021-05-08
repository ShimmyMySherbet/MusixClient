using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface ITranscoderIndex
    {
        Task<bool> Transcode(string sourceFormat, string targetFormat, Stream input, Stream output, DownloadContext context);

        bool CanTranscode(string sourceFormat, string targetFormat, DownloadContext context);

        void RegisterTranscoder(ITranscoder transcoder);

        void DeregisterTranscoder(ITranscoder transcoder);
    }
}