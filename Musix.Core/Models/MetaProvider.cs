using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class MetaProvider : IMetaProvider
    {
        private List<MetaInstance> m_MetaSources = new List<MetaInstance>();

        public void DeregisterSource(IMetaSource provider)
        {
            lock (m_MetaSources)
            {
                m_MetaSources.RemoveAll(x => x.Source == provider);
            }
        }

        public IMetaSource[] EnumerateSources()
        {
            lock (m_MetaSources)
            {
                return m_MetaSources.Select(x => x.Source).ToArray();
            }
        }

        public void MarkSourceEnabled(IMetaSource provider, bool enabled)
        {
            lock (m_MetaSources)
            {
                m_MetaSources.ForTrueEach(x => x.Source == provider, x => x.Enabled = enabled);
            }
        }

        public void RegisterMetadataSource(IMetaSource provider)
        {
            lock (m_MetaSources)
            {
                if (!m_MetaSources.Any(x => x.Source == provider))
                {
                    m_MetaSources.Add(new MetaInstance() { Source = provider });
                }
            }
        }

        public async Task<bool> SourceMetadata(DownloadContext context)
        {
            IMetaSource[] sources;
            lock (m_MetaSources)
            {
                sources = m_MetaSources.Where(x => x.Enabled).Select(x => x.Source).ToArray();
            }

            using (TaskList tasks = new TaskList())
            {
                for (int i = 0; i < sources.Length; i++)
                {
                    var source = sources[i];
                    tasks.Add(source.AddMetadata(context));
                }
                await tasks.WaitAll();

                return tasks.GetResults<MetadataResult>().Any(x => x.Success);
            }
        }

        private class MetaInstance
        {
            public IMetaSource Source;
            public bool Enabled = true;
        }
    }
}