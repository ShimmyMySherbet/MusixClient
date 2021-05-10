using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class DefaultContextFactory : IContextFactory
    {
        public DownloadContext CreateContext(string rawInput)
        {
            return new DownloadContext() { InitialMeta = rawInput };
        }

        public DownloadContext CreateContext(string rawInput, Dictionary<string, object> meta)
        {
            var context = new DownloadContext() { InitialMeta = rawInput, MetaIndex = new MetaIndex() };
            foreach(var m in meta)
            {
                context.MetaIndex.Add(m.Key, m.Value);
            }
            return context;
        }
    }
}
