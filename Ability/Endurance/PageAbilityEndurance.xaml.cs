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
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Ability.Endurance
{
    public partial class PageAbilityEndurance : PhoneApplicationPage
    {
        public PageAbilityEndurance()
        {
            InitializeComponent();
            EnduranceManager.ExtractResult();
            UpdateTotalResult();
            StartWaiting();
        }

        private bool isBtnChecked;

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


        private void StopMeasure()
        {
            try
            {
                isBtnChecked = false;
                double distance = 2000;
                TimeSpan time = new TimeSpan(0,1,3,0);
                double speed = 8;

                EnduranceManager.SaveResults(distance,time,speed);
                UpdateTotalResult();
            }
            catch (Exception err)
            {
                Logger.Error("StopMeasure", err.Message);
            }
        }

        private void StartWaiting ()
        {
            Thread wait = new Thread(WaitForGPS);
            wait.Start();
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

        private void UpdateCurrentResult()
        {
            try
            {
                    while (isBtnChecked)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            textCurrentDistance.Text = EnduranceManager.GetCurrentDistance().ToString();
                            textCurrentTime.Text =EnduranceManager.GetCurrentTime().ToString();
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

        private void WaitForGPS()
        {
            try
            {
                int wait = 0;
                //todo replace with real call
                //while (!EnduranceManager.IsGpsAvailabel())
                while (wait < 5)
                {
                    Thread.Sleep(1000);
                    ++wait;
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

        private void btnStart_Unchecked(object sender, RoutedEventArgs e)
        {
            StopMeasure();
        }

    }
}