using System.Drawing;
using System.Windows.Forms;
using Musix.Controls.MenuItems;
using Musix.Windows.API.Interfaces;
using NAudio.SoundFont;

namespace Musix.Models
{
    public static class extensions
    {
        public static void CentreControl(this Control control)
        {
            int cWidth = control.Width;
            int offset = control.Parent.Width - cWidth;
            Point rPos = control.Location;
            rPos.X = (int)(offset / 2.2);
            control.Location = rPos;
        }

        public static void ClosePopup(this IPopupItem popup)
        {
            if (typeof(Control).IsAssignableFrom(popup.GetType()))
            {
                Control pc = (Control)popup;
                if (pc.Parent != null && pc.Parent is Panel pn)
                {
                    if (pn.Parent != null && pn.Parent is PanelPopup popupItem)
                    {
                        MainWindow.Instance.ClosePopup(popupItem);
                    }
                }
            }
        }
    }
}