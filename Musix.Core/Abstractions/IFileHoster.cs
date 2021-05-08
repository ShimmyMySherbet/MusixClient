using System;

namespace Musix.Core.Abstractions
{
    public interface IFileHoster : IDisposable
    {
        void Start();

        void Stop();

        /// <returns>The path to the hosted asset</returns>
        string RegisterFile(ICachedAsset asset);

        void DeregisterAsset(ICachedAsset asset);
    }
}