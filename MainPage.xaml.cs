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

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/PageAbout.xaml", UriKind.Relative));
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/PageHelp.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowProfileInfor();
        }

        private void ShowProfileInfor()
        {
            Profile.Profile profile = ProfileManager.getProfile();
            textNickName.Text = profile.NickName;
            textProgress.Text = profile.CurrentLevel.ToString();
            if (!string.IsNullOrEmpty(profile.AvatarUri))
            {
                BitmapImage image = new BitmapImage(new Uri(profile.AvatarUri));

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(profile.AvatarUri, FileMode.Open, FileAccess.Read))
                    {
                        image.SetSource(fileStream);
                    }
                }

                imgAvatar.Source = image;
            }

            
        }
    }
}