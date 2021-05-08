using System;
using Musix.Core.Abstractions;

namespace Musix.HTTPHostedFiles
{
    public class HTTPFileHoster : IFileHoster
    {
        public void DeregisterAsset(ICachedAsset asset)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string RegisterFile(ICachedAsset asset)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}