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

namespace gk3
{
    static class BitmapConverter
    {
        //public static BitmapImage ConvertBitmapToSource(System.Drawing.Bitmap bitmap)
        //{
        //    using (MemoryStream memory = new MemoryStream())
        //    {
        //        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        //        memory.Position = 0;
        //        BitmapImage bitmapImage = new BitmapImage();
        //        bitmapImage.BeginInit();
        //        memory.Seek(0, SeekOrigin.Begin);
        //        bitmapImage.StreamSource = memory;
        //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapImage.EndInit();
        //        bitmapImage.Freeze();

        //        return bitmapImage;
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
