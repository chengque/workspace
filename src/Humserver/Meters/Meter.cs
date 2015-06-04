using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Meters
{
    public partial class Meter : UserControl
    {
        private int meterSize;
        public int MeterSize {
            get
            {
                return meterSize;
            }
            set
            {
                meterSize = value;
                this.Width = meterSize;
                this.Height = meterSize;
                this.Refresh();
            }
        }


        private int meterMargin;

        public  int MeterMargin
        {
            get
            {
                return meterMargin;
            }
            set
            {
                meterMargin = value;
                this.Refresh();
            }
        }

        private int meterArc;

        public int MeterArc
        {
            get
            {
                return meterArc;
            }
            set
            {
                meterArc = value;
                Refresh();
            }
        }

        private Color faceColor;

        public Color FaceColor
        {
            get
            {
                return faceColor;
            }
            set
            {
                faceColor = value;
            }
        }

        private double startValue;

        public double StartValue
        {
            get
            {
                return startValue;
            }
            set
            {
                startValue = value;
                Refresh();
            }
        }

        private double endValue;

        public double EndValue
        {
            get
            {
                return endValue;
            }
            set
            {
                endValue = value;
                Refresh();
            }
        }

        private double currentValue;

        public double CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue=value;
                Refresh();
            }
        }

        private string meterName;
        public string MeterName
        {
            get
            {
                return meterName;
            }
            set
            {
                meterName = value;
                Refresh();
            }
        }

        public Meter()
        {
            InitializeComponent();
            MeterSize = 100;
            MeterMargin = 10;
            MeterArc = 240;
            FaceColor = Color.Red;
            StartValue = 10;
            EndValue = 12.6;
            CurrentValue=StartValue;
        }

        


        private void Meter_Paint(object sender, PaintEventArgs e)
        {
            
            
        }

        private Rectangle GetMeterRect()
        {
            return new Rectangle(MeterMargin, MeterMargin, MeterSize - 2 * MeterMargin, MeterSize - 2 * MeterMargin);
        }

        private void pbBack_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Pen indictPen = new Pen(FaceColor, 3);
            Pen indictSmallPen = new Pen(FaceColor, 1);
            Pen pointerPen = new Pen(Brushes.Black, 4);
            Pen pointerSmallPen = new Pen(Brushes.Red, 1);
            Font font = new System.Drawing.Font(new FontFamily("Arial"), 12);
            Font meterNameFont = new System.Drawing.Font(new FontFamily("微软雅黑"), 12);


            g.DrawArc(indictPen, GetMeterRect(), 90 - (360 - MeterArc) / 2, -MeterArc);

            ///-----------------------------------------------------------------------------------------------------
            ///以下绘制文字
            ///

            //移动原点至控件中心
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            //            g.RotateTransform(45);
            g.RotateTransform(-90 - (MeterArc - 180) / 2);

            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(indictPen, new Point(0, -MeterSize / 2 + MeterMargin + 10), new Point(0, -MeterSize / 2 + MeterMargin));
                ///以下绘制刻度坐标
                string v = (startValue + i * (endValue - startValue) / 4).ToString("f1");
                SizeF size = g.MeasureString(v, font);
                g.DrawString(v, font, Brushes.Red, new PointF(-size.Width / 2, -MeterSize / 2 + MeterMargin +size.Height/2));
                if (i < 4)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        g.RotateTransform(MeterArc / 20);
                        g.DrawLine(indictSmallPen, new Point(0, -MeterSize / 2 + MeterMargin + 5), new Point(0, -MeterSize / 2 + MeterMargin));
                    }
                }
                else
                {
                    g.RotateTransform(-MeterArc / 2);
                }

            }

            string strv = CurrentValue.ToString("f2");
            SizeF sizev = g.MeasureString(strv, font);
            g.DrawString(strv, font, Brushes.Green, new PointF(-sizev.Width / 2, MeterSize / 8));
     
            sizev = g.MeasureString(meterName, font);
            g.DrawString(meterName, meterNameFont, Brushes.Black, new PointF(-sizev.Width / 2, MeterSize / 4));

            

            double cv = CurrentValue;
            if (cv < StartValue)
            {
                cv = StartValue;
            }

            if (cv > EndValue)
            {
                cv = EndValue;
            }

            g.RotateTransform((float)(-MeterArc / 2 + MeterArc * (cv - StartValue) / (EndValue - StartValue)));

            PointF[] pointer = new PointF[5];
            pointer[0] = new PointF(0, MeterMargin);
            pointer[1] = new PointF(5, -5);
            pointer[2] = new PointF(0, -MeterSize / 2 +MeterMargin+20);
            pointer[3] = new PointF(-5, -5);
            pointer[4] = new PointF(0, MeterMargin);
            g.DrawPolygon(pointerPen,pointer );
            g.DrawLine(pointerSmallPen, new Point(0, -5), new Point(0, -MeterSize / 2 + MeterMargin+20));
            g.DrawEllipse(pointerPen, new Rectangle(-1, -1, 2, 2));
        }
    }
}
