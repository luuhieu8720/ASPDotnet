using AspNetCoreApplication.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Extensions
{
    public static class ImageExts
    {
        public static byte[] ImageToByteArray(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public static Image EnsureLimitSize(this Image image, int heightLimit, int widthLimit)
        {
            if (image.Width < widthLimit && image.Height < heightLimit)
            {
                return image;
            }

            var scaleWidth = image.Width * 1.0 / widthLimit;
            var scaleHeight = image.Height * 1.0 / heightLimit;

            var scale = Math.Max(scaleWidth, scaleHeight);

            return new Bitmap(image, (int)(image.Width / scale), (int)(image.Height / scale));
        }
    }
}
