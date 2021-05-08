using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class FileCacheProvider : ICacheProvider
    {
        public string AssetCachePath { get; set; }
        public string AudioCachePath { get; set; }

        /// <summary>
        /// False if a local HTTP server is required to provide a path.
        /// </summary>
        public bool RequiresPathHosting => false;

        public List<FileCache> CachedFiles = new List<FileCache>();

        public ICachedAsset CreateAsset(string name, bool providePath)
        {
            string path = Path.Combine(AssetCachePath, $"{DateTime.Now.Ticks}_{name}");
            var f = new FileCache(name, path, this);
            lock (CachedFiles)
            {
                CachedFiles.Add(f);
            }
            return f;
        }

        public ICachedAsset CreateAudioCache(string name, bool providePath)
        {
            string path = Path.Combine(AudioCachePath, $"{DateTime.Now.Ticks}_{name}");
            var f = new FileCache(name, path, this);
            lock (CachedFiles)
            {
                CachedFiles.Add(f);
            }
            return f;
        }

        internal void SendDisposed(FileCache instance)
        {
            lock (CachedFiles)
            {
                if (CachedFiles.Contains(instance))
                {
                    CachedFiles.Remove(instance);
                }
            }
        }

        public void Dispose()
        {
            List<FileCache> cacheCopy;
            lock (CachedFiles)
            {
                cacheCopy = CachedFiles.ToList();
            }
            foreach (var cacheInstance in cacheCopy)
            {
                cacheInstance?.Dispose();
            }
        }

        public ICacheShard CreateShard()
        {
            return new FileCacheShard(this, $"Asset_{DateTime.Now.Ticks}");
        }
    }
}