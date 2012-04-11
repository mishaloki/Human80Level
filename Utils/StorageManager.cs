using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace Human80Level.Utils
{
    public static class StorageManager
    {
        private const string ImageFolder = "Images";

        private const string ImageName = "avatar.jpeg";
        
        /// <summary>
        /// Gets image from Isolated storage
        /// </summary>
        /// <param name="imageUri"></param>
        /// <returns></returns>
        public static BitmapImage GetImageFromStorage(string imageUri)
        {
            try
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
            catch (Exception err)
            {
                Logger.Error("GetImageFromStorage", err.Message);
                return null;
            }
        }

        /// <summary>
        /// Saves image to Isolated Storage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string SaveImageToStorage(WriteableBitmap image)
        {
            try
            {
                var isoFile = IsolatedStorageFile.GetUserStoreForApplication();

                if (!isoFile.DirectoryExists(ImageFolder))
                {
                    isoFile.CreateDirectory(ImageFolder);
                }

                string filePath = System.IO.Path.Combine("/" + ImageFolder + "/", ImageName);

                using (var stream = isoFile.CreateFile(filePath))
                {
                    image.SaveJpeg(stream, image.PixelWidth, image.PixelHeight, 0, 100);
                }

                return new Uri(filePath, UriKind.Relative).ToString();
            }
            catch (Exception err)
            {
                Logger.Error("SaveImageToStorage", err.Message);
                return null;
            }
        }
    }
}
