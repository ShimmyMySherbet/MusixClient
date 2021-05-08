using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IMetaDataWriter
    {
        Task WriteMetadata(DownloadContext context, Stream stream);
    }
}
