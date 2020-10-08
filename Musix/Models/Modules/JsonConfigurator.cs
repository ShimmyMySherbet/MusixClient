using Musix.Windows.API.Interfaces;

namespace Musix.Models.Modules
{
    public class JsonConfigurator<T> : IConfigurator<T>
    {
        private T _Instance;
        public T Instance => _Instance;
        public IConfigurationProvider Provider;
        public string Name;

        public JsonConfigurator(IConfigurationProvider provider, string name)
        {
            Name = name;
            Provider = provider;
        }

        public void Load()
        {
            _Instance = Provider.LoadEntity<T>(Name);
        }

        public void Save()
        {
            Provider.Save();
        }
    }
}