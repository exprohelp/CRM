using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;


namespace ChandanCRM.App_Start
{
    public class BarcodeGenerator
    {
        public static string GenerateBarCode(string barcode, int w, int h)
        {
            string BarcodeImage = string.Empty;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img;
            int W = Convert.ToInt32(w);
            int H = Convert.ToInt32(h);
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
            type = BarcodeLib.TYPE.CODE128A;
            try
            {
                if (type != BarcodeLib.TYPE.UNSPECIFIED)
                {
                    b.IncludeLabel = false;
                    //b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), this.cbRotateFlip.SelectedItem.ToString(), true);
                    b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                    img = b.Encode(type, barcode, Color.Black, Color.White, W, H);
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    b.Encode(type, barcode, Color.Black, Color.White, W, H).Save(memoryStream, ImageFormat.Png);
                    byte[] byteImage = memoryStream.ToArray();
                    Convert.ToBase64String(byteImage);
                    BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }
            catch (Exception ex)
            {
                BarcodeImage = ex.Message;
            }
            return BarcodeImage;
        }
    }
}