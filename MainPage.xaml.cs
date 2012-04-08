using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
            try
            {
                if (ProfileManager.getProfile() != null)
                {
                    //TODO replace all hardcode text with constants
                    bool createNew =
                        (MessageBox.Show("All previous result will be removed. Are you sure?", "Confirm removing profile",
                                         MessageBoxButton.OKCancel) == MessageBoxResult.OK);
                    if (!createNew)
                    {
                        return;
                    }

                    this.NavigationService.Navigate(new Uri("/PageProfile.xaml?new=true", UriKind.Relative));
                }
            }
            catch (Exception err)
            {
                Logger.Error("btnNewProfile_Click", err.Message);
            }
            
        }

        private void btnStartFlow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/PageAbilityList.xaml", UriKind.Relative));
            }
            catch (Exception err)
            {
                Logger.Error("btnStartFlow_Click", err.Message);
            }
            
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/PageAbout.xaml", UriKind.Relative));
            }
            catch (Exception err)
            {
                Logger.Error("btnAbout_Click", err.Message);
            }
            
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/PageHelp.xaml", UriKind.Relative));
            }
            catch (Exception err)
            {
                Logger.Error("btnHelp_Click", err.Message);
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowProfileInfor();
        }

        private void ShowProfileInfor()
        {
            try
            {
                Profile.Profile profile = ProfileManager.getProfile();
                if (profile == null)
                {
                    Logger.Info("gridProfile_Tap", "Profile == null");
                    return;
                }
                textNickName.Text = profile.NickName;
                textProgress.Text = profile.CurrentLevel.ToString();
                if (!string.IsNullOrEmpty(profile.AvatarUri))
                {
                    imgAvatar.Source = StorageManager.GetImageFromStorage(profile.AvatarUri);
                }  
            }
            catch (Exception err)
            {
                Logger.Error("gridProfile_Tap", err.Message);
            }
                     
        }

        private void gridProfile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/PageProfile.xaml", UriKind.Relative));
            }
            catch (Exception err)
            {
                Logger.Error("gridProfile_Tap", err.Message);
            }
            
        }
    }
}