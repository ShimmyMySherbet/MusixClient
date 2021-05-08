using System.Collections.Generic;
using Musix.Core.Models;

namespace Musix.Core.Abstractions
{
    public interface IContextFactory
    {
        DownloadContext CreateContext(string rawInput);
        DownloadContext CreateContext(string rawInput, Dictionary<string, object> meta);
    }
}