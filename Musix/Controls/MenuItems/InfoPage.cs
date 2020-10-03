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
    public class InfoPage : IMusixMenuItem
    {
        public CreditsPage page = new CreditsPage();
        public bool ShowWhenUnselected => false;

        public bool ShowWhenSelected => true;

        public string Name => "Credits";

        public Image GetIcon(EStyle style)
        {
            if (style == EStyle.Blue)
            {
                return Assets.Musix_Main_BDROP1;
            } else
            {
                return Assets.Musix_Main_R1;
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
