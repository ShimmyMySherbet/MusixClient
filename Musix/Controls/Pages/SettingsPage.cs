using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Controls.MenuItems;
using Musix.Controls.Popups;

namespace Musix.Controls.Pages
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ChangePage<InfoPage>();
        }

        private void btnTestPopup_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowPopup(new TestPopup());
        }
    }
}
