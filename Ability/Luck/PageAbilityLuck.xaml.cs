using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Human80Level.Ability.Luck;
using Microsoft.Phone.Controls;
using System.Linq;

namespace Human80Level
{
    public partial class PageAbilityLuck : PhoneApplicationPage
    {
        private const string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private const string TrashImageUrl = "/Images/Ability/Luck/trash.png";

        private const string DefaultEventMessage = "Open clover";

        private const string NullOrEmptyMessageText = "Message can't be empty. Please enter some text";

        private const string NullOrEmptyMessageTitle = "Error";

        private const string SuccessAddMessage = "Event has been added";

        private const string AlreadyUseCloverMessage = "You have already use clover today";

        private const string RemoveEventMessageText = "Event will be deleted. Are you sure?";

        private const string RemoveEventMessageTitle = "Confirm removing";

        private ObservableCollection <LuckEventMessage> eventList;
        
        public PageAbilityLuck()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                eventList = LuckEventManager.getEventList();
                listEventList.ItemsSource = eventList;
                LuckEventMessage message = (from luckEventMessage in eventList
                                where
                                    (luckEventMessage.Message == DefaultEventMessage) && (luckEventMessage.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                                select luckEventMessage).FirstOrDefault();
                if (message!=null)
                {
                    textTryCaption.Text = AlreadyUseCloverMessage;
                    pivotItemTryLuck.IsEnabled = false;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);

            }


        }

        private void imgLeft_Hold(object sender, GestureEventArgs e)
        {
            string url = string.Empty;
            bool isLuck = this.isLuck();
            if (isLuck)
            {
                url = CloverImageUrl;
            }
            else
            {
                url = TrashImageUrl;
            } 
            
            BitmapImage bitmapImage = new BitmapImage(new Uri(url,UriKind.Relative));
            Image image = sender as Image;
            image.Source = bitmapImage;
            pivotItemTryLuck.IsEnabled = false;
            LuckEventMessage message = new LuckEventMessage(DefaultEventMessage, DateTime.Now, isLuck);
            LuckEventManager.AddEventMessage(message);
            eventList.Add(message);
            textTryCaption.Text = AlreadyUseCloverMessage;
        }

        private bool isLuck ()
        {
            Random random = new Random();
            int value = random.Next(0, 2);           
            return (value == 0) ? true : false;            
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
            LuckEventMessage message = new LuckEventMessage(textMessage.Text, DateTime.Now, isLuck);
            LuckEventManager.AddEventMessage(message);
            eventList.Add(message);
            MessageBox.Show(SuccessAddMessage);
            textMessage.Text = string.Empty;
        }

        private bool isMessageValid()
        {
            return !string.IsNullOrEmpty(textMessage.Text);
        }

        private static void ShowValidationErrorMessage()
        {
            MessageBox.Show(NullOrEmptyMessageText, NullOrEmptyMessageTitle, MessageBoxButton.OK);
        }

        private void listEventList_Hold(object sender, GestureEventArgs e)
        {

        }

        private void listEventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LuckEventMessage message = listEventList.SelectedItem as LuckEventMessage;
            if (message != null)
            {
                textMessage.Text = message.Message;
            }
            
        }

        private void listEventList_DoubleTap(object sender, GestureEventArgs e)
        {
            LuckEventMessage message = (LuckEventMessage) listEventList.SelectedItem;
            if (MessageBox.Show(RemoveEventMessageText,RemoveEventMessageTitle,MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                eventList.Remove(message);
                LuckEventManager.RemoveEventMessage(message);
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            textMessage.Text = string.Empty;
        }

        private void textMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textMessage.Text))
            {
                btnClear.IsEnabled = false;
            }
            else
            {
                btnClear.IsEnabled = true;
            }
        }
        
    }
}