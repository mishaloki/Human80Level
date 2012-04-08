using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Human80Level.Utils;
using Microsoft.Phone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Human80Level
{
    public partial class PageProfile : PhoneApplicationPage
    {
        /// <summary>
        /// Path to avatar image in Isolated storage
        /// </summary>
        private string avatarUrl = string.Empty;
       
        /// <summary>
        /// Task that opens phone gallery 
        /// </summary>
        private PhotoChooserTask photoChooserTask;

        /// <summary>
        /// Message box title for incorrect height
        /// </summary>
        private readonly string MBheightIncorrectTitle = "";

        /// <summary>
        /// Message box message for incorrect nickname
        /// </summary>
        private readonly string MBnickIncorrectMessage = "";

        /// <summary>
        /// Message box title for incorrect nickname
        /// </summary>
        private readonly string MBnickIncorrectTitle = "";

        /// <summary>
        /// Message box message for incorrect height
        /// </summary>
        private readonly string MBheightIncorrectMessage = "";

        /// <summary>
        /// Message box save image error title
        /// </summary>
        private readonly string MBsaveErrorTitle = "";

        /// <summary>
        /// Message box save image error message
        /// </summary>
        private readonly string MBsaveErrorMessage = "";

        /// <summary>
        /// Message box get image error title
        /// </summary>
        private readonly string MBgetErrorTitle = "";

        /// <summary>
        /// Message box get image error message
        /// </summary>
        private readonly string MBgetErrorMessage = "";

        /// <summary>
        /// Message box update  title
        /// </summary>
        private readonly string MBupdateTitle = "";

        /// <summary>
        /// Message box update message
        /// </summary>
        private readonly string MBupdateMessage = "";

        /// <summary>
        /// Task that opens phone camera app
        /// </summary>
        private CameraCaptureTask cameraTask;
        
        /// <summary>
        /// Page constructor
        /// </summary>
        public PageProfile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save button handler
        /// </summary>
        /// <param name="sender">Save button</param>
        /// <param name="e">button event args</param>
        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {            
            this.SaveProfile();
        }

        /// <summary>
        /// Gets profile data from Input fields and saves it to Isolated storage
        /// </summary>
        private void SaveProfile()
        {
            try
            {
                if (string.IsNullOrEmpty(textNickName.Text))
                {
                    MessageBox.Show(MBnickIncorrectTitle, MBnickIncorrectMessage, MessageBoxButton.OK);
                    return;
                }
                int height;
                if (!int.TryParse(boxHeight.Text,out height))
                {
                    MessageBox.Show(MBheightIncorrectTitle, MBheightIncorrectMessage, MessageBoxButton.OK);
                    return;
                }
                //todo Add datepicker icons
                DateTime birth = dateBirth.Value.Value;
                Profile.Profile profile = new Profile.Profile(textNickName.Text, avatarUrl, 0, true, 0, birth, height);
                ProfileManager.UpdateProfile(profile);
                MessageBox.Show(MBupdateMessage,MBupdateTitle,MessageBoxButton.OK);
            }
            catch (Exception err)
            {
                Logger.Error("SaveProfile", err.Message);
            }
        }

        /// <summary>
        /// Empty button handler
        /// </summary>
        /// <param name="sender">empty button</param>
        /// <param name="e">button event args</param>
        private void btnImageEmpty_Click(object sender, RoutedEventArgs e)
        {
            SetAvatarToEmptyImage();
        }

        /// <summary>
        /// Resets avatar Uri and sets preview image source to null
        /// </summary>
        private void SetAvatarToEmptyImage()
        {
            try
            {
                avatarUrl = string.Empty;
                imgAvatar.Source = null;
            }
            catch (Exception err)
            {
                Logger.Error("SetAvatarToEmptyImage", err.Message);
            }
        }

        /// <summary>
        /// Gets image from camera or gallery
        /// </summary>
        /// <param name="fromCamera">does image have to be taken from camera or from gallery</param>
        private void GetPicture(bool fromCamera)
        {
            try
            {
                if (fromCamera)
                {
                    cameraTask = new CameraCaptureTask();
                    cameraTask.Completed += onTaskCompleted;
                    cameraTask.Show();
                }
                else
                {
                    photoChooserTask = new PhotoChooserTask();
                    photoChooserTask.Completed += onTaskCompleted;
                    photoChooserTask.Show();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(MBgetErrorTitle, MBgetErrorMessage, MessageBoxButton.OK);
                Logger.Error("GetPicture", err.Message);
            }
        }

        /// <summary>
        /// Saves image to Isolated storage and sets preview image source to new image
        /// </summary>
        /// <param name="sender">camera or photo chosen task</param>
        /// <param name="e"></param>
        private void onTaskCompleted(object sender, PhotoResult e)
        {
            try
            {
                switch (e.TaskResult)
                {
                    case TaskResult.OK:
                        try
                        {
                            string imagePathOrContent = string.Empty;
                            WriteableBitmap image = PictureDecoder.DecodeJpeg(e.ChosenPhoto);
                            imagePathOrContent = StorageManager.SaveImageToStorage(image);
                            avatarUrl = imagePathOrContent;
                            imgAvatar.Source = StorageManager.GetImageFromStorage(avatarUrl);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(MBsaveErrorTitle,MBsaveErrorMessage,MessageBoxButton.OK);
                        }
                        break;

                    case TaskResult.Cancel:
                        break;

                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(MBgetErrorTitle, MBgetErrorMessage, MessageBoxButton.OK);
                Logger.Error("onTaskCompleted", err.Message);
            }
            
        }

        /// <summary>
        /// Album button handler
        /// </summary>
        /// <param name="sender">album button</param>
        /// <param name="e">button event args</param>
        private void btnImageAlbum_Click(object sender, RoutedEventArgs e)
        {
            this.GetPicture(false);
        }

        /// <summary>
        /// Camera button 
        /// </summary>
        /// <param name="sender">camera button</param>
        /// <param name="e">button event args</param>
        private void btnImageCamera_Click(object sender, RoutedEventArgs e)
        {
            this.GetPicture(true);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);    
            CheckProfileExistence();
        }

        /// <summary>
        /// Checks if new profile is needed and display profile data if it is already exist
        /// </summary>
        private void CheckProfileExistence()
        {
            try
            {               
                if (this.NavigationContext.QueryString.ContainsKey("new"))
                {
                    ProfileManager.RemoveProfile();
                }
                else
                {
                    ShowProfile();
                }
            }
            catch (Exception err)
            {
                Logger.Error("CheckProfileExistence", err.Message);
            }        
        }

        /// <summary>
        /// Gets profile from app settings and displays profile data
        /// </summary>
        private void ShowProfile()
        {
            try
            {
                Profile.Profile profile = ProfileManager.GetProfile();
                if (profile == null)
                {
                    return;
                }
                textNickName.Text = profile.NickName;
                boxHeight.Text = profile.Heigth.ToString();
                dateBirth.Value = profile.Birth;
                if (!string.IsNullOrEmpty(profile.AvatarUri))
                {
                    imgAvatar.Source = StorageManager.GetImageFromStorage(profile.AvatarUri);
                }                              
            }
            catch (Exception e)
            {
                Logger.Error("ShowProfile", e.Message);
            }
        }
    }
}