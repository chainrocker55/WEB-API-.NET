using Gma.QrCodeNet.Encoding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Common.Extensions
{
    public static class TextBoxExtension
    {
        //public static String GetStringValueOrNull(this TextBox txt)
        //{

        //    if (!String.IsNullOrWhiteSpace(txt.Text))
        //        return txt.Text.Trim();

        //    return null;
        //}
        //public static String GetStringValueOrEmpty(this TextBox txt)
        //{

        //    if (!String.IsNullOrWhiteSpace(txt.Text))
        //        return txt.Text.Trim();

        //    return String.Empty;
        //}
        //public static bool IsNullOrEmpty(this TextBox txt)
        //{
        //    return String.IsNullOrEmpty(txt.Text);
        //}

        public static Image ToQRCode(this string txt)
        {
            try
            {
                QrEncoder Encoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode Code = Encoder.Encode(txt);
                Bitmap TempBMP = new Bitmap(Code.Matrix.Width, Code.Matrix.Height);
                for (int X = 0; X <= Code.Matrix.Width - 1; X++)
                {
                    for (int Y = 0; Y <= Code.Matrix.Height - 1; Y++)
                    {
                        if (Code.Matrix.InternalArray[X, Y])
                            TempBMP.SetPixel(X, Y, Color.Black);
                        else
                            TempBMP.SetPixel(X, Y, Color.White);
                    }
                }
                return (Image)TempBMP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] ToQRCodeByteArr(this string txt)
        {
            try
            {
                Image img = txt.ToQRCode();
                byte[] ba = ImageToByteArray(img);
                return ba;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
