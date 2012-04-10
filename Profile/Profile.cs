using System;
using System.Runtime.Serialization;

namespace Human80Level.Profile
{
    [DataContract(Name = "Profile", Namespace = "Human80Level.Profile")]
    public class Profile
    {
        [DataMember(Name = "NickName")]
        public string NickName { get; set; }

        [DataMember(Name = "AvatarUri")]
        public string AvatarUri { get; set; }

        [DataMember(Name = "CurrentLevel")]
        public double CurrentLevel { get; set; }

        [DataMember(Name = "IsProgress")]
        public bool IsProgress { get; set; }

        [DataMember(Name = "Delta")]
        public double Delta { get; set; }

        [DataMember(Name = "Birth")]
        public DateTime Birth { get; set; }

        [DataMember(Name = "Heigth")]
        public double Heigth { get; set; }

        /// <summary>
        /// Profile constructor
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="avatar"></param>
        /// <param name="level"></param>
        /// <param name="isProgres"></param>
        /// <param name="delta"></param>
        /// <param name="birth"></param>
        /// <param name="height"></param>
        public Profile (string nick, string avatar, double level, bool isProgres, double delta, DateTime birth, double height)
        {
            this.NickName = nick;
            this.AvatarUri = avatar;
            this.CurrentLevel = level;
            this.IsProgress = isProgres;
            this.Delta = delta;
            this.Birth = birth;
            this.Heigth = height;
        }
    }
}
