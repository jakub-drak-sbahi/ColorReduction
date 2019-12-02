using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    static class PopularityAlgorithm
    {
        public static Bitmap ReduceColors(Bitmap image, int numberOfColors)
        {
            Bitmap _return = new Bitmap(image.Width, image.Height);
            MyColorComparer comparer = new MyColorComparer();
            Dictionary<MyColor, int> dict = new Dictionary<MyColor, int>(comparer);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    MyColor color = new MyColor(image.GetPixel(i, j));
                    if (dict.ContainsKey(color))
                    {
                        dict[color]++;
                    }
                    else
                    {
                        dict[color] = 0;
                    }
                }
            }
            List<KeyValuePair<MyColor, int>> dictList = dict.ToList();
            dictList.Sort((a, b) => b.Value - a.Value);
            dict = dictList.GetRange(0, numberOfColors <= dictList.Count ? numberOfColors : dictList.Count).ToDictionary(x => x.Key, x => x.Value, comparer);
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    int minDist = int.MaxValue;
                    MyColor color = new MyColor(0, 0, 0);
                    MyColor currentColor = new MyColor(image.GetPixel(i, j));
                    foreach (var elem in dict)
                    {
                        int dist = elem.Key.Distance(currentColor);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            color = elem.Key;
                        }                        
                    }
                    _return.SetPixel(i, j, color.ToColor());
                }
            }
            return _return;
        }

    }

    class MyColorComparer : IEqualityComparer<MyColor>
    {
        public bool Equals(MyColor x, MyColor y)
        {
            return x.A==y.A && x.R == y.R && x.G == y.G && x.B == y.B;
        }

        public int GetHashCode(MyColor obj)
        {
            return (int)obj.A*1000000000 + (int)(obj.R) * 1000000 + (int)(obj.G) * 1000 + (int)(obj.B);
        }
    }

}
