using System.Drawing;
using System.Windows.Forms;
using Musix.Windows.API.Themes;

namespace Musix.Windows.API.Models
{
    public interface IMusixMenuItem
    {
        bool ShowWhenUnselected { get; }
        bool ShowWhenSelected { get; }
        string Name { get; }

        Control GetMenuControl();

        Image GetIcon(EStyle style);

        void OnSelect();

        void OnDeselect();
    }
}