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
using Human80Level.Utils;
using Microsoft.Phone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Human80Level
{
    public partial class PageProfile : PhoneApplicationPage
    {
        private string avatarUrl = string.Empty;

       

        PhotoChooserTask photoChooserTask;

        CameraCaptureTask cameraTask;
        
        public PageProfile()
        {
            InitializeComponent();
        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            DateTime birth = dateBirth.Value.Value;
            int height = int.Parse(boxHeight.Text);
            Profile.Profile profile = new Profile.Profile(textNickName.Text,avatarUrl,0,true,0,birth,height);
            ProfileManager.UpdateProfile(profile); 
        }

        private void btnImageEmpty_Click(object sender, RoutedEventArgs e)
        {
            avatarUrl = string.Empty;
            imgAvatar.Source = null;
        }

        public void GetPicture(bool fromCamera)
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

        public void onTaskCompleted(object sender, PhotoResult e)
        {
            if (e.Error != null)
            {
                
                return;
            }

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

                    }
                    break;

                case TaskResult.Cancel:
                    break;

                default:
                    break;
            }
        }

        private void btnImageAlbum_Click(object sender, RoutedEventArgs e)
        {
            this.GetPicture(false);
        }

        private void btnImageCamera_Click(object sender, RoutedEventArgs e)
        {
            this.GetPicture(true);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.NavigationContext.QueryString.ContainsKey("new"))
            {
                
            }
            else
            {
                ShowProfile();
            }
            
        }

        private void ShowProfile()
        {
            Profile.Profile profile = ProfileManager.getProfile();
            if (profile == null)
            {
                return;
            }
            textNickName.Text = profile.NickName;
            boxHeight.Text = profile.Heigth.ToString();
            imgAvatar.Source = StorageManager.GetImageFromStorage(profile.AvatarUri);
            dateBirth.Value = profile.Birth;
        }
    }
}