using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Components;
using Musix.Controls.MenuItems;
using Musix.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;
using Musix.Windows.API.Themes;
using TagLib.Ape;

namespace Musix.Controls
{
    public partial class MusixDynamicSidebarItem : UserControl, IStyleableControl
    {
        public readonly IMusixMenuItem MenuItem;
        private IAssetCache<Image> AssetCache = new MusixAssetCache();
        private bool _IsSelected = false;
        private bool _IsHover = false;

        public TaskScheduler TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        public TaskFactory UITaskFactory;

        public delegate void OnSelectArgs(MusixDynamicSidebarItem item);

        public event OnSelectArgs OnSelect;

        public delegate void OnCursorUpdateArgs(MusixDynamicSidebarItem item);

        public event OnCursorUpdateArgs OnCursorUpdate;

        public Color SelectedColor = ColorHelper.GetColor("3d4858");

        public Color HoverColor = Color.FromArgb(80, 83, 88);

        public Color DefaultColor = Color.FromArgb(44, 47, 51);

        public IStatusIconProvider StatusIconProvider;

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (!IsSelected && value)
                {
                    OnSelect?.Invoke(this);
                }
                _IsSelected = value;
                if (value)
                {
                    SendHighlight(SelectedColor);
                }
                else
                {
                    if (IsHover)
                    {
                        SendHighlight(HoverColor);
                    }
                    else
                    {
                        SendHighlight(DefaultColor);
                    }
                }
            }
        }

        public bool IsHover
        {
            get
            {
                return _IsHover;
            }
            set
            {
                _IsHover = value;
                if (!IsSelected)
                {
                    if (value)
                    {
                        SendHighlight(HoverColor);
                    }
                    else
                    {
                        SendHighlight(DefaultColor);
                    }
                }
            }
        }

        public void SendHighlight(Color color)
        {
            BackColor = color;
            lblTitle.BackColor = color;
            pbIcon.BackColor = color;
        }

        public MusixDynamicSidebarItem()
        {
            InitializeComponent();
            Init();
            Load += MusixDynamicSidebarItem_Load;
        }

        public MusixDynamicSidebarItem(IMusixMenuItem item)
        {
            InitializeComponent();
            MenuItem = item;
            Init();
            LoadUI();
            UITaskFactory = new TaskFactory(TaskScheduler);
            if (typeof(IStatusIconProvider).IsAssignableFrom(item.GetType()))
            {
                StatusIconProvider = (IStatusIconProvider)item;
                StatusIconProvider.UpdateIcon += Provider_UpdateIcon;
                StatusIconProvider.Init();
            }
        
            Load += MusixDynamicSidebarItem_Load;
        }

        private void Provider_UpdateIcon(Image image)
        {
            UITaskFactory.StartNew(() => UpdateStatusIcon(image));
        }


        public void UpdateStatusIcon(Image image)
        {
            if (StatusIconProvider.DisposeOnUpdate && pbStat.Image != null)
            {
                pbStat.Image.Dispose();
            }
            pbStat.Visible = true;
            pbStat.Image = image;

        }

        private void MusixDynamicSidebarItem_Load(object sender, EventArgs e)
        {
            Width = Parent.Width;
        }

        private void Init()
        {
            Click += OnClick;
            pbIcon.Click += OnClick;
            lblTitle.Click += OnClick;

            this.MouseEnter += OnCUpdate;
            //lblTitle.MouseEnter += OnCUpdate;
            //pbIcon.MouseEnter += OnCUpdate;

            this.MouseLeave += OnCUpdate;

            lblTitle.MouseLeave += OnCUpdate;
            pbIcon.MouseLeave += OnCUpdate;
        }

        private void OnCUpdate(object sender, EventArgs e)
        {
            OnCursorUpdate?.Invoke(this);
        }

        private void OnClick(object sender, System.EventArgs e)
        {
            IsSelected = true;
        }

        public void SendStyle(EStyle Style)
        {
            StatusIconProvider?.StyleChanged(Style);
            if (!AssetCache.HasAsset(Style, EAsset.MenuIcon))
            {
                Image newAsset = MenuItem.GetIcon(Style);
                AssetCache.RegisterAsset(Style, EAsset.MenuIcon, newAsset);
                pbIcon.Image = newAsset;
            } else
            {
                pbIcon.Image = AssetCache.GetAsset(Style, EAsset.MenuIcon);
            }
        }

        private void LoadUI()
        {
            Image DefaultIcon = GetAsset(EStyle.Blue, EAsset.MenuIcon);
            pbIcon.Image = DefaultIcon;
            lblTitle.Text = MenuItem.Name;
        }

        public Image GetAsset(EStyle style, EAsset asset)
        {
            if (!AssetCache.HasAsset(style, asset))
            {
                Image newAsset = MenuItem.GetIcon(style);
                AssetCache.RegisterAsset(style, EAsset.MenuIcon, newAsset);
                return newAsset;
            }
            else
            {
                return AssetCache.GetAsset(style, asset);
            }
        }
    }
}