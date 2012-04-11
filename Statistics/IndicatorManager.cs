using System;
using System.Collections.Generic;
using Human80Level.Ability.Luck;
using Human80Level.Resources;
using Human80Level.Utils;

namespace Human80Level.Statistics
{
    public class IndicatorManager
    {       
        private static List<Indicator> indicators;

        private static string powerBeginIconUri = "/Images/Ability/Luck/loser.png";

        private static string powerEndIconUri = "";

        private static string powerDescript = "";

        private static string [] powerLevels = new string[] { AppResources.APower1, AppResources.APower2, AppResources.APower3, AppResources.APower4, AppResources.APower5 };

        private static string luckBeginIconUri = "/Images/Ability/Luck/loser.png";

        private static string luckEndIconUri = "/Images/Ability/Luck/lucky.png";

        private static string luckDescript = AppResources.ALuckD;

        private static string[] luckLevels = new string[5] { AppResources.ALuckL1, AppResources.ALuckL2, AppResources.ALuckL3, AppResources.ALuckL4, AppResources.ALuckL5 };

        /// <summary>
        /// Gets list of all indicators
        /// </summary>
        /// <returns></returns>
        public static List<Indicator> GetIndicators()
        {
            return indicators;
        }

        /// <summary>
        /// Creates indicators for all abilities
        /// </summary>
        public static void CreateIndicators()
        {
            indicators = new List<Indicator>();

            string title = AppResources.AbListBtnPower;

            Indicator power = new Indicator(title, powerDescript, powerBeginIconUri, powerEndIconUri, powerLevels, GetCurrentLevel("power"),GetCurrentValue("power"));

            title = AppResources.AbListBtnLuck;
            Indicator luck = new Indicator(title, luckDescript, luckBeginIconUri, luckEndIconUri, luckLevels, GetCurrentLevel("luck"), GetCurrentValue("luck"));

            indicators.Add(power);
            indicators.Add(luck);
        }

        /// <summary>
        /// Gets current indicator level
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private static int GetCurrentLevel(string title)
        {
            try
            {
                int level = 0;
                switch (title)
                {
                    case "luck":
                        level = LuckEventManager.GetLevel();
                        break;
                }
                return level;
            }
            catch (Exception err)
            {
                Logger.Error("GetCurrentLevel", err.Message);
                return 0;
            }
        }

        /// <summary>
        /// Gets current indicator value
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private static int GetCurrentValue(string title)
        {
            try
            {
                int value = 0;
                switch (title)
                {
                    case "luck":
                        value = LuckEventManager.GetValue();
                        break;
                }
                return value;
            }
            catch (Exception err)
            {
                Logger.Error("GetCurrentValue",err.Message); 
                return 0;
            }
        }       
    }
}
