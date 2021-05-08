namespace Musix.Core.Abstractions
{
    public interface IMetaIndex
    {
        bool ContainsKey(string key);

        string GetString(string key);

        void Add<T>(string key, T Instance);

        T Get<T>(string key);

        void Delete(string key);
    }
}