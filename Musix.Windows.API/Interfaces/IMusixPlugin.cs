namespace Musix.Windows.API.Interfaces
{
    public interface IMusixPlugin
    {
        string Name { get; }
        void Load();

        void Unload();
    }
}