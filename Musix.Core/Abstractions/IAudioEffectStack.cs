using System.IO;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IAudioEffectStack
    {
        int Count { get; }
        IAudioEffect[] Effects { get; }

        Task<bool> ApplyEffects(Stream inputStream, Stream outputStream, DownloadContext context);

        void AddEffect(IAudioEffect effect);

        void RemoveEffect(IAudioEffect effect);
    }
}