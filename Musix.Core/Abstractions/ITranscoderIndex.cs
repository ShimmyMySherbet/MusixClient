using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface ITranscoderIndex
    {
        ITranscoder FindTranscoder(string inputFormat, string outputFormat, ETranscoderPreferance preferance);

        bool CanTranscode(string sourceFormat, string targetFormat, ETranscoderPreferance preferance, DownloadContext context);

        void RegisterTranscoder(ITranscoder transcoder);

        void DeregisterTranscoder(ITranscoder transcoder);
    }
}