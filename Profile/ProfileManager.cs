using System;
using System.IO.IsolatedStorage;
using Human80Level.Database;

namespace Human80Level.Utils
{
    public static class ProfileManager
    {
        /// <summary>
        /// Profile setting name
        /// </summary>
        private const string ProfileSettingsName = "Profile";

        private static Profile.Profile CurrentProfile;
        
        /// <summary>
        /// Gets profile from app settings
        /// </summary>
        /// <returns>profile if it exists or null in other case</returns>
        public static Human80Level.Profile.Profile GetProfile()
        {
            return CurrentProfile;
        }


        public static void ExtractProfileFromSettings()
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ProfileSettingsName))
                {
                    CurrentProfile = settings[ProfileSettingsName] as Profile.Profile;
                    Logger.Info("GetProfile", "Profile was got from settings");
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetProfile", err.Message);
            }
        }

        /// <summary>
        /// Updates profile info in app settings
        /// </summary>
        /// <param name="profile"></param>
        public static void UpdateProfile (Profile.Profile profile)
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ProfileSettingsName))
                {
                    settings.Remove(ProfileSettingsName);
                }
                settings.Add(ProfileSettingsName, profile);
                settings.Save();
                CurrentProfile = profile;
                DBHelper.CreateDatabase();
                Logger.Info("UpdateProfile", "Profile was updated");
            }
            catch (Exception err)
            {
                Logger.Error("UpdateProfile", err.Message);
            }

        }

        /// <summary>
        /// Removes profile from app settings and removes database with statistics
        /// </summary>
        /// <returns></returns>
        public static bool RemoveProfile()
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(ProfileSettingsName))
                {
                    settings.Remove(ProfileSettingsName);
                    settings.Save();
                    DBHelper.DeleteDatabase();                    
                    CurrentProfile = null;                    
                    Logger.Info("RemoveProfile", "Profile was deleted");
                    return true;
                }
                Logger.Info("RemoveProfile", "Profile doesn't exist");
                return false;
            }
            catch (Exception err)
            {
                Logger.Error("RemoveProfile", err.Message);
                return false;
            }
        }
    }
}
