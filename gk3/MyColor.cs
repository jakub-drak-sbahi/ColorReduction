using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    class MyColor
    {
        public double A { get; private set; }
        public double R { get; private set; }
        public double G { get; private set; }
        public double B { get; private set; }

        public MyColor(int A, int R, int G, int B)
        {
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public MyColor(Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public MyColor(double R, double G, double B)
        {
            this.A = 255;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public Color ToColor()
        {
            if (R < 0)
                R = 0;
            if (G < 0)
                G = 0;
            if (B < 0)
                B = 0;
            //return Color.FromArgb((int)A % 256, (int)R % 256, (int)G % 256, (int)B % 256);
            //return Color.FromArgb((int)A, (int)R, (int)G, (int)B);
            return Color.FromArgb((int)A>255?255:(int)A, (int)R > 255 ? 255 : (int)R, (int)G > 255 ? 255 : (int)G, (int)B > 255 ? 255 : (int)B);
        }

        public static MyColor operator -(MyColor c1, MyColor c2)
            => new MyColor(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B);

        public static MyColor operator +(MyColor c1, MyColor c2)
            => new MyColor(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);

        public static MyColor operator *(MyColor c, double d)
            => new MyColor(c.R * d, c.G * d, c.B * d);
    }
}
