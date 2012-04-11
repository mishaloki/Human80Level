using System;
using Microsoft.Phone.Controls;

namespace Human80Level.Utils
{
    public class Navigator
    {
        public static void NavigateTo(PhoneApplicationPage pageFrom,string pageTo)
        {
            try
            {
                pageFrom.NavigationService.Navigate(new Uri(pageTo, UriKind.Relative));
                Logger.Info("Navigate to",pageTo);
            }
            catch (Exception err)
            {
                Logger.Error("NavigateTo",err.Message);
            }
            
        }
    }
}
