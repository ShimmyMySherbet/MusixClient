using System;
using System.Windows.Forms;
using Musix.Controls.Pages;
using Musix.Models;

namespace Musix
{
    public partial class MainWindow : Form, IStyleableControl
    {
        public BrowserPage Browser = new BrowserPage();
        public SearchPage Search = new SearchPage();
        public DownloadsPage Downloads = new DownloadsPage();
        public SettingsPage Settings = new SettingsPage();

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SuspendLayout();

            PNContent.Controls.Add(Browser);
            PNContent.Controls.Add(Search);
            PNContent.Controls.Add(Downloads);
            PNContent.Controls.Add(Settings);

            Browser.Hide();
            Search.Hide();
            Downloads.Hide();
            Settings.Hide();

            ResumeLayout();

            SideBar.SelectionChanged += SideBar_SelectionChanged;
            ChangeActiveScreen(EMenuPage.Browse);
            SendStyle(EStyle.Blue);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeActiveScreen(EMenuPage Page)
        {
            foreach (Control ActiveControl in PNContent.Controls) ActiveControl.Hide();
            if (Page == EMenuPage.Browse)
            {
                Browser.Show();
            }
            else if (Page == EMenuPage.Downloads)
            {
                Downloads.Show();
            }
            else if (Page == EMenuPage.Search)
            {
                Search.Show();
            }
            else if (Page == EMenuPage.Settings)
            {
                Settings.Show();
            }
        }

        private void SideBar_SelectionChanged(EMenuPage Page)
        {
            ChangeActiveScreen(Page);
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
    }
}