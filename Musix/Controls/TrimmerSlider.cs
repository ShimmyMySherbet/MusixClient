using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Musix.Controls
{
    public class TrimmerSlider : Control
    {
        private Size MinSize = new Size(200, 10);
        public Color BackgroundColor = Color.FromArgb(44, 47, 51);
        public override Size GetPreferredSize(Size proposedSize)
        {
            if ((proposedSize.Width * proposedSize.Height) < (MinSize.Width * MinSize.Height))
            {
                return MinSize;
            }
            return base.GetPreferredSize(proposedSize);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle Clip = e.ClipRectangle;
            Console.WriteLine($"Send Paint: {e.ClipRectangle}");
            e.Graphics.FillRectangle(new SolidBrush(BackgroundColor), ShrinkRectangle(e.ClipRectangle, 10));

            //float MinS;
            //if (Clip.Height < Clip.Width)
            //{
            //    MinS = Clip.Height;
            //} else
            //{
            //    MinS = Clip.Width;
            //}

            //RectangleF LeftFloat = new RectangleF()



            Rectangle InnerBound = ShrinkRectangle(e.ClipRectangle, 20);
            e.Graphics.FillRectangle(Brushes.White, InnerBound);

            Rectangle OuterB = ShrinkRectangle(InnerBound, 10);
            e.Graphics.FillRectangle(Brushes.Aqua, OuterB);

            Rectangle Square = new Rectangle(0, 0, 500, 500);

            RectangleF Clipped = ClipRectangle(Square, OuterB);

            e.Graphics.FillRectangle(Brushes.Coral, Clipped);



        }



        public Rectangle ShrinkRectangle(Rectangle rectangle, int shrink)
        {
            return new Rectangle() { Height = rectangle.Height - shrink, Width = rectangle.Width - shrink, X = (rectangle.X + (shrink / 2)), Y = (rectangle.Y + (shrink / 2)) };
        }


        public RectangleF ClipRectangle(Rectangle innerRectangle, Rectangle outerRectangle)
        {

            int OuterHeight = outerRectangle.Height;
            int OuterWidth = outerRectangle.Width;

            int OuterX = outerRectangle.X;
            int OuterY = outerRectangle.Y;



            int InnerHeight = innerRectangle.Height;
            int InnerWidth = innerRectangle.Width;

            int InnerX = innerRectangle.X;
            int InnerY = innerRectangle.Y;


            float HeightRatio = (float)OuterHeight / InnerHeight;
            float WidthRatio = (float)InnerWidth / OuterWidth;


            if (HeightRatio > WidthRatio)
            {
                //Bounds is taller

                int HeightRemainder = OuterHeight - InnerHeight;

                int HeightGap = HeightRemainder / 2;



                return new RectangleF(OuterX, OuterY + HeightGap, OuterWidth, InnerHeight / HeightRatio);






            } else
            {
                //Bounds is wider



                int WidthRemainder = OuterWidth - InnerWidth;

                int WidthGap = WidthRemainder / 2;

                return new RectangleF(OuterX + WidthGap, OuterY, OuterWidth / WidthRatio, InnerHeight);



            }




        }


    }
}
