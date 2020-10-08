namespace Musix.Windows.API.Interfaces
{
    public interface IConfigurator<T>
    {
        T Instance { get; }

        void Load();

        void Save();
    }
}