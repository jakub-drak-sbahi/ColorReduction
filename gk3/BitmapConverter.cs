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
        public static BitmapSource ConvertBitmapToSource(System.Drawing.Bitmap bitmap)
        {
            var _return = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
            RenderOptions.SetBitmapScalingMode(_return, BitmapScalingMode.NearestNeighbor);
            return _return;
        }
    }
}
