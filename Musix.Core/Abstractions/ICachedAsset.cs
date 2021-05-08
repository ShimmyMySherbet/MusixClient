using System;
using System.IO;

namespace Musix.Core.Abstractions
{
    public interface ICachedAsset : IDisposable
    {
        string Name { get; }
        string Path { get; }

        Stream Open();
    }
}