using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Managers;

namespace Musix.Controls.Pages
{
    public partial class DownloadsPage : UserControl
    {
        public DownloadsPage()
        {
            InitializeComponent();
        }

        private void DownloadsPage_Load(object sender, EventArgs e)
        {
            DownloadsManager.DownloadsChanged += DownloadsManager_DownloadsChanged;
        }

        private void DownloadsManager_DownloadsChanged()
        {
            label1.Text = $"Downloads: {DownloadsManager.ActiveDownloads}";
        }
    }
}
