using System;
using System.Device.Location;
using System.IO.IsolatedStorage;
using Human80Level.Resources;
using Human80Level.Utils;

namespace Human80Level.Ability.Endurance
{
    public static class EnduranceManager
    {
        #region private fields

        private const string AppSettingsGpsName = "GpsData";

        private const string tileUri = "/Images/Ability/Endurance/tile.png";

        private static GpsData todaysRusult;

        private static GpsData currentResult;

        private static GeoCoordinateWatcher watcherGps;

        private static bool isGpsAbailable;

        private static GeoCoordinate previousPosition;

        private static DateTime startTime;

        private static bool needToCount;

        #endregion

        #region saving/extracting endurance data

        public static void SaveResults()
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(AppSettingsGpsName))
                {
                    settings.Remove(AppSettingsGpsName);
                }
                if (todaysRusult.Date.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    todaysRusult = new GpsData(0,new TimeSpan(0,0,0,0),0);
                }
                todaysRusult.TotalDistance += currentResult.TotalDistance;
                todaysRusult.TotalTime += currentResult.TotalTime;
                //todo check if additional check is needed
                if (todaysRusult.TotalTime.TotalHours != 0)
                {
                    todaysRusult.AvgSpeed = (todaysRusult.TotalDistance / 1000) / todaysRusult.TotalTime.TotalHours;
                }
                
                settings.Add(AppSettingsGpsName, todaysRusult);
                settings.Save();
                TileManager.UpdateTile(AppResources.AbLisgBtnEndurance,GetValue(),tileUri);
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
                todaysRusult = new GpsData(0, new TimeSpan(), 0);
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(AppSettingsGpsName))
                {
                    todaysRusult = settings[AppSettingsGpsName] as GpsData;
                    Logger.Info("ExtractResult", "GPS data was got from settings");
                }
            }
            catch (Exception err)
            {               
                Logger.Error("ExtractResult", err.Message);
            }
        }

        #endregion

        #region getting/setting endurance data

        public static double GetTotalDistance()
        {
            return todaysRusult.TotalDistance;
        }

        public static TimeSpan GetTotalTime()
        {
            return todaysRusult.TotalTime;
        }

        public static double GetAvgSpeed()
        {
            return todaysRusult.AvgSpeed;
        }

        public static double GetCurrentDistance()
        {
            return currentResult.TotalDistance;
        }

        public static TimeSpan GetCurrentTime()
        {
            return (DateTime.Now - startTime);
        }

        public static double GetCurrentSpeed()
        {
            return currentResult.AvgSpeed;
        }

        #endregion

        #region GPS accessors

        public static void StartGps()
        {
            try
            {
                if (watcherGps == null)
                {
                    watcherGps = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    //todo check if next string is needed
                    //watcher.MovementThreshold = 1;
                    watcherGps.StatusChanged += StatusListener;
                    watcherGps.PositionChanged += PositionListener;
                    currentResult = new GpsData(0, new TimeSpan(0, 0, 0, 0), 0);
                    startTime = DateTime.Now;
                    watcherGps.Start();
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
                if (watcherGps != null)
                {
                    watcherGps.Stop();
                    Logger.Info("StartGps", "GPS was stopped");
                }
            }
            catch (Exception err)
            {
                Logger.Error("StopGps", err.Message);
            }

        }

        public static bool IsGpsAvailabel()
        {
            return isGpsAbailable;
        }

        public static void StartCount()
        {
            needToCount = true;
        }

        public static void StopCount()
        {
            needToCount = false;
        }
        #endregion

        #region GPS listeners
        
        private static void StatusListener(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see whether the user has disabled the Location Service.
                    if (watcherGps.Permission == GeoPositionPermission.Denied)
                    {
                        // The user has disabled the Location Service on their device.
                        isGpsAbailable = false;
                    }
                    else
                    {
                        isGpsAbailable = false;
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    // Disable the Start Location button.
                    isGpsAbailable = false;
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    // Alert the user and enable the Stop Location button.
                    isGpsAbailable = false;
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    // Show the current position and enable the Stop Location button.
                    isGpsAbailable = true;
                    break;
            }
        }

        private static void PositionListener(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            try
            {
                if (previousPosition == null)
                {
                    previousPosition = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
                }

                if (!needToCount)
                {
                    return;
                }
                //todo check why sometimes getDistanceTo returns incorrect values
                GeoCoordinate current = e.Position.Location;
                currentResult.TotalDistance += e.Position.Location.GetDistanceTo(previousPosition);
                previousPosition.Latitude = current.Latitude;
                previousPosition.Longitude = current.Longitude;
            }
            catch (Exception err)
            {
                Logger.Error("PositionListener", err.Message);
            }

        }
        #endregion

        #region statistics methods

        public static double GetValue()
        {
            double dif = todaysRusult.TotalDistance;
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
            double dif = todaysRusult.TotalDistance;
            int level;
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
        #endregion
    }
}
