using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Human80Level.Ability.Endurance
{
    public class EnduranceManager
    {
        public static bool IsGpsAvailabel()
        {
            return true;
        }

        public static void SaveResults(double distancem, TimeSpan time, double speed)
        {
            
        }

        public static double GetTotalDistance()
        {
            return 0;
        }

        public static TimeSpan GetTotalTime()
        {
            return new TimeSpan();
        }

        public static double GetAvgSpeed()
        {
            return 0;
        }
    }
}
