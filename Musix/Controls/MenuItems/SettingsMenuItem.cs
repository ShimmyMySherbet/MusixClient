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
    public class SettingsMenuItem : IMusixMenuItem
    {
        public SettingsPage page = new SettingsPage();
        public bool ShowWhenUnselected => true;

        public bool ShowWhenSelected => true;

        public string Name => "Settings";

        public Control CustomMenuItem => null;

        public Image GetIcon(EStyle style)
        {
            if (style == EStyle.Blue)
            {
                return Assets.Settings_Blue;
            }
            else
            {
                return Assets.Settings_Color;
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
