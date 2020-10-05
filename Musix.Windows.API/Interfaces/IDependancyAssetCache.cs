namespace Musix.Windows.API.Interfaces
{
    public interface IDependancyAssetCache<T, D, I>
    {
        void TryRegisterAsset(T Asset, I AssetID);

        void TryDeregisterAsset(I AssetID);

        T GetAsset(I AssetID);

        bool AssetExists(I AssetID);

        void RegisterDependant(I AssetID, D Dependant);

        void DeregisterDependant(I AssetID, D Dependant);

        bool IsDependant(I AssetID, D Dependant);
    }
}