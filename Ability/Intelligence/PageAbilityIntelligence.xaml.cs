using System;
using System.Windows;
using System.Windows.Navigation;
using Human80Level.Ability.Intelligance;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Ability.Intelligence
{
    public partial class PageIntelligence : PhoneApplicationPage
    {
        public PageIntelligence()
        {
            InitializeComponent();
            LocalizedStrings.LocalizeHelpBtn(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);           
            this.DisplayQuestion();
        }
        
        private void DisplayQuestion()
        {
            try
            {
                if (IntelligenceManager.IsAlreadyAsked())
                {
                    BlockScreen();
                    return;
                }
                IntelligenceManager.ExtractRandomQuestion();
                textQuestion.Text = IntelligenceManager.GetQuestion();
                btnLink.NavigateUri = new Uri(IntelligenceManager.GetLink(),UriKind.Absolute);
            }
            catch (Exception err)
            {
                Logger.Error("DisplayQuestion", err.Message);
            }

        }

        private void BlockScreen()
        {
            textQuestion.Text = AppResources.IntelPageTextAlreadyAnsweredToday;
            boxAnswer.Visibility = Visibility.Collapsed;
            textAnswer.Visibility = Visibility.Collapsed;
            btnLink.Visibility = Visibility.Collapsed;
            btnAnswer.Visibility = Visibility.Collapsed;
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            this.CheckAnswer();
        }

        private void CheckAnswer()
        {
            try
            {
                string answer = boxAnswer.Text;

                if (IntelligenceManager.IsAnswerCorrect(answer))
                {
                    MessageBox.Show(AppResources.IntelPageMBCorrect, AppResources.IntelPageMBTitleCorrect,
                                    MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(AppResources.IntelPageMBWrong, AppResources.IntelPageMBTitleIncorrect,
                                    MessageBoxButton.OK);
                }
                IntelligenceManager.SetAlreadyAsked();
                BlockScreen();
            }
            catch (Exception err)
            {
                Logger.Error("CheckAnswer", err.Message); 
            }
        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);
        }
    }
}