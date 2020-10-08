using System;
using System.Windows.Forms;
using Musix.Models;
using Musix.Windows.API.Interfaces;
using Musix.Windows.API.Models;

namespace Musix.Controls.Popups
{
    public partial class DragDropPopup : UserControl, IPopupItem
    {
        public Delegates.DragDropPopupCallback Callback;

        public DragDropPopup()
        {
            InitializeComponent();
            Init();
        }

        public DragDropPopup(Delegates.DragDropPopupCallback callback)
        {
            InitializeComponent();
            Init();
            Callback = callback;
        }

        private void Init()
        {
            label1.CentreControl();
            AllowDrop = true;
            DragEnter += DragDropPopup_DragEnter;
            DragOver += DragDropPopup_DragOver;
            DragDrop += DragDropPopup_DragDrop;
            DragLeave += DragDropPopup_DragLeave;
        }

        private void DragDropPopup_DragLeave(object sender, EventArgs e)
        {
            Console.WriteLine("UI CANCEL");
            Callback?.Invoke("", true);
            this.ClosePopup();
        }

        private void DragDropPopup_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("UI DROP");
            Callback?.Invoke((string)e.Data.GetData(DataFormats.StringFormat, true), false);
            this.ClosePopup();
        }

        private void DragDropPopup_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void DragDropPopup_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        public bool OnBeforePopupClose()
        {
            return true;
        }

        public void OnPopupClose()
        {
        }

        public void OnPopupOpen()
        {
        }
    }
}