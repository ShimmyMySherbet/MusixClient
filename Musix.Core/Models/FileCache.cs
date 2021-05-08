using System.IO;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class FileCache : ICachedAsset
    {
        public FileCacheProvider FileCacheProvider { get; private set; }
        internal FileCache(string name, string path, FileCacheProvider provider)
        {
            Name = name;
            Path = path;
            File.Create(path);
            FileCacheProvider = provider;
        }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public Stream Open()
        {
            return new FileStream(Path, FileMode.Open, FileAccess.ReadWrite);
        }

        public void Dispose()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
                FileCacheProvider.SendDisposed(this);
            }
              
        }
    }
}