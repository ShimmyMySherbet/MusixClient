namespace Musix.Windows.API.Interfaces
{
    public interface IConfiguratorFactory
    {
        IConfigurator<T> CreateConfigurator<T>(string ConfigName);
    }
}