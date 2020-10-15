using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Musix.Controls.MenuItems;
using Musix.Core.Client;
using Musix.Managers;
using Musix.Models;
using Musix.Windows.API.Attributes;
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
        public IDependancyAssetCache<Image, object, string> UIAssetCache = new MusixUIAssetCache();
        public Dictionary<string, IMusixPlugin> Plugins = new Dictionary<string, IMusixPlugin>();

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Instance = this;
            DateTime Started = DateTime.Now;
            FileInfo AppBaseInfo = new FileInfo(Application.ExecutablePath);
            Environment.CurrentDirectory = AppBaseInfo.DirectoryName;
            ConfigManager.Init();

            CheckFolders();

            DateTime PStarted = DateTime.Now;
            foreach (string PluginFile in Directory.GetFiles("Plugins", "*.dll"))
            {
                FileInfo info = new FileInfo(PluginFile);
                try
                {
                    string SName = info.Name.Substring(0, info.Name.Length - info.Extension.Length);
                    Console.WriteLine($"Loading plugin {SName}...");
                    List<string> Deps = new List<string>();
                    if (Directory.Exists(Path.Combine("Plugins", SName)))
                    {
                        foreach (string Dependancy in Directory.GetFiles(Path.Combine("Plugins", info.Name), "*.dll"))
                        {
                            Deps.Add(Dependancy);
                        }
                    }
                    Console.WriteLine($"Loading {Deps.Count} dependancies for plugin {info.Name}...");
                    foreach(string asm in Deps)
                    {
                        AppDomain.CurrentDomain.Load(File.ReadAllBytes(asm));
                    }

                    Assembly Plugin = Assembly.LoadFrom(PluginFile);
                    foreach (Type t in Plugin.GetTypes())
                    {
                        if (typeof(IMusixPlugin).IsAssignableFrom(t))
                        {
                            Console.WriteLine($"Initializing plugin {info.Name}...");
                            IMusixPlugin entryPoint = ((IMusixPlugin)Activator.CreateInstance(t));
                            entryPoint.Load();
                            Plugins.Add(entryPoint.Name, entryPoint);
                            Console.WriteLine($"Initialized plugin {entryPoint.Name}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load plugin {info.Name}; {ex.Message}");
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine($"[Plugins Loaded] took {Math.Round(DateTime.Now.Subtract(PStarted).TotalMilliseconds),2} millisecond/s.");


            Client = new MusixClient("955b354ccd0e4270b6ad97f8b4003d9a", "5a008b85c33b499da7857fbdf05f08ef", "ImageCache", "AudioCache");
            Client.OnClientReady += Client_OnClientReady;
            Client.StartClient();

            AcceptButton = new Button();

            Activated += MainWindow_Activated;
            Deactivate += MainWindow_Deactivate;
            ((Button)AcceptButton).Click += BtnAccept_Click;

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

            foreach (var item in MenuItems)
            {
                if (Attribute.GetCustomAttribute(item.Value.GetType(), typeof(AutoInitialize)) != null)
                {
                    Control ct = item.Value.GetMenuControl();
                    ct.Visible = false;
                    ct.Dock = DockStyle.Fill;
                    if (!PNContent.Controls.Contains(ct))
                    {
                        PNContent.Controls.Add(ct);
                    }
                }
            }

            MDSSideBar.OnSelectionChanged += MDSSideBar_OnSelectionChanged;

            MDSSideBar.SelectItemAtIndex(0);
            SendStyle(EStyle.Blue);

            DateTime Finished = DateTime.Now;

            Console.WriteLine($"[Initialized] took {Math.Round(Finished.Subtract(Started).TotalMilliseconds), 3} millisecond/s.");

        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (SelectedPage != null && typeof(IAcceptListener).IsAssignableFrom(SelectedPage.GetType()))
            {
                ((IAcceptListener)SelectedPage).OnPageAccept();
            }
        }

        public Control SelectedPage
        {
            get
            {
                foreach (Control ct in PNContent.Controls)
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
            foreach (Control pn in PNContent.Controls)
            {
                pn.Visible = false;
            }
            page.Dock = DockStyle.Fill;
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
            if (!Directory.Exists("Plugins")) Directory.CreateDirectory("Plugins");
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