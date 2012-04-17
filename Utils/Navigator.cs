using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;

namespace Human80Level.Utils
{
    public class Navigator
    {
        public  const string AboutUri = "/PageAbout.xaml";
         
        public  const string StatUri = "/Statistics/PageStatistics.xaml";
         
        public  const string HelpUri = "/PageHelp.xaml";
         
        public  const string StartUri = "/PageAbilityList.xaml";
         
        public  const string ProfileUri = "/Profile/PageProfile.xaml";
         
        public  const string FromPage = "fromPage";

        public static List<string> pages = new List<string>() { "main","profile", "statistics", "abilities", "power", "endurance", "intelligence", "physique", "luck" };

        public static void NavigateTo(PhoneApplicationPage pageFrom,string pageTo)
        {
            try
            {
                pageFrom.NavigationService.Navigate(new Uri(pageTo+"?"+FromPage+"="+pageFrom.Title, UriKind.Relative));
                Logger.Info("Navigate to",pageTo);
            }
            catch (Exception err)
            {
                Logger.Error("NavigateTo",err.Message);
            }
            
        }
    }
}
