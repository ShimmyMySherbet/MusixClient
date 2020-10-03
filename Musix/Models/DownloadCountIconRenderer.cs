using System.Drawing;

namespace Musix.Models
{
    public class DownloadCountIconRenderer
    {
        public Font Font = new Font(FontFamily.GenericSansSerif, 1);
        public Size RenderSize = new Size(1000, 1000);
        public Brush CircleBrush = Brushes.Red;
        public Brush TextBrush = Brushes.Black;

        public Image Render(int Count)
        {
            Bitmap basemap = new Bitmap(RenderSize.Width, RenderSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics G = Graphics.FromImage(basemap);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            G.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            G.FillEllipse(CircleBrush, new RectangleF(new Point(1, 1), new Size(RenderSize.Width - 2, RenderSize.Height - 2)));

            Point BoundingLocation = new Point((int)(RenderSize.Width * 0), (int)(RenderSize.Height * 0));
            Size BoundingSize = new Size((int)(RenderSize.Width * 1), (int)(RenderSize.Height * 1));
            Rectangle BoundingBox = new Rectangle(BoundingLocation, BoundingSize);
            RectangleF BoundingBoxF = new RectangleF(BoundingBox.X, BoundingBox.Y, BoundingBox.Width, BoundingBox.Height);

            Font DrawFont = GetMaxFontSize(BoundingSize, Font, Count.ToString(), out SizeF Bounds);
            int XOffsetRemainder = BoundingBox.Width - (int)Bounds.Width;
            int XOffset = XOffsetRemainder / 2;

            int YOffsetRemainder = BoundingBox.Height - (int)Bounds.Height;
            int YOffset = YOffsetRemainder / 2;

            PointF OffsetRenderOrigin = new PointF(BoundingLocation.X + XOffset, BoundingLocation.Y + YOffset);

            RectangleF OffsetBoundingBoxF = new RectangleF(OffsetRenderOrigin, BoundingBoxF.Size);
            G.DrawString(Count.ToString(), DrawFont, TextBrush, OffsetBoundingBoxF);

            G.Save();
            G.Dispose();
            return basemap;
        }

        public Font GetMaxFontSize(Size BoundingArea, Font font, string Text, out SizeF TextBounds)
        {
            Graphics G = Graphics.FromImage(new Bitmap(10, 10));
            Font lastfont = font;
            SizeF LastBounds = new SizeF(0f, 0f);
            for (int i = 1; true; i++)
            {
                Font newFont = new Font(font.FontFamily, (float)i, font.Style, GraphicsUnit.Pixel, font.GdiCharSet, font.GdiVerticalFont);
                SizeF strSize = G.MeasureString(Text, newFont);
                if (strSize.Width > BoundingArea.Width || strSize.Height > BoundingArea.Height)
                {
                    TextBounds = LastBounds;
                    G.Dispose();
                    return lastfont;
                }
                else
                {
                    lastfont = newFont;
                    LastBounds = strSize;
                }
            }
        }
    }
}