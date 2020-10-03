using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Musix.Controls.Pages
{
    public partial class CreditsPage : UserControl
    {
        public CreditsPage()
        {
            InitializeComponent();
            Load += CreditsPage_Load;
            SizeChanged += CreditsPage_SizeChanged;
        }

        private void CreditsPage_SizeChanged(object sender, EventArgs e)
        {
            CheckSize();
        }

        private void CreditsPage_Load(object sender, EventArgs e)
        {
            CheckSize();
        }

        public void CheckSize()
        {
            foreach (Control control in Controls)
            {
                CentreControl(control);
            }
        }

        public void CentreControl(Control control)
        {
            int cWidth = control.Width;
            int offset = Width - cWidth;
            Point rPos = control.Location;
            rPos.X = (int)(offset / 2.2);
            control.Location = rPos;
        }

        private void btnViewOnGihub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ShimmyMySherbet/MusixDownloader");
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            Process.Start("https://ko-fi.com/shimmymysherbet");
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ShimmyMySherbet/MusixDownloader/tags");
        }
    }
}