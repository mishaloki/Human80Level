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

namespace Human80Level.Ability.Power
{
    [DataContract(Name = "PowerResult", Namespace = "Human80Level.Ability.Power")]
    public class PowerResult
    {
        [DataMember(Name = "Abs")]
        public int Abs { get; set; }

        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
    }
}
