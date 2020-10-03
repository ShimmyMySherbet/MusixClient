using System.Drawing;
using System.Windows.Forms;
using Musix.Controls.Pages;
using Musix.Managers;
using Musix.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix.Controls.MenuItems
{
    public class DownloadsMenuItem : IMusixMenuItem, IStatusIconProvider
    {
        public DownloadsPage page = new DownloadsPage();
        public bool ShowWhenUnselected => true;

        public bool ShowWhenSelected => true;

        public string Name => "Downloads";

        public event Delegates.UpdateIconArgs UpdateIcon;

        public DownloadCountIconRenderer renderer;

        public bool DisposeOnUpdate => true;

        private void DownloadsManager_DownloadsChanged()
        {
            if (DownloadsManager.ActiveDownloads == 0)
            {
                UpdateIcon?.Invoke(null);
            }
            else
            {
                UpdateIcon?.Invoke(renderer.Render(DownloadsManager.ActiveDownloads));
            }
        }

        public void Init()
        {
            renderer = new DownloadCountIconRenderer();
            DownloadsManager.DownloadsChanged += DownloadsManager_DownloadsChanged;
        }

        public Image GetIcon(EStyle style)
        {
            if (style == EStyle.Blue)
            {
                return Assets.Download1_Blue;
            }
            else
            {
                return Assets.Download1_Color;
            }
        }

        public Control GetMenuControl() => page;

        public void OnDeselect()
        {
        }

        public void OnSelect()
        {
        }

        public void StyleChanged(EStyle style)
        {
        }
    }
}