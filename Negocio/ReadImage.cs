using System;
using System.Drawing;
using ZXing;
using System.IO;
using Persistencia;

namespace Negocio
{
    public class ReadImage
    {
        public string GetBarCodeFromToTheImage(string ubImage)
        {
            // create a barcode reader instance
            var barcodeReader = new BarcodeReader(); 
            barcodeReader.Options.TryHarder = true; 
        
            // create an in memory bitmap
            //var barcodeBitmap = (Bitmap)(image);
            var barcodeBitmap = (Bitmap)Bitmap.FromFile(ubImage); 
        
            // decode the barcode from the in memory bitmap
            var barcodeResult = barcodeReader.Decode(barcodeBitmap); 
        
            // Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");

            return Convert.ToString(barcodeResult?.Text); 
        }

        public bool BarCodeExist (string BarCode)
        {
            return new ProductRepo().DoesTheProductExists(BarCode);
        }
    }
}