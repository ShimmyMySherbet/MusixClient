using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Managers;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix.Models
{
    public class DownloadStatusIconProvider : IStatusIconProvider
    {
        public event Delegates.UpdateIconArgs UpdateIcon;
        public DownloadCountIconRenderer renderer;

        public bool DisposeOnUpdate => true;

        public void Init()
        {
            renderer = new DownloadCountIconRenderer();
            DownloadsManager.DownloadsChanged += DownloadsManager_DownloadsChanged;
            UpdateIcon?.Invoke(renderer.Render(DownloadsManager.ActiveDownloads));

        }

        private void DownloadsManager_DownloadsChanged()
        {
            if (DownloadsManager.ActiveDownloads == 0)
            {
                UpdateIcon?.Invoke(null);
            } else
            {
                UpdateIcon?.Invoke(renderer.Render(DownloadsManager.ActiveDownloads));
            }
        }

        public void StyleChanged(EStyle style)
        {
        }
    }
}
