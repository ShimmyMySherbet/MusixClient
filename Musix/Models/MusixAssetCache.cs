using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Musix.Windows.API.Themes;

namespace Musix.Models
{
    public sealed class MusixAssetCache : IAssetCache<Image>
    {
        internal List<MusixAssetItem> assets = new List<MusixAssetItem>();

        public void DeregisterAsset(EStyle style, EAsset asset, bool Dispose)
        {
            lock (assets)
            {
                MusixAssetItem item = assets.Where(x => x.asset == asset && x.style == style && x.modifier == EAssetModifier.All).First();
                if (item != null)
                {
                    if (Dispose)
                    {
                        item.image.Dispose();
                    }
                    assets.Remove(item);
                }
            }
        }

        public void DeregisterAsset(EStyle style, EAsset asset, EAssetModifier modifier, bool Dispose)
        {
            lock (assets)
            {
                MusixAssetItem item = assets.Where(x => x.asset == asset && x.style == style && x.modifier == modifier).First();
                if (item != null)
                {
                    if (Dispose)
                    {
                        item.image.Dispose();
                    }
                    assets.Remove(item);
                }
            }
        }

        public bool DisposeCache()
        {
            lock (assets)
            {
                if (assets.Count == 0)
                {
                    return false;
                }
                else
                {
                    assets.ForEach(x => x.image.Dispose());
                    assets.Clear();
                    return true;
                }
            }
        }

        public Image GetAsset(EStyle style, EAsset asset)
        {
            lock (assets)
            {
                return assets.Where(x => x.style == style && asset == x.asset && x.modifier == EAssetModifier.All).FirstOrDefault()?.image;
            }
        }

        public Image GetAsset(EStyle style, EAsset asset, EAssetModifier modifier)
        {
            lock (assets)
            {
                return assets.Where(x => x.style == style && asset == x.asset && x.modifier == modifier).First()?.image;
            }
        }

        public bool HasAsset(EStyle style, EAsset asset)
        {
            return GetAsset(style, asset) != null;
        }

        public bool HasAsset(EStyle style, EAsset asset, EAssetModifier modifier)
        {
            return GetAsset(style, asset, modifier) != null;
        }

        public void RegisterAsset(EStyle style, EAsset asset, Image Image)
        {
            lock (assets)
            {
                if (!HasAsset(style, asset))
                {
                    assets.Add(new MusixAssetItem() { asset = asset, style = style, image = Image });
                }
            }
        }

        public void RegisterAsset(EStyle style, EAsset asset, EAssetModifier modifier, Image Image)
        {
            lock (assets)
            {
                if (!HasAsset(style, asset))
                {
                    assets.Add(new MusixAssetItem() { asset = asset, style = style, image = Image, modifier = modifier });
                }
            }
        }
    }
}