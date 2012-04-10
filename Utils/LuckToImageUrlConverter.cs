using System;
using System.Globalization;
using System.Windows.Data;

namespace Human80Level.Utils
{
    public class LuckToImageUrlConverter : IValueConverter
    {
        private const string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private const string TrashImageUrl = "/Images/Ability/Luck/trash.png";
        
        /// <summary>
        /// Converts bool value of isLuck field to Image Url
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                {
                    return CloverImageUrl;
                }
                return TrashImageUrl;
            }
            catch (Exception err)
            {
                Logger.Error("Convert", err.Message);
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
