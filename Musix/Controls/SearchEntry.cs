using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Models;
using Musix.Windows.API.Themes;
using Musix.Windows.API.Interfaces;

namespace Musix.Controls
{
    public partial class SearchEntry : UserControl, IStyleableControl
    {
        public SearchEntry()
        {
            InitializeComponent();
        }

        public void SendStyle(EStyle Style)
        {
            if (Style == EStyle.Blue)
            {
                pbDownload.Image = Assets.DownloadMain_Blue;
            } else if (Style == EStyle.Color)
            {
                pbDownload.Image = Assets.DownloadMain_Color;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SearchEntry_Load(object sender, EventArgs e)
        {
            Width = (Parent.Width - 30);
        }

        private void pbDownload_Click(object sender, EventArgs e)
        {

        }
    }
}
