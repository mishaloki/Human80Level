using System;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Profile;

namespace Human80Level.Utils
{
    public static class ProfileManager
    {
        private const string ProfileSettingsName = "Profile";
        
        public static Human80Level.Profile.Profile getProfile()
        {       
            string name = string.Empty;
            string avatar = string.Empty;
            double level = 0;
            double delta = 0;
            bool isProgress = true;

            Profile.Profile profile = new Human80Level.Profile.Profile(name,avatar,level,isProgress,delta);
            //TODO Implemented logic to check if profile already exist
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(ProfileSettingsName))
            {
                profile = settings[ProfileSettingsName] as Profile.Profile;
            }

            return profile;
        }

        public static void UpdateProfile (Profile.Profile profile)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(ProfileSettingsName))
            {
                settings.Remove(ProfileSettingsName);
            }
            settings.Add(ProfileSettingsName, profile);
            settings.Save();
        }
    }
}
