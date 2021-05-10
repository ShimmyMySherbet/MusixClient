using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IMetaProvider
    {
        Task<bool> SourceMetadata(DownloadContext context);

        void RegisterMetadataSource(IMetaSource provider);

        IMetaSource[] EnumerateSources();

        void DeregisterSource(IMetaSource provider);

        void MarkSourceEnabled(IMetaSource provider, bool enabled);

    }
}