using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnNewProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileManager.getProfile() != null )
            {
                //TODO replace all hardcode text with constants
                bool createNew =
                    (MessageBox.Show("All previous result will be removed. Are you sure?", "Confirm removing profile",
                                     MessageBoxButton.OKCancel) == MessageBoxResult.OK);
                if (!createNew)
                {
                    return;
                }

                this.NavigationService.Navigate(new Uri("/PageProfile.xaml",UriKind.Relative));
            }
        }

        private void btnStartFlow_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/PageAbilityList.xaml", UriKind.Relative));
        }
    }
}