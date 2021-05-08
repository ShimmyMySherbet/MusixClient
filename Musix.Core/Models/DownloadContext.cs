using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class DownloadContext
    {
        public IMetaIndex MetaIndex;
        public string InitialMeta;
        public ETranscoderPreferance TranscoderPreferance = ETranscoderPreferance.None;
        public ICacheShard CacheShard;
    }
}
