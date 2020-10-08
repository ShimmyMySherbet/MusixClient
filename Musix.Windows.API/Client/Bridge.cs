using Musix.Windows.API.Interfaces;

namespace Musix.Windows.API.Client
{
    public static class Bridge
    {
        public static IConfiguratorFactory ConfiguratorFactory { get; set; }
        public static IUIManager UIManager { get; set; }
    }
}