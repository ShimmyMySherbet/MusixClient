using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class TranscoderIndex : ITranscoderIndex
    {
        public bool CanTranscode(string sourceFormat, string targetFormat, ETranscoderPreferance preferance, DownloadContext context)
        {
            throw new NotImplementedException();
        }

        public void DeregisterTranscoder(ITranscoder transcoder)
        {
            throw new NotImplementedException();
        }

        public ITranscoder FindTranscoder(string inputFormat, string outputFormat, ETranscoderPreferance preferance)
        {
            throw new NotImplementedException();
        }

        public void RegisterTranscoder(ITranscoder transcoder)
        {
            throw new NotImplementedException();
        }
    }
}
