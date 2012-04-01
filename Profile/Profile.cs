using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Human80Level.Profile
{
    public class Profile
    {
        public string NickName { get; set; }

        public string AvatarUri { get; set; }

        public double CurrentLevel { get; set; }

        public bool IsProgress { get; set; }

        public double Delta { get; set; }

        public Profile (string nick, string avatar, double level, bool isProgres, double delta)
        {
            this.NickName = nick;
            this.AvatarUri = avatar;
            this.CurrentLevel = level;
            this.IsProgress = isProgres;
            this.Delta = delta;
        }
    }
}
