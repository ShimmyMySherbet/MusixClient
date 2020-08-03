using System;
using System.Windows.Forms;
using Musix.Models;

namespace Musix
{
    public partial class MainWindow : Form, IStyleableControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SendStyle(EStyle Style)
        {
            if (Style == EStyle.Blue)
            {
                Icon = Assets.Musix_Icon_Blue;
            }
            else if (Style == EStyle.Color)
            {
                Icon = Assets.Musix_Icon_Rainbow;
            }

            foreach (Control Ct in Controls)
            {
                if (typeof(IStyleableControl).IsAssignableFrom(Ct.GetType()))
                {
                    IStyleableControl styleableControl = Ct as IStyleableControl;
                    styleableControl.SendStyle(Style);
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SendStyle(EStyle.Blue);
        }
    }
}