using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Themes;

namespace Musix.Windows.API.Models
{
    public abstract class MusixMenuItem<Page> : IMenuItem where Page : Control
    {
        public string Name => "";

        public Image DefaultIcon => null;

        public Image GetIcon(EStyle style)
        {
            return null;
        }

        public virtual void OnDeselect()
        {
        }

        public virtual void OnSelect()
        {
        }
    }
}
