using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualMetadata
{
    public class ManualMetadataPlugin : IMusixPlugin, IMusixMenuItem
    {
        public ManualDownloadPage Page = new ManualDownloadPage();

        public string Name => "Manual";

        public bool ShowWhenUnselected => true;

        public bool ShowWhenSelected => true;

        public Image GetIcon(EStyle style)
        {
            return Assets.Browse_Blue;
        }

        public Control GetMenuControl() => Page;

        public void Load()
        {
        }

        public void OnDeselect()
        {
        }

        public void OnSelect()
        {
        }

        public void Unload()
        {
            Page?.Dispose();
        }
    }
}
