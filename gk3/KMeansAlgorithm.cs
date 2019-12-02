using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    static class KMeansAlgorithm
    {
        private static Random rand = new Random();
        public static Bitmap ReduceColors(Bitmap image, int numberOfColors)
        {
            Bitmap _return = new Bitmap(image.Width, image.Height);
            List<MyColor>[] centroids1 = new List<MyColor>[numberOfColors];
            List<MyColor>[] centroids2 = new List<MyColor>[numberOfColors];
            for (int i = 0; i < numberOfColors; ++i)
            {
                centroids1[i] = new List<MyColor>();
                centroids2[i] = new List<MyColor>();
                centroids1[i].Add(new MyColor(rand.Next(256), rand.Next(256), rand.Next(256), rand.Next(256)));
            }
            List<MyColor>[] centroids = centroids1;
            List<MyColor>[] last = centroids2;
            bool flag = true;
            while (flag)
            {                
                for (int x = 0; x < image.Width; ++x)
                {
                    for (int y = 0; y < image.Height; ++y)
                    {
                        int minDist = int.MaxValue;
                        int ind = -1;
                        MyColor pixelColor = new MyColor(image.GetPixel(x, y));
                        for (int i = 0; i < numberOfColors; ++i)
                        {
                            int dist = pixelColor.Distance(centroids[i].First());
                            if (dist < minDist)
                            {
                                minDist = dist;
                                ind = i;
                            }
                        }
                        centroids[ind].Add(pixelColor);
                    }
                }
                if (last[0].Count == 0)
                {
                    last = centroids1;
                    centroids = centroids2;
                }
                else
                {
                    flag = false;
                    for (int i = 0; i < numberOfColors; ++i)
                    {
                        if (centroids[i].Count != last[i].Count)
                        {
                            flag = true;
                            break;
                        }
                        for (int j = 1; j < centroids[i].Count; ++j)
                        {
                            if (centroids[i][j] != last[i][j])
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                            break;
                    }
                    if (flag)
                    {
                        if (centroids == centroids1)
                        {
                            centroids2 = new List<MyColor>[numberOfColors];
                            centroids = centroids2;
                            last = centroids1;
                        }
                        else
                        {
                            centroids1 = new List<MyColor>[numberOfColors];
                            centroids = centroids1;
                            last = centroids2;
                        }                        
                    }
                }
                if(flag)
                {
                    for (int i = 0; i < numberOfColors; ++i)
                    {
                        MyColor avg = new MyColor(0, 0, 0, 0);
                        for (int j = 1; j < last[i].Count; ++j)
                        {
                            avg = avg + last[i][j];
                        }
                        avg = avg / (last[i].Count < 2 ? 1 : last[i].Count - 1);
                        centroids[i] = new List<MyColor>();
                        centroids[i].Add(avg);
                    }
                }
            }
            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    int minDist = int.MaxValue;
                    int ind = -1;
                    MyColor pixelColor = new MyColor(image.GetPixel(x, y));
                    for (int i = 0; i < numberOfColors; ++i)
                    {
                        int dist = pixelColor.Distance(centroids[i].First());
                        if (dist < minDist)
                        {
                            minDist = dist;
                            ind = i;
                        }
                    }
                    _return.SetPixel(x, y, centroids[ind].First().ToColor());
                }
            }
            return _return;
        }
    }
}
