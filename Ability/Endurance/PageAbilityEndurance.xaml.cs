using System;
using System.Threading;
using System.Windows;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Ability.Endurance
{
    public partial class PageAbilityEndurance : PhoneApplicationPage
    {
        #region private fields

        private bool isBtnChecked;

        #endregion

        public PageAbilityEndurance()
        {
            InitializeComponent();
            EnduranceManager.ExtractResult();
            UpdateTotalResult();
            EnduranceManager.StartGps();
            StartWaiting();
        }

        #region waiting GPS methods

        private void StartWaiting()
        {
            Thread wait = new Thread(WaitForGps);
            wait.Start();
        }

        private void WaitForGps()
        {
            try
            {
                while (!EnduranceManager.IsGpsAvailabel())
                {
                    Thread.Sleep(1000);
                }

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    btnStart.Visibility = System.Windows.Visibility.Visible;
                    textWait.Visibility = System.Windows.Visibility.Collapsed;
                    progressWait.Visibility = System.Windows.Visibility.Collapsed;
                });
            }
            catch (Exception err)
            {
                Logger.Error("WaitForGPS", err.Message);
            }
        }
        #endregion

        #region start GPS methods

        private void btnStart_Checked(object sender, RoutedEventArgs e)
        {
            StartMeasure();
        }

        private void StartMeasure()
        {
            try
            {
                isBtnChecked = true;
                Thread updater = new Thread(UpdateCurrentResult);
                updater.Start();
            }
            catch (Exception err)
            {
                Logger.Error("StartMeasure", err.Message);
            }
        }
        #endregion

        #region UI updators
        private void UpdateTotalResult()
        {
            try
            {
                textTotalTime.Text = EnduranceManager.GetTotalTime().ToString();
                textTotalDistance.Text = EnduranceManager.GetTotalDistance().ToString();
                textAvgSpeed.Text = EnduranceManager.GetAvgSpeed().ToString();
            }
            catch (Exception err)
            {
                Logger.Error("UpdateTotalResult", err.Message);
            }
        }

        private void UpdateCurrentResult()
        {
            try
            {
                EnduranceManager.StartCount();
                while (isBtnChecked)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        textCurrentDistance.Text = EnduranceManager.GetCurrentDistance().ToString();
                        textCurrentTime.Text = EnduranceManager.GetCurrentTime().ToString();
                        textCurrentSpeed.Text = EnduranceManager.GetCurrentSpeed().ToString();
                    });
                    Thread.Sleep(1000);
                }
            }
            catch (Exception err)
            {
                Logger.Error("UpdateCurrentResult", err.Message);
            }
        }
        #endregion

        #region stop GPS methods
        private void btnStart_Unchecked(object sender, RoutedEventArgs e)
        {
            StopMeasure();
        }

        private void StopMeasure()
        {
            try
            {
                EnduranceManager.StopCount();
                EnduranceManager.StopGps();
                EnduranceManager.SaveResults();
                UpdateTotalResult();
            }
            catch (Exception err)
            {
                Logger.Error("StopMeasure", err.Message);
            }
        }
        #endregion

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);
        }
    }
}