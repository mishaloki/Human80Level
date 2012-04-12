using System;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Human80Level.Ability.Physique
{
    public class PhysiqueManager
    {
        public static double GetValue()
        {
            double dif = GetDifference();
            if (dif < 0)
            {
                dif = 0;
            }
            else
            {
                if (dif > 30)
                {
                    dif = 30;
                }
            }
            return 50-50*dif/30;
        }

        public static int GetLevel()
        {
            double dif = GetDifference();
            int level = 5;
            if (dif < 1)
            {
                level = 4;
            }
            else
            {
                if (dif < 10)
                {
                    level = 3;
                }
                else
                {
                    if (dif < 20)
                    {
                        level = 2;
                    }
                    else
                    {
                        if (dif < 30)
                        {
                            level = 1;
                        }
                        else
                        {
                            level = 0;
                        }
                    }
                }
            }

            return level;
        }

        private static double GetDifference()
        {
            try
            {
                Profile.Profile profile = ProfileManager.GetProfile();
                return Math.Abs(profile.Weight-GetIdealWeight(profile.Weight));
            }
            catch (Exception err)
            {
                Logger.Error("GetDifference", err.Message);
                return 0;
            }

        }

        public static double GetIdealWeight(double currentWeight)
        {
            Profile.Profile profile = ProfileManager.GetProfile();
            int age = DateTime.Now.Year - profile.Birth.Year;
            double ideal = 50 + 0.75 * (profile.Heigth - 150) + (age - 20) / 5;
            return ideal;
        }
    }
}
