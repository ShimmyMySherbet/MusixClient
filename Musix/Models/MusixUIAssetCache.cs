using System.Collections.Generic;
using System.Drawing;
using Musix.Windows.API.Interfaces;

namespace Musix.Models
{
    public class MusixUIAssetCache : IDependancyAssetCache<Image, object, string>
    {
        private Dictionary<string, Image> Assets = new Dictionary<string, Image>();
        private Dictionary<string, List<object>> Dependants = new Dictionary<string, List<object>>();

        public void DeregisterDependant(string AssetID, object Dependant)
        {
            lock (Dependants)
            {
                if (Dependants.ContainsKey(AssetID))
                {
                    if (Dependants[AssetID].Contains(Dependant))
                    {
                        Dependants[AssetID].Remove(Dependant);

                        if (Dependants[AssetID].Count == 0)
                        {
                            lock (Assets)
                            {
                                DeregisterAsset(AssetID);
                            }
                        }
                    }
                }
            }
        }

        public Image GetAsset(string AssetID)
        {
            lock (Assets)
            {
                if (Assets.ContainsKey(AssetID))
                {
                    return Assets[AssetID];
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsDependant(string AssetID, object Dependant)
        {
            lock (Dependants)
            {
                if (!Dependants.ContainsKey(AssetID)) return false;
                return Dependants[AssetID].Contains(Dependant);
            }
        }

        public void RegisterDependant(string AssetID, object Dependant)
        {
            lock (Dependants)
            {
                if (!Dependants.ContainsKey(AssetID))
                {
                    Dependants.Add(AssetID, new List<object>() { Dependant });
                }
                else
                {
                    if (!Dependants[AssetID].Contains(Dependant))
                    {
                        Dependants[AssetID].Add(Dependant);
                    }
                }
            }
        }

        public void TryDeregisterAsset(string AssetID)
        {
            lock (Assets)
            {
                DeregisterAsset(AssetID);
            }
        }

        private void DeregisterAsset(string AssetID)
        {
            System.Console.WriteLine($"Deregistering asset: {AssetID}");
            if (Assets.ContainsKey(AssetID))
            {
                Assets[AssetID].Dispose();
                Assets.Remove(AssetID);
            }
            if (Dependants.ContainsKey(AssetID))
            {
                Dependants.Remove(AssetID);
            }
        }

        public void TryRegisterAsset(Image Asset, string AssetID)
        {
            lock (Assets)
            {
                if (!Assets.ContainsKey(AssetID))
                {
                    Assets.Add(AssetID, Asset);
                }
            }
        }

        public bool AssetExists(string AssetID)
        {
            lock(Assets)
            {
                return Assets.ContainsKey(AssetID);
            }
        }
    }
}