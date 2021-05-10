using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class AudioProvider : IAudioProvider
    {
        private List<SourceListing> m_AudioSources = new List<SourceListing>();

        public void DeregisterSource(IAudioSource source)
        {
            lock (m_AudioSources)
            {
                m_AudioSources.RemoveAll(x => x.Source == source);
            }
        }

        public IAudioSource[] EnumerateSources()
        {
            lock (m_AudioSources)
            {
                return m_AudioSources.Select(x => x.Source).ToArray();
            }
        }

        public async Task<IAudioSource> GetAudioSource(DownloadContext context, ETranscoderPreferance preferance)
        {
            IReadOnlyCollection<IAudioSource> sources;
            lock (m_AudioSources)
            {
                sources = m_AudioSources.Where(x => x.Enabled).Select(x => x.Source).ToArray();
            }

            using (TaskList tasks = new TaskList())
            {
                foreach (var source in sources)
                {
                    tasks.Add(source.VerifyAvailable(context));
                }
                await tasks.WaitAll();

                var valids = tasks
                    .GetResults<AudioSourceAvailability>()
                    .Where(x => x.Available)
                    .OrderBy(x => x.Bitrate)
                    .ToArray();

                if (valids.Length > 0)
                {
                    return valids[0].Source;
                }
            }

            return null;
        }

        public void MarkSourceEnabled(IAudioSource source, bool enabled)
        {
            lock (m_AudioSources)
            {
                m_AudioSources.ForTrueEach(x => x.Source == source, x => x.Enabled = enabled);
            }
        }

        public void RegisterAudioSource(IAudioSource source)
        {
            lock (m_AudioSources)
            {
                if (!m_AudioSources.Any(x => x.Source == source))
                {
                    m_AudioSources.Add(new SourceListing() { Source = source });
                }
            }
        }

        private class SourceListing
        {
            public IAudioSource Source;
            public bool Enabled = true;
        }
    }
}