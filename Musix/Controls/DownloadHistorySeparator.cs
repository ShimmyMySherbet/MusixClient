using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Musix.Controls
{
    public class DownloadHistorySeparator : Control
    {
        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(100, 5);
        }


    }
}
