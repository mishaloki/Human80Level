using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Human80Level.Utils
{
    public static class StorageManager
    {
        private const string isoFolder = "CapturedImagesCache";

        private const string ImageName = "avatar.jpeg";
        
        public static BitmapImage GetImageFromStorage(string imageUri)
        {
            BitmapImage image = new BitmapImage();

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(imageUri, FileMode.Open, FileAccess.Read))
                {
                    image.SetSource(fileStream);
                    return image;
                }
            }
        }

        public static string SaveImageToStorage(WriteableBitmap image)
        {

            if (image == null)
            {
                throw new ArgumentNullException("imageBytes");
            }
            try
            {


                var isoFile = IsolatedStorageFile.GetUserStoreForApplication();

                if (!isoFile.DirectoryExists(isoFolder))
                {
                    isoFile.CreateDirectory(isoFolder);
                }

                string filePath = System.IO.Path.Combine("/" + isoFolder + "/", ImageName);

                using (var stream = isoFile.CreateFile(filePath))
                {
                    image.SaveJpeg(stream, image.PixelWidth, image.PixelHeight, 0, 100);
                }

                return new Uri(filePath, UriKind.Relative).ToString();
            }
            catch (Exception e)
            {
                //TODO: log or do something else
                throw;
            }
        }
    }
}
