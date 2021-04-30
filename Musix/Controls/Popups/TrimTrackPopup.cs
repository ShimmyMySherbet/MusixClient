using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Musix.Core.Components.AudioModifiers;
using Musix.Windows.API.Interfaces;
using Musix.Models;

namespace Musix.Controls
{
    public partial class TrimTrackPopup : UserControl, IPopupItem
    {

        public delegate void TrimtrackCallbackArgs(AudioTrimmer trimmer);

        public TrimtrackCallbackArgs Callback;


        public TrimTrackPopup()
        {
            InitializeComponent();
        }



        public TrimTrackPopup(TrimtrackCallbackArgs callback, int StartTime = 0, int EndTime = 0)
        {
            InitializeComponent();
            Callback = callback;
            txtStart.Text = StartTime.ToString();
            txtEnd.Text = EndTime.ToString();
        }

        private void TrimTrackControl_Load(object sender, EventArgs e)
        {

        }

        private void btnTrim_Click(object sender, EventArgs e)
        {
            int ST = int.Parse(txtStart.Text);
            int ET = int.Parse(txtEnd.Text);
            AudioTrimmer trimmer = new AudioTrimmer(TimeSpan.FromSeconds(ST), TimeSpan.FromSeconds(ET));
            Callback?.Invoke(trimmer);
            this.ClosePopup();
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
        }

        private void txtStart_TextChanged(object sender, EventArgs e)
        {
            txtStart.Text = ToInt(txtStart.Text).ToString();
        }

        char[] Numeric = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public int ToInt(string str)
        {
            StringBuilder b = new StringBuilder();
            foreach(char cha in str)
            {
                if (Numeric.Contains(cha))
                    b.Append(cha);
            }
            string e = b.ToString();
            if (string.IsNullOrEmpty(e))
            {
                return 0;
            } else
            {
                return int.Parse(e);
            }
        }

        private void txtEnd_TextChanged(object sender, EventArgs e)
        {
            txtEnd.Text = ToInt(txtEnd.Text).ToString();
        }
    }
}
