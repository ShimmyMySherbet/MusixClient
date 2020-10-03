using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Controls.Pages;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix.Controls.MenuItems
{
    public class DownloadsMenuItem : IMusixMenuItem
    {
        public DownloadsPage page = new DownloadsPage();
        public bool ShowWhenUnselected => true;

        public bool ShowWhenSelected => true;

        public string Name => "Downloads";

        public Control CustomMenuItem => null;

        public Image GetIcon(EStyle style)
        {
            if (style == EStyle.Blue)
            {
                return Assets.Download1_Blue;
            } else
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
    }
}
