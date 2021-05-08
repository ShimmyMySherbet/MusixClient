using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IAudioProvider
    {
        Task<IAudioSource> GetAudioSource(DownloadContext context, ETranscoderPreferance preferance);

        void RegisterAudioSource(IAudioSource source);

        IAudioSource EnumerateSources();

        void DeregisterSource(IAudioSource source);

        void MarkSourceEnabled(IAudioSource source, bool enabled);

    }
}