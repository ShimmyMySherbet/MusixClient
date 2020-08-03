using System;
using System.Windows.Forms;
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

        public MusixSideBar()
        {
            InitializeComponent();

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
            } else if (Style == EStyle.Color)
            {
                pbBrowse.Image = Assets.Browse_Color;
                pbDownloads.Image = Assets.DownloadMain_Color;
                pbSearch.Image = Assets.Search_Color;
                pbSettings.Image = Assets.Settings_Color;
            }
        }
    }
}