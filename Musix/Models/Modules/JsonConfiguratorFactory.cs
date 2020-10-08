using Musix.Windows.API.Interfaces;

namespace Musix.Models.Modules
{
    public class JsonConfiguratorFactory : IConfiguratorFactory
    {
        private IConfigurationProvider Provider;

        public JsonConfiguratorFactory(IConfigurationProvider provider)
        {
            Provider = provider;
        }

        public IConfigurator<T> CreateConfigurator<T>(string ConfigName)
        {
            return new JsonConfigurator<T>(Provider, ConfigName);
        }
    }
}