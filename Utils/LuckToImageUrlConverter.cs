using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Human80Level.Utils
{
    public class LuckToImageUrlConverter : IValueConverter
    {
        private const string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private const string TrashImageUrl = "/Images/Ability/Luck/trash.png";
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return CloverImageUrl;
            }
            return TrashImageUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
