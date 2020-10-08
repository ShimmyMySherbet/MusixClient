namespace Musix.Windows.API.Interfaces
{
    public interface IConfigurationProvider
    {
        void Load();

        void Save();

        T LoadEntity<T>(string EntityName);

        void WriteEntity<T>(T Entity, string EntityName);

        void DeleteEntity(string EntityName);

        string[] GetEntities();
    }
}