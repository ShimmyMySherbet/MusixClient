using System.Collections.Generic;
using System.Linq;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class TranscoderIndex : ITranscoderIndex
    {
        private List<ITranscoder> m_Transcoders = new List<ITranscoder>();

        public bool CanTranscode(string sourceFormat, string targetFormat, ETranscoderPreferance preferance, DownloadContext context)
        {
            return FindTranscoder(sourceFormat, targetFormat, preferance, context) != null;
        }

        private bool Matches(ITranscoder transcoder, ETranscoderPreferance pref)
        {
            if (pref == ETranscoderPreferance.PathedOnly)
            {
                return transcoder.RequiresPath;
            }
            else if (pref == ETranscoderPreferance.NoPathedOnly)
            {
                return !transcoder.RequiresPath;
            }
            return true;
        }

        public void DeregisterTranscoder(ITranscoder transcoder)
        {
            lock (m_Transcoders)
            {
                if (m_Transcoders.Contains(transcoder))
                {
                    m_Transcoders.Remove(transcoder);
                }
            }
        }

        public ITranscoder FindTranscoder(string sourceFormat, string targetFormat, ETranscoderPreferance preferance, DownloadContext context)
        {
            lock (m_Transcoders)
            {
                foreach (var tc in m_Transcoders.Where(x => Matches(x, preferance)))
                {
                    if (tc.CanTranscode(sourceFormat, targetFormat, context))
                    {
                        if (preferance == ETranscoderPreferance.PreferNoPathed && !tc.RequiresPath)
                        {
                            return tc;
                        }
                        else if (preferance == ETranscoderPreferance.PreferPathed && tc.RequiresPath)
                        {
                            return tc;
                        }
                        else
                        {
                            return tc;
                        }
                    }
                }
            }
            return null;
        }

        public void RegisterTranscoder(ITranscoder transcoder)
        {
            lock (m_Transcoders)
            {
                m_Transcoders.Add(transcoder);
            }
        }
    }
}