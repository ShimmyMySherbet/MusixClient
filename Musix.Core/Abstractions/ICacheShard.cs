using System;

namespace Musix.Core.Abstractions
{
    public interface ICacheShard : IDisposable
    {
        string ShardID { get; }

        ICachedAsset CreateAsset(string name);

        bool HasAsset(string name);

        ICachedAsset GetAsset(string name);
    }
}