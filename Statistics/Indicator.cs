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

namespace Human80Level.Statistics
{
    public class Indicator
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string BeginIconUri { get; set; }

        public string EndIconUri { get; set; }

        public string[] Levels { get; set; }

        public string CurrentLevel { get; set; }

        public int CurrentValue { get; set; }

        public Indicator(string title, string description, string beginIconUri, string endIconUri, string [] levels,int currentLevel, int currentValue)
        {
            this.Title = title;
            this.Description = description;
            this.BeginIconUri = beginIconUri;
            this.EndIconUri = endIconUri;
            this.Levels = levels;
            this.CurrentLevel = this.Levels[currentLevel];
            this.CurrentValue = currentValue;
        }
    }
}
