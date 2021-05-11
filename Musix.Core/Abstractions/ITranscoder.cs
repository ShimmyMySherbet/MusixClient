using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface ITranscoder
    {
        bool RequiresPath { get; }
        bool CanTranscode(string sourceFormat, string targetFormat, DownloadContext context);

        Task<bool> Transcode(Stream source, Stream Destination, DownloadContext context);
    }
}
