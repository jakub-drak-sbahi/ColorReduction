using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace gk3
{
    public enum FilterMatrix { FloydAndSteinberg, Burkes, Stucky }
    static class ErrorDiffusionDithering
    {
        public static Bitmap ReduceColors(Bitmap image, FilterMatrix filterMatrix, int numberOfColors)
        {
            double[,] filter = new double[1, 1];
            switch (filterMatrix)
            {
                case FilterMatrix.FloydAndSteinberg:
                    filter = new double[3, 3];
                    for (int i = 0; i < 3; ++i)
                        for (int j = 0; j < 3; ++j)
                            filter[i, j] = 0;
                    filter[2, 1] = 7.0 / 16;
                    filter[0, 2] = 3.0 / 16;
                    filter[1, 2] = 5.0 / 16;
                    filter[2, 2] = 1.0 / 16;
                    break;
                case FilterMatrix.Burkes:
                    filter = new double[5, 3];
                    for (int i = 0; i < 5; ++i)
                        for (int j = 0; j < 3; ++j)
                            filter[i, j] = 0;
                    filter[3, 1] = 8.0 / 32;
                    filter[4, 1] = 4.0 / 32;
                    filter[0, 2] = 2.0 / 32;
                    filter[1, 2] = 4.0 / 32;
                    filter[2, 2] = 8.0 / 32;
                    filter[3, 2] = 4.0 / 32;
                    filter[4, 2] = 2.0 / 32;
                    break;
                case FilterMatrix.Stucky:
                    filter = new double[5, 5];
                    for (int i = 0; i < 5; ++i)
                        for (int j = 0; j < 5; ++j)
                            filter[i, j] = 0;
                    filter[3, 2] = 8.0 / 42;
                    filter[4, 2] = 4.0 / 42;
                    filter[0, 3] = 2.0 / 42;
                    filter[1, 3] = 4.0 / 42;
                    filter[2, 3] = 8.0 / 42;
                    filter[3, 3] = 4.0 / 42;
                    filter[4, 3] = 2.0 / 42;
                    filter[0, 4] = 1.0 / 42;
                    filter[1, 4] = 2.0 / 42;
                    filter[2, 4] = 4.0 / 42;
                    filter[3, 4] = 2.0 / 42;
                    filter[4, 4] = 1.0 / 42;
                    break;
                default:
                    break;
            }

            Bitmap _return = new Bitmap(image.Width, image.Height);
            MyColor[,] table = new MyColor[image.Width, image.Height];
            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    table[x, y] = new MyColor(image.GetPixel(x, y));
                }
            }
            int diffX = filter.GetLength(0) / 2;
            int diffY = filter.GetLength(1) / 2;

            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    MyColor oldPixel = table[x, y];
                    MyColor K = Approximate(table[x, y].ToColor(), numberOfColors);
                    table[x, y] = K;
                    MyColor error = oldPixel - table[x, y];
                    for (int i = 0; i < filter.GetLength(0); ++i)
                    {
                        for (int j = 0; j < filter.GetLength(1); ++j)
                        {
                            if (x + i - diffX >= 0 && y + j - diffY >= 0 && x + i - diffX < table.GetLength(0) && y + j - diffY < table.GetLength(1))
                            {
                                table[x + i - diffX, y + j - diffY] = table[x + i - diffX, y + j - diffY] + (error * filter[i, j]);
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    _return.SetPixel(x, y, table[x, y].ToColor());
                }
            }
            return _return;
        }

        private static MyColor Approximate(Color color, int numberOfColors)
        {
            numberOfColors -= 1;
            double period = 255.0 / numberOfColors;
            double rn = color.R / (double)period;
            double gn = color.G / (double)period;
            double bn = color.B / (double)period;
            double R = rn - (int)rn < 0.5 ? (int)rn * period : ((int)rn + 1) * period;
            double G = gn - (int)gn < 0.5 ? (int)gn * period : ((int)gn + 1) * period;
            double B = bn - (int)bn < 0.5 ? (int)bn * period : ((int)bn + 1) * period;
            return new MyColor(R, G, B);
        }
    }
}
