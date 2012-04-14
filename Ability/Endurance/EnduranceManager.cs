using System;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Utils;

namespace Human80Level.Ability.Endurance
{
    public class EnduranceManager
    {
        private static GpsData totalReuslt;

        private const string ResultSetting = "GpsData";

        private static int current = 0;
        
        public static bool IsGpsAvailabel()
        {
            return true;
        }

        public static void SaveResults(double distancem, TimeSpan time, double speed)
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ResultSetting))
                {
                    settings.Remove(ResultSetting);
                }
                if (totalReuslt.Date.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    totalReuslt = new GpsData(0,new TimeSpan(0,0,0,0),0);
                }
                totalReuslt.TotalDistance += distancem;
                totalReuslt.TotalTime += time;
                totalReuslt.AvgSpeed = totalReuslt.TotalDistance/totalReuslt.TotalTime.TotalHours;
                settings.Add(ResultSetting, totalReuslt);
                settings.Save();

                Logger.Info("SaveResults", "GPS data was saved");
            }
            catch (Exception err)
            {
                Logger.Error("SaveResults", err.Message);
            }
        }

        public static void ExtractResult()
        {
            try
            {
                totalReuslt = new GpsData(0, new TimeSpan(), 0);
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ResultSetting))
                {
                    totalReuslt = settings[ResultSetting] as GpsData;
                    Logger.Info("ExtractResult", "GPS data was got from settings");
                }
            }
            catch (Exception err)
            {               
                Logger.Error("ExtractResult", err.Message);
            }
        }

        public static double GetTotalDistance()
        {
            return totalReuslt.TotalDistance;
        }

        public static TimeSpan GetTotalTime()
        {
            return totalReuslt.TotalTime;
        }

        public static double GetAvgSpeed()
        {
            return totalReuslt.AvgSpeed;
        }

        public static double GetCurrentDistance()
        {
            return ++current;
        }

        public static TimeSpan GetCurrentTime()
        {
            return new TimeSpan(0,0,current,0);
        }

        public static double GetCurrentSpeed()
        {
            return current*2;
        }

    }
}
