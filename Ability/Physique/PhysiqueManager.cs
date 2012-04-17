using System;
using Human80Level.Utils;

namespace Human80Level.Ability.Physique
{
    public class PhysiqueManager
    {
        public static double GetIdealWeight(double currentWeight)
        {
            Profile.Profile profile = ProfileManager.GetProfile();
            int age = DateTime.Now.Year - profile.Birth.Year;
            //todo check warning
            double ideal = 50 + 0.75 * (profile.Heigth - 150) + (age - 20) / 5;
            return ideal;
        }

        #region statistics methods

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
            return 100-100*dif/30;
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

        #endregion

    }
}
