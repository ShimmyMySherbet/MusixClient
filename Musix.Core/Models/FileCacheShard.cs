using System;
using System.Collections.Generic;
using System.Linq;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class FileCacheShard : ICacheShard
    {
        public FileCacheProvider FileCacheProvider;

        public string ShardID { get; private set; }

        private List<ICachedAsset> m_CachedAssets = new List<ICachedAsset>();

        internal FileCacheShard(FileCacheProvider provider, string ID)
        {
            FileCacheProvider = provider;
            ShardID = ID;
        }

        public ICachedAsset CreateAsset(string name)
        {
            var ast = FileCacheProvider.CreateAsset($"{ShardID}_{name.ToLower()}", false);
            lock (m_CachedAssets)
            {
                m_CachedAssets.Add(ast);
            }

            return ast;
        }

        public void Dispose()
        {
            lock(m_CachedAssets)
            {
                foreach(var ast in m_CachedAssets)
                {
                    ast.Dispose();
                }
                m_CachedAssets.Clear();
            }
        }

        public ICachedAsset GetAsset(string name)
        {
            lock (m_CachedAssets)
            {
                foreach (var ast in m_CachedAssets.Where(x => x.Name == $"{ShardID}_{name.ToLower()}"))
                {
                    return ast;
                }
            }
            return null;
        }

        public bool HasAsset(string name)
        {
            return GetAsset(name) != null;
        }
    }
}