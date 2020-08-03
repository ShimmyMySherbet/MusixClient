using System;
using System.Drawing;
using System.Windows.Forms;
using Musix.Components;
using Musix.Models;

namespace Musix.Controls
{
    public partial class MusixSideBar : UserControl, IStyleableControl
    {
        public delegate void SelectArgs();

        public event SelectArgs BrowseSelected;

        public event SelectArgs SearchSelected;

        public event SelectArgs DownloadsSelected;

        public event SelectArgs SettingsSelected;

        public delegate void SelectionChangedArgs(EMenuPage Page);

        public event SelectionChangedArgs SelectionChanged;

        public EMenuPage SelectedPage { get; protected set; } = EMenuPage.Browse;

        public Color SelectedColor = ColorHelper.GetColor("3d4858");

        public Color HoverColor = Color.FromArgb(80, 83, 88);

        public MusixSideBar()
        {
            InitializeComponent();

            PnBrowse.BackgroundImageLayout = ImageLayout.Stretch;
            PnDownloads.BackgroundImageLayout = ImageLayout.Stretch;
            PnSearch.BackgroundImageLayout = ImageLayout.Stretch;
            PnSettings.BackgroundImageLayout = ImageLayout.Stretch;

            PnBrowse.Click += Browse_Select;
            lblBrowse.Click += Browse_Select;
            pbBrowse.Click += Browse_Select;

            PnSearch.Click += Search_Select;
            lblSearch.Click += Search_Select;
            pbSearch.Click += Search_Select;

            PnDownloads.Click += Downloads_Select;
            lblDownloads.Click += Downloads_Select;
            pbDownloads.Click += Downloads_Select;

            PnSettings.Click += Settings_Select;
            lblSettings.Click += Settings_Select;
            pbSettings.Click += Settings_Select;

            SelectionChanged += MusixSideBar_SelectionChanged;

            UpdateHighlights();

            PnBrowse.MouseEnter += Browse_MouseChanged;
            PnBrowse.MouseLeave += Browse_MouseChanged;

            pbBrowse.MouseEnter += Browse_MouseChanged;
            pbBrowse.MouseLeave += Browse_MouseChanged;

            lblBrowse.MouseEnter += Browse_MouseChanged;
            lblBrowse.MouseLeave += Browse_MouseChanged;

            PnDownloads.MouseEnter += Downloads_MouseChanged;
            PnDownloads.MouseLeave += Downloads_MouseChanged;

            pbDownloads.MouseEnter += Downloads_MouseChanged;
            pbDownloads.MouseLeave += Downloads_MouseChanged;

            lblDownloads.MouseEnter += Downloads_MouseChanged;
            lblDownloads.MouseLeave += Downloads_MouseChanged;

            PnSearch.MouseEnter += Search_MouseChanged;
            PnSearch.MouseLeave += Search_MouseChanged;

            PnSearch.MouseEnter += Search_MouseChanged;
            PnSearch.MouseLeave += Search_MouseChanged;

            lblSearch.MouseEnter += Search_MouseChanged;
            lblSearch.MouseLeave += Search_MouseChanged;

            PnSettings.MouseEnter += Settings_MouseChanged;
            PnSettings.MouseLeave += Settings_MouseChanged;

            pbSettings.MouseEnter += Settings_MouseChanged;
            pbSettings.MouseLeave += Settings_MouseChanged;

            lblSettings.MouseEnter += Settings_MouseChanged;
            lblSettings.MouseLeave += Settings_MouseChanged;
        }

        private void Settings_MouseChanged(object sender, EventArgs e)
        {
            if (SelectedPage == EMenuPage.Settings) return;
            if (CursorWithinControl(PnSettings))
            {
                if (PnSettings.BackColor != HoverColor) PnSettings.BackColor = HoverColor;
            }
            else
            {
                if (PnSettings.BackColor != Color.Transparent) PnSettings.BackColor = Color.Transparent;
            }
        }

        private void Search_MouseChanged(object sender, EventArgs e)
        {
            if (SelectedPage == EMenuPage.Search) return;
            if (CursorWithinControl(PnSearch))
            {
                if (PnSearch.BackColor != HoverColor) PnSearch.BackColor = HoverColor;
            }
            else
            {
                if (PnSearch.BackColor != Color.Transparent) PnSearch.BackColor = Color.Transparent;
            }
        }

        private void Downloads_MouseChanged(object sender, EventArgs e)
        {
            if (SelectedPage == EMenuPage.Downloads) return;
            if (CursorWithinControl(PnDownloads))
            {
                if (PnDownloads.BackColor != HoverColor) PnDownloads.BackColor = HoverColor;
            }
            else
            {
                if (PnDownloads.BackColor != Color.Transparent) PnDownloads.BackColor = Color.Transparent;
            }
        }

        private void Browse_MouseChanged(object sender, EventArgs e)
        {
            if (SelectedPage == EMenuPage.Browse) return;
            if (CursorWithinControl(PnBrowse))
            {
                if (PnBrowse.BackColor != HoverColor) PnBrowse.BackColor = HoverColor;
            }
            else
            {
                if (PnBrowse.BackColor != Color.Transparent) PnBrowse.BackColor = Color.Transparent;
            }
        }

        private void MusixSideBar_SelectionChanged(EMenuPage Page)
        {
            UpdateHighlights();
        }

        public void UpdateHighlights()
        {
            switch (SelectedPage)
            {
                case EMenuPage.Browse:
                    PnBrowse.BackColor = SelectedColor;
                    pbBrowse.BackColor = SelectedColor;
                    PnDownloads.BackColor = Color.Transparent;
                    PnSearch.BackColor = Color.Transparent;
                    PnSettings.BackColor = Color.Transparent;
                    PnDownloads.BackColor = Color.Transparent;
                    pbSearch.BackColor = Color.Transparent;
                    pbSettings.BackColor = Color.Transparent;
                    break;

                case EMenuPage.Downloads:
                    PnDownloads.BackColor = SelectedColor;
                    pbDownloads.BackColor = SelectedColor;
                    PnBrowse.BackColor = Color.Transparent;
                    PnSettings.BackColor = Color.Transparent;
                    PnSearch.BackColor = Color.Transparent;
                    pbBrowse.BackColor = Color.Transparent;
                    pbSettings.BackColor = Color.Transparent;
                    pbSearch.BackColor = Color.Transparent;
                    break;

                case EMenuPage.Search:
                    PnSearch.BackColor = SelectedColor;
                    pbSearch.BackColor = SelectedColor;
                    PnDownloads.BackColor = Color.Transparent;
                    PnBrowse.BackColor = Color.Transparent;
                    PnSettings.BackColor = Color.Transparent;
                    pbDownloads.BackColor = Color.Transparent;
                    pbBrowse.BackColor = Color.Transparent;
                    pbSettings.BackColor = Color.Transparent;
                    break;

                case EMenuPage.Settings:
                    PnSettings.BackColor = SelectedColor;
                    pbSettings.BackColor = SelectedColor;
                    PnDownloads.BackColor = Color.Transparent;
                    PnBrowse.BackColor = Color.Transparent;
                    PnSearch.BackColor = Color.Transparent;
                    pbDownloads.BackColor = Color.Transparent;
                    pbBrowse.BackColor = Color.Transparent;
                    pbSearch.BackColor = Color.Transparent;
                    break;
            }
        }

        private void Browse_Select(object sender, EventArgs e)
        {
            if (SelectedPage != EMenuPage.Browse)
            {
                SelectedPage = EMenuPage.Browse;
                BrowseSelected?.Invoke();
                SelectionChanged?.Invoke(SelectedPage);
            }
        }

        private void Search_Select(object sender, EventArgs e)
        {
            if (SelectedPage != EMenuPage.Search)
            {
                SelectedPage = EMenuPage.Search;
                SearchSelected?.Invoke();
                SelectionChanged?.Invoke(SelectedPage);
            }
        }

        private void Downloads_Select(object sender, EventArgs e)
        {
            if (SelectedPage != EMenuPage.Downloads)
            {
                SelectedPage = EMenuPage.Downloads;
                DownloadsSelected?.Invoke();
                SelectionChanged?.Invoke(SelectedPage);
            }
        }

        private void Settings_Select(object sender, EventArgs e)
        {
            if (SelectedPage != EMenuPage.Settings)
            {
                SelectedPage = EMenuPage.Settings;
                SettingsSelected?.Invoke();
                SelectionChanged?.Invoke(SelectedPage);
            }
        }

        public void SendStyle(EStyle Style)
        {
            if (Style == EStyle.Blue)
            {
                pbBrowse.Image = Assets.Browse_Blue;
                pbDownloads.Image = Assets.DownloadMain_Blue;
                pbSearch.Image = Assets.Search_Blue;
                pbSettings.Image = Assets.Settings_Blue;
            }
            else if (Style == EStyle.Color)
            {
                pbBrowse.Image = Assets.Browse_Color;
                pbDownloads.Image = Assets.DownloadMain_Color;
                pbSearch.Image = Assets.Search_Color;
                pbSettings.Image = Assets.Settings_Color;
            }
        }

        public bool CursorWithinControl(Control Ctrl)
        {
            Point GlobalCursor = Cursor.Position;
            Point LocalCursor = PointToClient(GlobalCursor);
            Rectangle CtrlRect = new Rectangle(Ctrl.Location, Ctrl.Size);
            return CtrlRect.Contains(LocalCursor);
        }
    }
}