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
    public class SearchMenuItem : IMusixMenuItem
    {
        public SearchPage page = new SearchPage();
        public bool ShowWhenUnselected => true;

        public bool ShowWhenSelected => true;

        public string Name => "Search";

        public Image GetIcon(EStyle style)
        {
            if (style == EStyle.Blue)
            {
                return Assets.Search_Blue;
            } else
            {
                return Assets.Search_Color;
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
