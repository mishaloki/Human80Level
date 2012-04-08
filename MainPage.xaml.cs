using System;
using System.Windows;
using System.Windows.Navigation;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly string MBnewProfileTitle = AppResources.MainPageMainPageMBnewProfileTitle;

        private readonly string MBnewProfileMessage = AppResources.MainPageMainPageMBnewProfileMessage;
        
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            ProfileManager.ExtractProfileFromSettings();
        }



        private void btnNewProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProfileManager.GetProfile() != null)
                {
                    //TODO replace all hardcode text with constants
                    bool createNew =
                        (MessageBox.Show(MBnewProfileMessage, MBnewProfileTitle,
                                         MessageBoxButton.OKCancel) == MessageBoxResult.OK);
                    if (!createNew)
                    {
                        return;
                    }                    
                }
                this.NavigationService.Navigate(new Uri("/PageProfile.xaml?new=true", UriKind.Relative));
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
                Profile.Profile profile = ProfileManager.GetProfile();
                if (profile == null)
                {
                    imgAvatar.Source = null;
                    textNickName.Text = string.Empty;
                    textProgress.Text = string.Empty;
                    imgStatus.Source = null;
                    SetStartFlowBtnState(false);
                    Logger.Info("gridProfile_Tap", "Profile == null");
                    return;
                }
                textNickName.Text = profile.NickName;
                textProgress.Text = profile.CurrentLevel.ToString();
                if (!string.IsNullOrEmpty(profile.AvatarUri))
                {
                    imgAvatar.Source = StorageManager.GetImageFromStorage(profile.AvatarUri);
                }  
                SetStartFlowBtnState(true);
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
                if (ProfileManager.GetProfile()!=null)
                {
                    this.NavigationService.Navigate(new Uri("/PageProfile.xaml", UriKind.Relative));
                }               
            }
            catch (Exception err)
            {
                Logger.Error("gridProfile_Tap", err.Message);
            }
            
        }

        private void SetStartFlowBtnState(bool isProfileExist)
        {
            if (isProfileExist)
            {
                btnStartFlow.IsEnabled = true;
            }
            else
            {
                btnStartFlow.IsEnabled = false;
            }
        }
    }
}