using Musix.Models;
using Musix.Models.Modules;
using Musix.Windows.API.Client;
using Musix.Windows.API.Interfaces;

namespace Musix.Managers
{
    public static class ConfigManager
    {
        public static IConfigurationProvider Provider;
        public static IConfiguratorFactory ConfiguratorFactory;
        public static IConfigurator<MusixConfig> Configurator;
        public static MusixConfig Config;

        public static void Init()
        {
            Provider = new JsonConfigurationProvider() { FileName = "Config.json" };
            Provider.Load();
            ConfiguratorFactory = new JsonConfiguratorFactory(Provider);
            Bridge.ConfiguratorFactory = new JsonConfiguratorFactory(Provider);
            Configurator = ConfiguratorFactory.CreateConfigurator<MusixConfig>("MusixClient");
            Configurator.Load();
            Config = Configurator.Instance;
        }
    }
}