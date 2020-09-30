using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Windows.API.Themes;

namespace Musix.Windows.API.Interfaces
{
    public interface IMenuItem
    {
        string Name { get; }
        Image DefaultIcon { get; }
        Image GetIcon(EStyle style);
        void OnSelect();
        void OnDeselect();
    }
}
