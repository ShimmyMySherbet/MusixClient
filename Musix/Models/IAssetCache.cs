using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Windows.API.Themes;

namespace Musix.Models
{
    public interface IAssetCache<T>
    {
        void RegisterAsset(EStyle style, EAsset asset, T AssetItem);
        void RegisterAsset(EStyle style, EAsset asset, EAssetModifier modifier, T AssetItem);
        bool HasAsset(EStyle style, EAsset asset);
        bool HasAsset(EStyle style, EAsset asset, EAssetModifier modifier);
        T GetAsset(EStyle style, EAsset asset);
        T GetAsset(EStyle style, EAsset asset, EAssetModifier modifier);
        bool DisposeCache();
        void DeregisterAsset(EStyle style, EAsset asset, bool Dispose);
        void DeregisterAsset(EStyle style, EAsset asset, EAssetModifier modifier, bool Dispose);
    }
}
