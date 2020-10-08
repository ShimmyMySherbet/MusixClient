namespace Musix.Windows.API.Interfaces
{
    public interface IMusixPluginEntryPoint
    {
        string Name { get; }
        void Load();

        void Unload();
    }
}