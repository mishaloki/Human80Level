using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Human80Level.Utils
{
    public class CultureManager
    {
        public static bool IsRus()
        {
            return CultureInfo.CurrentCulture.Name=="ru-RU";
        }
    }
}
