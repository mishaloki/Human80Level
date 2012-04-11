using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Human80Level.Ability.Luck;
using Human80Level.Database;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageAbilityLuck : PhoneApplicationPage
    {
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private readonly string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private readonly string TrashImageUrl = "/Images/Ability/Luck/trash.png";

        private readonly string DefaultEventMessage = AppResources.DefaultEventMessage;

        private static readonly string NullOrEmptyMessageText = AppResources.NullOrEmptyMessageText;
                
        private static readonly string NullOrEmptyMessageTitle = AppResources.NullOrEmptyMessageTitle;
                
        private readonly string SuccessAddMessage = AppResources.SuccessAddMessage;
                
        private readonly string AlreadyUseCloverMessage = AppResources.AlreadyUseCloverMessage;
                
        private readonly string RemoveEventMessageText = AppResources.RemoveEventMessageText;
                
        private readonly string RemoveEventMessageTitle = AppResources.RemoveEventMessageTitle;

        private ObservableCollection <Event> eventList;

        private static readonly string LoggerMessageFormat = "Error in {0}, message: {1}";
        
        public PageAbilityLuck()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CheckTryLuckEvent();
        }

        private void CheckTryLuckEvent()
        {
            try
            {
                DBHelper.CreateDatabase();
                eventList = LuckEventManager.GetEventList();
                listEventList.ItemsSource = eventList;
                Event message = (from luckEventMessage in eventList
                                            where
                                                (luckEventMessage.Message.Trim() == DefaultEventMessage) && (luckEventMessage.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                                            select luckEventMessage).FirstOrDefault();
                if (message != null)
                {
                    textTryCaption.Text = AlreadyUseCloverMessage;
                    pivotItemTryLuck.IsEnabled = false;
                    
                }
                eventList.CollectionChanged += UpdateEventCounter;
                this.UpdateEventCounter(eventList, null);
            }
            catch (Exception error)
            {
                Logger.Error(string.Format(LoggerMessageFormat, "CheckTryLuckEvent", error.Message));
            }
        }

        private void UpdateEventCounter(object sender, NotifyCollectionChangedEventArgs args)
        {
            try
            {
                ObservableCollection<Event> eventMessages = sender as ObservableCollection<Event>;
                int luckNumb = (from luckEventMessage in eventMessages
                                where luckEventMessage.IsLuck == true
                                select luckEventMessage).Count();
                int failureNumb = eventMessages.Count - luckNumb;

                textFailureCounter.Text = failureNumb.ToString();
                textLuckCounter.Text = luckNumb.ToString();
            }
            catch (Exception err)
            {
                Logger.Error("UpdateEventCounter", err.Message);
            }

        }

        private void imgLeft_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image image = sender as Image;
            this.TryLuck(image);
        }

        private void TryLuck(Image image)
        {
            try
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

                BitmapImage bitmapImage = new BitmapImage(new Uri(url, UriKind.Relative));                
                image.Source = bitmapImage;
                pivotItemTryLuck.IsEnabled = false;
                Event message = new Event(DefaultEventMessage, DateTime.Now, isLuck);
                LuckEventManager.AddEventMessage(message);
                eventList.Add(message);
                textTryCaption.Text = AlreadyUseCloverMessage;
            }
            catch (Exception error)
            {
                Logger.Error(string.Format(LoggerMessageFormat, "TryLuck", error.Message));
            }
        }

        private bool isLuck ()
        {
            try
            {
                Random random = new Random();
                int value = random.Next(0, 2);
                return (value == 0) ? true : false;  
            }
            catch (Exception error)
            {
                Logger.Error(string.Format(LoggerMessageFormat, "isLuck", error.Message));
                return false;
            }
          
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
            try
            {
                if (!isMessageValid())
                {
                    ShowValidationErrorMessage();
                    return;
                }
                Event message = new Event(textMessage.Text, DateTime.Now, isLuck);
                message.Id = 1;
                LuckEventManager.AddEventMessage(message);
                eventList.Add(message);
                MessageBox.Show(SuccessAddMessage);
                textMessage.Text = string.Empty;
            }
            catch (Exception error)
            {
                Logger.Error(string.Format(LoggerMessageFormat, "AddMessage", error.Message));
            }

        }

        private bool isMessageValid()
        {
            return !string.IsNullOrEmpty(textMessage.Text);
        }

        private static void ShowValidationErrorMessage()
        {
            MessageBox.Show(NullOrEmptyMessageText, NullOrEmptyMessageTitle, MessageBoxButton.OK);
        }

        private void listEventList_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void listEventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event message = listEventList.SelectedItem as Event;
            this.CopyMessageToInputField(message);
            
        }


        private void CopyMessageToInputField(Event message)
        {
            if (message != null)
            {
                textMessage.Text = message.Message;
            }
        }

        private void listEventList_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Event message = (Event) listEventList.SelectedItem;
            this.RemoveEventMessage(message);
            
        }

        private void RemoveEventMessage(Event message)
        {
            try
            {
                if (MessageBox.Show(RemoveEventMessageText, RemoveEventMessageTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    eventList.Remove(message);
                    LuckEventManager.RemoveEventMessage(message);
                }
            }
            catch (Exception err)
            {
                Logger.Error("RemoveEventMessage", err.Message);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.ClearMessageField();
        }

        private void ClearMessageField()
        {
            textMessage.Text = string.Empty;
        }


        private void textMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SetClearButtonState();   
        }

        private void SetClearButtonState()
        {
            try
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
            catch (Exception error)
            {
                Logger.Error("SetClearButtonState", error.Message);
            }
            
        }
        
    }
}