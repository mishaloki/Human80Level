using System;
using System.IO.IsolatedStorage;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace Human80Level.Ability.Power
{
    public class PowerManager
    {
        private static Accelerometer _accelSensor;

        private static Vector3 _accelReading;

        private static Vector3 _previousValue;

        public static bool AccelActive;
        
        private static PowerResult _totalReuslt;

        private const string ResultSetting = "PowerResult";

        private const string tileUri = "/Images/Ability/Power/tile.png";

        private static PowerResult _currentResult;

        public static event Action<object ,EventArgs> NewAbs;

        private static bool _isOdd = true;

        public static void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
              CountAbs(e);
        }

        private static void CountAbs(AccelerometerReadingEventArgs e)
        {
            try
            {
                _accelReading.X = (float)e.X;
                _accelReading.Y = (float)e.Y;
                _accelReading.Z = (float)e.Z;

                if ((Math.Abs(_previousValue.Z - _accelReading.Z) > 0.5))
                {
                    _isOdd = !_isOdd;
                    _previousValue = _accelReading;
                    if (_isOdd)
                    {
                        return;
                    }
                    _currentResult.Abs += 1;
                    if (NewAbs != null)
                    {
                        NewAbs(_accelReading.Z - _previousValue.Z, null);
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Error("CountAbs", err.Message);
            }         
        }

        public static void PrepareAccel()
        {
            try
            {
                _accelSensor = new Accelerometer();
                _accelSensor.ReadingChanged += AccelerometerReadingChanged;
                _accelSensor.TimeBetweenUpdates = new TimeSpan(0, 0, 0, 0, 100);
            }
            catch (Exception err)
            {
                Logger.Error("PrepareAccel", err.Message);
            }

        }

        public static void StartAccel()
        {
            try
            {
                _accelSensor.Start();
                _currentResult = new PowerResult();
                AccelActive = true;
            }
            catch (AccelerometerFailedException err)
            {
                // the accelerometer couldn't be started.  No fun!
                Logger.Error("StartAccel", err.Message);
                AccelActive = false;
            }
            catch (UnauthorizedAccessException err)
            {
                // This exception is thrown in the emulator-which doesn't support an accelerometer.
                Logger.Error("StartAccel", err.Message);
                AccelActive = false;
            }
        }

        public static void StopAccel()
        {
            try
            {
                if (AccelActive)
                {
                    try
                    {
                        _accelSensor.Stop();
                    }
                    catch (AccelerometerFailedException err)
                    {
                        // the accelerometer couldn't be stopped now.
                        Logger.Error("StopAccel", err.Message);
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Error("StopAccel", err.Message);
            }

        }

        public static void ExtractResult()
        {
            try
            {
                _totalReuslt = new PowerResult();
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ResultSetting))
                {
                    _totalReuslt = settings[ResultSetting] as PowerResult;
                    Logger.Info("ExtractResult", "Abs result was got from settings");
                }
            }
            catch (Exception err)
            {
                Logger.Error("ExtractResult", err.Message);
            }
        }

        public static void SaveResults()
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ResultSetting))
                {
                    settings.Remove(ResultSetting);
                }
                if (_totalReuslt.Date.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    _totalReuslt = new PowerResult();
                    _totalReuslt.Date = DateTime.Now;
                }
                _totalReuslt.Abs += _currentResult.Abs;
                settings.Add(ResultSetting, _totalReuslt);
                settings.Save();
                TileManager.UpdateTile(AppResources.AbListBtnPower,GetValue(),tileUri);
                Logger.Info("SaveResults", "Abs result was saved");
            }
            catch (Exception err)
            {
                Logger.Error("SaveResults", err.Message);
            }
        }

        public static double GetTotalAbs()
        {
            return _totalReuslt.Abs;
        }


        public static double GetCurrentAbs()
        {
            return _currentResult.Abs;
        }

        public static double GetValue()
        {
            double dif = _totalReuslt.Abs;
            if (dif < 0)
            {
                dif = 0;
            }
            else
            {
                if (dif > 300)
                {
                    dif = 300;
                }
            }
            return 100 * dif / 300;
        }

        public static int GetLevel()
        {
            double dif = _totalReuslt.Abs;
            int level;
            if (dif < 50)
            {
                level = 0;
            }
            else
            {
                if (dif < 100)
                {
                    level = 1;
                }
                else
                {
                    if (dif < 200)
                    {
                        level = 2;
                    }
                    else
                    {
                        if (dif < 300)
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
