using System;
using System.Windows.Forms;
using Musix.Core.Helpers;
using Musix.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;

namespace Musix.Controls.Popups
{
    public partial class ManualResolvePopup : UserControl, IPopupItem
    {
        public Delegates.OnManualResolveCancelled OnManualResolveCancelled;
        public Delegates.OnManualResolveFinished OnManualResolveFinished;

        public ManualResolvePopup()
        {
            InitializeComponent();
        }

        public ManualResolvePopup(string YoutubeURL, string SpotifyURL, Delegates.OnManualResolveCancelled onManualResolveCancelled, Delegates.OnManualResolveFinished onManualResolveFinished)
        {
            InitializeComponent();
            txtSpotify.Text = SpotifyURL;
            txtYoutube.Text = YoutubeURL;
            OnManualResolveFinished = onManualResolveFinished;
            OnManualResolveCancelled = onManualResolveCancelled;
        }

        private void ManualResolvePopup_Load(object sender, EventArgs e)
        {
            lblt.CentreControl();
            btnApply.CentreControl();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSpotify.Text) || string.IsNullOrEmpty(txtYoutube.Text))
            {
                MessageBox.Show(this, "Both fields need to be set with valid links", "Incomplete Fields");
            }
            else
            {
                string YTID = YoutubeHeleprs.GetVideoID(txtYoutube.Text);
                string SPID = MainWindow.Instance.Client.ExtractSpotifyID(txtSpotify.Text);
                this.ClosePopup();
                OnManualResolveFinished(SPID, YTID);
            }
        }

        public void OnPopupOpen()
        {
        }

        public bool OnBeforePopupClose()
        {
            return true;
        }

        public void OnPopupClose()
        {
            OnManualResolveCancelled();
        }
    }
}