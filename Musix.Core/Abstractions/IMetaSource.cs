using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IMetaSource
    {
        string Name { get; }

        Task<MetadataResult> AddMetadata(DownloadContext context);
    }
}
