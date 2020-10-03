using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Musix.Controls.MenuItems;
using Musix.Core.Client;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;

namespace Musix
{
    public partial class MainWindow : Form, IStyleableControl
    {
        public MusixClient Client;
        public static MainWindow Instance;
        public Dictionary<Type, IMusixMenuItem> MenuItems = new Dictionary<Type, IMusixMenuItem>();

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Instance = this;
            FileInfo AppBaseInfo = new FileInfo(Application.ExecutablePath);
            Environment.CurrentDirectory = AppBaseInfo.DirectoryName;
            CheckFolders();
            Client = new MusixClient("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef", "ImageCache", "AudioCache");
            Client.OnClientReady += Client_OnClientReady;
            Client.StartClient();

            MenuItems.Add(typeof(SearchMenuItem), new SearchMenuItem());
            MenuItems.Add(typeof(DownloadsMenuItem), new DownloadsMenuItem());
            MenuItems.Add(typeof(SettingsMenuItem), new SettingsMenuItem());
            MDSSideBar.AddItem(MenuItems[typeof(SearchMenuItem)]);
            MDSSideBar.AddItem(MenuItems[typeof(DownloadsMenuItem)]);
            MDSSideBar.AddItem(MenuItems[typeof(SettingsMenuItem)]);

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in asm.GetTypes().Where(x => typeof(IMusixMenuItem).IsAssignableFrom(x) && !x.IsInterface))
                {
                    if (!MenuItems.ContainsKey(t))
                    {
                        IMusixMenuItem newItem = (IMusixMenuItem)Activator.CreateInstance(t);
                        MDSSideBar.AddItem(newItem);
                        MenuItems.Add(t, newItem);
                    }
                }
            }

            MDSSideBar.OnSelectionChanged += MDSSideBar_OnSelectionChanged;
  MDSSideBar.SelectItemAtIndex(0);
            SendStyle(EStyle.Color);
        }

        private void MDSSideBar_OnSelectionChanged(IMusixMenuItem SelectedItem)
        {
            Console.WriteLine("SELECTION CHANGED");
            ShowPanelItem(SelectedItem.GetMenuControl());
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

        //public void ChangeActiveScreen(EMenuPage Page)
        //{
        //    //foreach (Control ActiveControl in PNContent.Controls) ActiveControl.Hide();
        //    //if (Page == EMenuPage.Browse)
        //    //{
        //    //    Browser.Show();
        //    //}
        //    //else if (Page == EMenuPage.Downloads)
        //    //{
        //    //    Downloads.Show();
        //    //}
        //    //else if (Page == EMenuPage.Search)
        //    //{
        //    //    Search.Show();
        //    //}
        //    //else if (Page == EMenuPage.Settings)
        //    //{
        //    //    Settings.Show();
        //    //}
        //}

        public void AddPanelItem(Control control)
        {
            if (!PNContent.Controls.Contains(control))
            {
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                PNContent.Controls.Add(control);
            }
        }

        public void ShowPanelItem(Control control)
        {
            AddPanelItem(control);
            foreach (Control ct in PNContent.Controls)
            {
                if (ct.Visible) ct.Visible = false;
            }
            control.Visible = true;
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