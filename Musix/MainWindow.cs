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
using Unosquare.Swan;

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

            Activated += MainWindow_Activated;
            Deactivate += MainWindow_Deactivate;

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
            SendStyle(EStyle.Blue);
        }


        public Control SelectedPage
        {
            get
            {
                foreach(Control ct in PNContent.Controls)
                {
                    if (ct.Visible) return ct;
                }
                return null;
            }
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            MDSSideBar.IsListeningToCursor = false;
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            MDSSideBar.IsListeningToCursor = true;
        }

        private void MDSSideBar_OnSelectionChanged(IMusixMenuItem SelectedItem)
        {
            Console.WriteLine("SELECTION CHANGED");
            ShowPanelItem(SelectedItem.GetMenuControl());
        }

        public void ChangePage<T>() where T : IMusixMenuItem
        {
            if (MenuItems.ContainsKey(typeof(T)))
            {
                MDSSideBar.SelectItem(MenuItems[typeof(T)]);
            }
        }

        public void ChangePage(Control page)
        {
            foreach(Control pn in PNContent.Controls)
            {
                pn.Visible = false;
            }

            if (!PNContent.Controls.Contains(page))
            {
                PNContent.Controls.Add(page);
                page.Dock = DockStyle.Fill;
            }
            page.Visible = true;
        }


        public void RemovePage(Control page, bool dispose = false)
        {
            if (PNContent.Controls.Contains(page))
            {
                PNContent.Controls.Remove(page);
                if (dispose)
                {
                    page.Dispose();
                }
            }
        }

        public void ShowPopup(Control PopupItem)
        {
            Control popupBase = SelectedPage;
            if (popupBase != null)
            {
                PNContent.SuspendLayout();
                PanelPopup popup = new PanelPopup(popupBase, PopupItem);
                DoubleBuffered = true;
                popup.OnCloseRequested += Popup_OnCloseRequested;
                if (typeof(IPopupItem).IsAssignableFrom(PopupItem.GetType()))
                {
                    IPopupItem item = (IPopupItem)PopupItem;
                    item.OnPopupOpen();
                }
                ChangePage(popup);
                PNContent.ResumeLayout();
            }

        }

        private void Item_ClosePopupRequested(object sender, EventArgs e)
        {


        }

        public void ClosePopup(PanelPopup popup)
        {
            Popup_OnCloseRequested(popup, popup.popupBase);
        }

        private void Popup_OnCloseRequested(PanelPopup sender, Control popupBase)
        {
            if (typeof(IPopupItem).IsAssignableFrom(sender.popup.GetType()))
            {
                IPopupItem item = (IPopupItem)sender.popup;
                if (!item.OnBeforePopupClose())
                {
                    return;
                }
                item.OnPopupClose();
            }
            ChangePage(popupBase);
            RemovePage(sender, true);
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