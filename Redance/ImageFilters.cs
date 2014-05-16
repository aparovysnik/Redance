using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Imaging.Filters;
using AForge;
using System.Drawing;
using System.Drawing.Imaging;

namespace Redance
{
    class ImageFilters
    {
        /// <summary>
        /// Limits the range of colors present on the given image.
        /// </summary>
        /// <param name="r">Range of R channel</param>
        /// <param name="g">Range of G channel</param>
        /// <param name="b">Range of B channel</param>
        /// <param name="image">Image to filter</param>
        public static void FilterColor(IntRange r, IntRange g, IntRange b, Bitmap image)
        {
            // create color filter
            ColorFiltering colorFilter = new ColorFiltering();
            // configure the filter to keep red object only
            colorFilter.Red = r;
            colorFilter.Green = g;
            colorFilter.Blue = b;
            // filter image
            colorFilter.ApplyInPlace(image);
        }

        /// <summary>
        /// Creates a copy of the given bitmap and draws the specified list of points on it using a default solid color
        /// </summary>
        /// <param name="bmp">Bitmap to draw upon</param>
        /// <param name="points">Points to draw</param>
        /// <returns>Bitmap with points drawn</returns>
        public static Bitmap Draw(Bitmap bmp, List<IntPoint> points)
        {
            lock (bmp)
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int size = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] bytes = new Byte[size];
                foreach (IntPoint point in points)
                {
                    bytes[point.Y * bmpData.Stride + point.X * 3] = 0;
                    bytes[point.Y * bmpData.Stride + point.X * 3 + 1] = 0;
                    bytes[point.Y * bmpData.Stride + point.X * 3 + 2] = 255;
                }
                System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bmpData.Scan0, size);
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }
    }
}
