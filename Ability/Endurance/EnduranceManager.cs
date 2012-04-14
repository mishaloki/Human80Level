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
using Human80Level.Database;
using Human80Level.Utils;
using System.Device.Location;

namespace Human80Level.Ability.Endurance
{
    public class EnduranceManager
    {
        private static GpsData totalReuslt;

        private const string ResultSetting = "GpsData";

        private static int current = 0;

        private static GeoCoordinateWatcher watcher;

        private static bool IsGpsAbailable;

        private static GeoCoordinate PreviousPosition;

        private static GpsData CurrentResult;

        private static DateTime StartTime;

        public static void StartGps()
        {
            try
            {
                if (watcher == null)
                {
                    watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    //watcher.MovementThreshold = 20;
                    watcher.StatusChanged += StatusListener;
                    watcher.PositionChanged += PositionListener;
                    CurrentResult = new GpsData(0,new TimeSpan(0,0,0,0),0 );
                    StartTime = DateTime.Now;
                    watcher.Start();
                    Logger.Info("StartGps", "GPS was started");
                }
            }
            catch (Exception err)
            {
                Logger.Error("StartGps", err.Message);
            }  

        }

        public static void StopGps()
        {
            try
            {
                if (watcher != null)
                {
                    watcher.Stop();
                }
            }
            catch (Exception err)
            {
                Logger.Error("StopGps", err.Message);
            }           

        }

        private static void StatusListener(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see whether the user has disabled the Location Service.
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                        // The user has disabled the Location Service on their device.
                        IsGpsAbailable = false;
                    }
                    else
                    {
                        IsGpsAbailable = false;
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    // Disable the Start Location button.
                    IsGpsAbailable = false;
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    // Alert the user and enable the Stop Location button.
                    IsGpsAbailable = false;
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    // Show the current position and enable the Stop Location button.
                    IsGpsAbailable = true;
                    break;
            }
        }
        
        private static void PositionListener(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (PreviousPosition == null)
            {
                PreviousPosition = new GeoCoordinate(e.Position.Location.Latitude,e.Position.Location.Longitude);
            }
            CurrentResult.TotalDistance += e.Position.Location.GetDistanceTo(PreviousPosition);
            PreviousPosition.Latitude = e.Position.Location.Latitude;
            PreviousPosition.Longitude = e.Position.Location.Longitude;
        }

        public static bool IsGpsAvailabel()
        {
            return IsGpsAbailable;
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
            return CurrentResult.TotalDistance;
        }

        public static TimeSpan GetCurrentTime()
        {
            return (DateTime.Now - StartTime);
        }

        public static double GetCurrentSpeed()
        {
            return CurrentResult.AvgSpeed;
        }

        public static double GetValue()
        {
            double dif = totalReuslt.TotalDistance;
            if (dif < 0)
            {
                dif = 0;
            }
            else
            {
                if (dif > 42185)
                {
                    dif = 42185;
                }
            }
            return 100 * dif / 42185;
        }

        public static int GetLevel()
        {
            double dif = totalReuslt.TotalDistance;
            int level = 0;
            //todo replace
            if (dif < 4000)
            {
                level = 0;
            }
            else
            {
                if (dif < 10000)
                {
                    level = 1;
                }
                else
                {
                    if (dif < 20000)
                    {
                        level = 2;
                    }
                    else
                    {
                        if (dif < 42185)
                        {
                            level = 3;
                        }
                        else
                        {
                            level = 4;
                        }
                    }
                }
            }
            return level;
        }
    }
}
