using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib.IFD.Tags;
using System.Diagnostics;
using Musix.Models;

namespace Musix.Controls.MenuItems
{
    public partial class PanelPopup : UserControl
    {
        public Control popupBase;
        public Control popup;

        public delegate void OnCloseRequestedArgs(PanelPopup sender, Control popupBase);
        public event OnCloseRequestedArgs OnCloseRequested;

        public PanelPopup()
        {
            InitializeComponent();
        }



        public PanelPopup(Control popupBase, Control popup)
        {
            InitializeComponent();
            this.popup = popup;
            this.popupBase = popupBase;
            pnContent.Size = popup.Size;
            Bitmap pBase = new Bitmap(popupBase.Width, popupBase.Height);
            popupBase.DrawToBitmap(pBase, new Rectangle(popupBase.Location, popupBase.Size));
            Graphics G = Graphics.FromImage(pBase);
            Color coverColor = Color.FromArgb(100, 44, 47, 51);
            G.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            G.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            G.FillRectangle(new SolidBrush(coverColor), new RectangleF(0, 0, pBase.Width, pBase.Height));
            G.Save();
            Size = popupBase.Size;
            BackgroundImage = pBase;
            Load += PanelPopup_Load;
            Click += PanelPopup_Click;
            SizeChanged += PanelPopup_SizeChanged;
        }

        private void PanelPopup_SizeChanged(object sender, EventArgs e)
        {
            pnContent.CentreControlFull();
            popupBase.Size = Size;
            Bitmap bitmap = new Bitmap(popupBase.Width, popupBase.Height);
            popupBase.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), popupBase.Size));
            BackgroundImage = bitmap;
        }

        private void PanelPopup_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"CLOSE POPUP");
            OnCloseRequested?.Invoke(this, popupBase);
        }

        private void PanelPopup_Load(object sender, EventArgs e)
        {
            int MWidth = Width;
            int Mheight = Height;

            int PWidth = popup.Width;
            int PHeight = popup.Height;

            int OffsetWidth = (MWidth - PWidth) / 2;
            int OffsetHeight = (Mheight - PHeight) / 2;

            pnContent.Location = new Point(OffsetWidth, OffsetHeight);


            pnContent.Controls.Add(popup);
            popup.Dock = DockStyle.Fill;


        }
    }
}
