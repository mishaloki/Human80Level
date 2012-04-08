using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Resources;

namespace Human80Level.Statistics
{
    public class IndicatorManager
    {
        private static List<Indicator> indicators;

        private static string powerBeginIconUri = "/Images/Ability/Luck/loser.png";

        private static string powerEndIconUri = "";

        private static string powerDescript = "";

        private static string [] powerLevels = new string[] {""};

        private static string luckBeginIconUri = "/Images/Ability/Luck/loser.png";

        private static string luckEndIconUri = "/Images/Ability/Luck/lucky.png";

        private static string luckDescript = "";

        private static string[] luckLevels = new string[] { "" };

        public static List<Indicator> GetIndicators()
        {
            return indicators;
        }

        public static void CreateIndicators()
        {
            indicators = new List<Indicator>();

            string title = AppResources.AbListBtnPower;

            Indicator power = new Indicator(title, powerDescript, powerBeginIconUri, powerEndIconUri, powerLevels, GetCurrentLevel(title),GetCurrentValue(title));

            title = AppResources.AbListBtnLuck;
            Indicator luck = new Indicator(title, luckDescript, luckBeginIconUri, luckEndIconUri, luckLevels, GetCurrentLevel(title), GetCurrentValue(title));

            indicators.Add(power);
            indicators.Add(luck);
        }

        private static int GetCurrentLevel(string title)
        {
            return 0;
        }

        private static int GetCurrentValue(string title)
        {
            return 0;
        }
    }
}
