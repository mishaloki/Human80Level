using System;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Human80Level.Ability.Endurance
{
    [DataContract(Name = "GpsData", Namespace = "Human80Level.Ability.Endurance")]
    public class GpsData
    {
        [DataMember(Name = "TotalDistance")]
        public double TotalDistance { get; set; }

        [DataMember(Name = "TotalTime")]
        public TimeSpan TotalTime { get; set; }

        [DataMember(Name = "AvgSpeed")]
        public double AvgSpeed { get; set; }

        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }

        public GpsData(double distance, TimeSpan time, double speed)
        {
            this.TotalDistance = distance;
            this.TotalTime = time;
            this.AvgSpeed = speed;
            this.Date = DateTime.Now;
        }
    }
}
