using System;
using System.Runtime.Serialization;

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
