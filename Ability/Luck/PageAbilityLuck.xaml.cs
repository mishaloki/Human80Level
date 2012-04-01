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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Human80Level.Ability.Luck;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageAbilityLuck : PhoneApplicationPage
    {
        private const string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private const string TrashImageUrl = "/Images/Ability/Luck/trash.png";

        private const string DefaultEventMessage = "";

        private const string NullOrEmptyMessageText = "Message can't be empty. Please enter some text";

        private const string NullOrEmptyMessageTitle = "Error";



        private bool isLuck;
        
        public PageAbilityLuck()
        {
            InitializeComponent();
        }

        private void imgLeft_Hold(object sender, GestureEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(GetRandomImageUrl(),UriKind.Relative));
            Image image = sender as Image;
            image.Source = bitmapImage;
            
        }

        private string GetRandomImageUrl ()
        {
            Random random = new Random();
            int value = random.Next(0, 2);           
            isLuck = (value == 0) ? true : false;
            string url = (isLuck) ? CloverImageUrl : TrashImageUrl;
            return url;
        }

        private void btnLuck_Click(object sender, RoutedEventArgs e)
        {
           this.AddMessage(true);
        }

        private void btnFailure_Click(object sender, RoutedEventArgs e)
        {
            this.AddMessage(false);
        }

        private void AddMessage(bool isLuck)
        {
            if (!isMessageValid())
            {
                ShowValidationErrorMessage();
                return;
            }
            LuckEventManager.AddEventMessage(new LuckEventMessage(textMessage.Text, DateTime.Now, isLuck));
        }

        private bool isMessageValid()
        {
            return !string.IsNullOrEmpty(textMessage.Text);
        }

        private static void ShowValidationErrorMessage()
        {
            MessageBox.Show(NullOrEmptyMessageText, NullOrEmptyMessageTitle, MessageBoxButton.OK);
        }
        
    }
}