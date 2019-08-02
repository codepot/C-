using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Ucla.Common.ImageUtility
{
    /// <summary>
    /// Utilities for manipulating images using Windows GDI+
    /// </summary>
    public class ImageLib
    {
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image img, ImageFormat imageFormat)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, imageFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static byte[] ResizeImageBytes(byte[] inputBytes, int maxWidth, int maxHeight)
        {
            return ResizeImageBytes(inputBytes, maxWidth, maxHeight, null);
        }

        /// <summary>
        /// Given a byte array containing an encoded image, decode the imaage, scale to fit
        /// inside a rect, and re-encode in PNG format.
        /// </summary>
        /// <param name="inputBytes">Original enncoded image (JPEG, Png, Bmp, Gif)</param>
        /// <param name="maxWidth">maximum width of scaled image in pixels</param>
        /// <param name="maxHeight">maximum height of scaled image in pixels</param>
        /// <param name="imageFormat">omit parameter to use same image format as original, or specify a value of the desired output format.</param>
        /// <returns>Scaled image bytes that fits within a maxWidth x maxHeight rect</returns>
        public static byte[] ResizeImageBytes(byte[] inputBytes, int maxWidth, int maxHeight, ImageFormat imageFormat)
        {
            Image inputImg = ByteArrayToImage(inputBytes);
            var scaledHeight = maxHeight;
            var scaledWidth = maxWidth;
            var inputRect = inputImg.Size;
            if ((double)maxWidth/maxHeight > (double)inputRect.Width / inputRect.Height)
            {   // Height determines scale factor
                scaledWidth = (int)(inputRect.Width * (double)scaledHeight / inputRect.Height);
            }
            else
            {   // Width determines scale factor
                scaledHeight = (int)(inputRect.Height * (double)scaledWidth / inputRect.Width);
            }

            var scaledImage = ResizeImage(inputImg, scaledWidth, scaledHeight);

            // Re-encode the resized image int the original format
            return ImageToByteArray(scaledImage, imageFormat ?? inputImg.RawFormat);
        }

    }
}
