using System;

namespace Musix.Core.Abstractions
{
    public interface ICacheProvider : IDisposable
    {
        bool RequiresPathHosting { get; }

        ICachedAsset CreateAsset(string name, bool providePath);

        ICachedAsset CreateAudioCache(string name, bool providePath);
    }
}