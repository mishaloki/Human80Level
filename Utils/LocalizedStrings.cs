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
using Human80Level.Resources;
using Human80Level;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Human80Level.Utils
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return localizedResources; } }

        public static void LocalizeHelpBtn(PhoneApplicationPage page)
        {
            try
            {
                ApplicationBar appBar = (ApplicationBar)page.ApplicationBar;
                if (appBar == null)
                {
                    return;
                }
                ApplicationBarIconButton btnHelp = ((ApplicationBarIconButton)appBar.Buttons[0]);
                if (btnHelp == null)
                {
                    return;
                }
                btnHelp.Text = AppResources.CommonBtnHelp;
            }
            catch (Exception err)
            {
                Logger.Error("LocalizeHelpBtn", err.Message);
            }

        }
    }


}
