using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace gk3
{
    static class BitmapConverter
    {
        //[DllImport("user32.dll")]
        //static extern int GetSystemMetrics(int k = 12);

        //public static BitmapSource ConvertBitmapToSource(System.Drawing.Bitmap bitmap)
        //{
        //    using (MemoryStream memory = new MemoryStream())
        //    {
        //        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
        //        memory.Position = 0;
        //        BitmapImage bitmapImage = new BitmapImage();
        //        bitmapImage.BeginInit();
        //        bitmapImage.StreamSource = memory;
        //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapImage.EndInit();

        //        var DPI = GetSystemMetrics() / SystemParameters.IconHeight * 96;
        //        var scale = bitmap.HorizontalResolution / DPI;
        //        var transform = new ScaleTransform(scale, scale);

        //        TransformedBitmap transformedBitmap = new TransformedBitmap();
        //        transformedBitmap.BeginInit();
        //        transformedBitmap.Source = bitmapImage;
        //        transformedBitmap.Transform = transform;
        //        transformedBitmap.EndInit();

        //        return transformedBitmap;
        //    }
        //}

        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);

        //public static BitmapImage ConvertBitmapToSource(System.Drawing.Bitmap bitmap)
        //{
        //    IntPtr hBitmap = bitmap.GetHbitmap();
        //    BitmapImage retval;

        //    try
        //    {
        //        retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
        //                     hBitmap,
        //                     IntPtr.Zero,
        //                     Int32Rect.Empty,
        //                     BitmapSizeOptions.FromEmptyOptions());
        //    }
        //    finally
        //    {
        //        DeleteObject(hBitmap);
        //    }

        //    return retval;
        //}

        public static BitmapSource ConvertBitmapToSource(System.Drawing.Bitmap bitmap)
        {
            var _return = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
            //RenderOptions.SetBitmapScalingMode(_return, BitmapScalingMode.Unspecified);
            return _return;
        }
    }
}
