using System;
using System.Windows.Forms;
using Musix.Controls.Pages;
using Musix.Models;
using Musix.Core;
using Musix.Core.Client;
using System.IO;
using AngleSharp.Common;
using Musix.Windows.API.Themes;
using Musix.Windows.API.Interfaces;

namespace Musix
{
    public partial class MainWindow : Form, IStyleableControl
    {
        public MusixClient Client;
        public BrowserPage Browser = new BrowserPage();
        public SearchPage Search;
        public DownloadsPage Downloads = new DownloadsPage();
        public SettingsPage Settings = new SettingsPage();

        private void MainWindow_Load(object sender, EventArgs e)
        {
            FileInfo AppBaseInfo = new FileInfo(Application.ExecutablePath);
            Environment.CurrentDirectory = AppBaseInfo.DirectoryName;
            CheckFolders();
            Client = new MusixClient("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef", "ImageCache", "AudioCache");
            Client.OnClientReady += Client_OnClientReady;
            Client.StartClient();

            Search = new SearchPage(this);

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


            SendStyle(EStyle.Color);

        }

        private void Client_OnClientReady()
        {
            Console.WriteLine(">>>CLIENT READY");
        }

        public void CheckFolders()
        {
            if (!Directory.Exists("ImageCache")) Directory.CreateDirectory("ImageCache");
            if (!Directory.Exists("AudioCache")) Directory.CreateDirectory("AudioCache");
            if (!Directory.Exists("Music")) Directory.CreateDirectory("Music");
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
            foreach (Control Ct in PNContent.Controls)
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