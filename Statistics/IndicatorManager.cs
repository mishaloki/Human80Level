﻿using System;
using System.Collections.Generic;
using Human80Level.Ability.Luck;
using Human80Level.Ability.Physique;
using Human80Level.Resources;
using Human80Level.Utils;

namespace Human80Level.Statistics
{
    public class IndicatorManager
    {       
        private static List<Indicator> indicators;

        private static string powerBeginIconUri = "/Images/Ability/Power/weak.png";

        private static string powerEndIconUri = "/Images/Ability/Power/chuck.png";

        private static string powerDescript = "";

        private static string [] powerLevels = new string[] { AppResources.APower1, AppResources.APower2, AppResources.APower3, AppResources.APower4, AppResources.APower5 };

        private static string luckBeginIconUri = "/Images/Ability/Luck/loser.png";

        private static string luckEndIconUri = "/Images/Ability/Luck/lucky.png";

        private static string luckDescript = AppResources.ALuckD;

        private static string[] luckLevels = new string[5] { AppResources.ALuckL1, AppResources.ALuckL2, AppResources.ALuckL3, AppResources.ALuckL4, AppResources.ALuckL5 };


        private static string phyBeginIconUri = "/Images/Ability/Physique/fat.png";

        private static string phyEndIconUri = "/Images/Ability/Physique/slim.png";

        private static string phyDescript = AppResources.APhyD;

        private static string[] phyLevels = new string[5] { AppResources.APhyL1, AppResources.APhyL2, AppResources.APhyL3, AppResources.APhyL4, AppResources.APhyL5 };



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

            title = AppResources.AbListBtnPhysique;
            Indicator physique = new Indicator(title, phyDescript, phyBeginIconUri, phyEndIconUri, phyLevels, GetCurrentLevel("physique"), GetCurrentValue("physique"));

            indicators.Add(power);
            indicators.Add(physique);
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
                    case "physique":
                        level = PhysiqueManager.GetLevel();
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
        private static double GetCurrentValue(string title)
        {
            try
            {
                double value = 0;
                switch (title)
                {
                    case "luck":
                        value = LuckEventManager.GetValue();
                        break;
                    case "physique":
                        value = PhysiqueManager.GetValue();
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
