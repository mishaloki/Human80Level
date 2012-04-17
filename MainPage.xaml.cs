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


        
        /// <summary>
        /// Main page constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ProfileManager.ExtractProfileFromSettings();
            Logger.Info("MainPage constructor","page was initialize, profile extracted");
            TileManager.UpdateTile();
        }

        /// <summary>
        /// New profile button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProfile_Click(object sender, RoutedEventArgs e)
        {
            this.CreateNewProfile();            
        }

        /// <summary>
        /// Navigates to profile page with parameter to create new profile
        /// </summary>
        private void CreateNewProfile()
        {
            try
            {
                if (ProfileManager.GetProfile() != null)
                {
                    bool createNew =
                        (MessageBox.Show(MBnewProfileMessage, MBnewProfileTitle,
                                         MessageBoxButton.OKCancel) == MessageBoxResult.OK);
                    if (!createNew)
                    {
                        return;
                    }
                }
                Navigator.NavigateTo(this, Navigator.ProfileUri + "?new=true");
            }
            catch (Exception err)
            {
                Logger.Error("CreateNewProfile", err.Message);
            }
        }

        /// <summary>
        /// Navigates to Ability list page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartFlow_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.StartUri);           
        }

        /// <summary>
        /// Navigates to About page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.AboutUri);          
        }

        /// <summary>
        /// Navigates to Help page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowProfileInfo();
        }

        /// <summary>
        /// Displays info about current profile
        /// </summary>
        private void ShowProfileInfo()
        {
            try
            {
                Profile.Profile profile = ProfileManager.GetProfile();
                if (profile == null)
                {
                    imgAvatar.Source = null;
                    textNickName.Text = string.Empty;
                    textProgress.Text = string.Empty;
                    SetStartFlowBtnState(false);
                    Logger.Info("ShowProfileInfor", "Profile == null");
                    return;
                }
                textNickName.Text = profile.NickName;
                textProgress.Text = profile.CurrentLevel.ToString();
                if (!string.IsNullOrEmpty(profile.AvatarUri))
                {
                    imgAvatar.Source = StorageManager.GetImageFromStorage(profile.AvatarUri);
                }
                else
                {
                    imgAvatar.Source = null;
                }
                SetStartFlowBtnState(true);
            }
            catch (Exception err)
            {
                Logger.Error("ShowProfileInfor", err.Message);
            }
                     
        }

        /// <summary>
        /// Profile bar Tap event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridProfile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.OpenProfile();
        }

        /// <summary>
        /// Navigates to profile page if profile is exist
        /// </summary>
        private void OpenProfile()
        {
            try
            {
                if (ProfileManager.GetProfile() != null)
                {
                    Navigator.NavigateTo(this, Navigator.ProfileUri);
                }
            }
            catch (Exception err)
            {
                Logger.Error("OpenProfile", err.Message);
            }
        }

        /// <summary>
        /// Sets disable state for statistic and start flow buttons
        /// </summary>
        /// <param name="isProfileExist"></param>
        private void SetStartFlowBtnState(bool isProfileExist)
        {
            try
            {
                if (isProfileExist)
                {
                    btnStartFlow.IsEnabled = true;
                    btnStatistics.IsEnabled = true;
                }
                else
                {
                    btnStartFlow.IsEnabled = false;
                    btnStatistics.IsEnabled = false;
                }
            }
            catch (Exception err)
            {
                Logger.Error("SetStartFlowBtnState",err.Message);
            }

        }

        /// <summary>
        /// Navigates to statistic page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.StatUri);
        }
    }
}