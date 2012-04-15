using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Ability.Endurance
{
    public partial class PageAbilityEndurance : PhoneApplicationPage
    {
        private bool isBtnChecked;

        private double waitingTime = 0;
        
        public PageAbilityEndurance()
        {
            InitializeComponent();
            EnduranceManager.ExtractResult();
            UpdateTotalResult();
            EnduranceManager.StartGps();
            StartWaiting();
        }

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

        private void StartWaiting()
        {
            Thread wait = new Thread(WaitForGPS);
            wait.Start();
        }

        private void WaitForGPS()
        {
            try
            {
                //todo replace with real call
                while (!EnduranceManager.IsGpsAvailabel())
                //while (wait < 5)
                {
                    Thread.Sleep(1000);
                    waitingTime++;
                    if (waitingTime > 10)
                    {
                        return;
                    }
                }
                if (waitingTime > 10)
                {
                    MessageBox.Show(AppResources.EndurPageMBGpsReadyMessage, AppResources.EndurPageMBGpsFailTitle,
                                    MessageBoxButton.OK);
                    return;
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
                        //todo replace
                        //textCurrentTime.Text = EnduranceManager.GetCurrentTime().ToString();
                        textCurrentTime.Text = "x: " + EnduranceManager.Position.X.ToString() + ", y: " + EnduranceManager.Position.Y;
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

    }
}