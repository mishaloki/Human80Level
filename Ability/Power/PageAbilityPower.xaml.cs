using System;
using System.Threading;
using System.Windows;
using System.Windows.Resources;
using Human80Level.Utils;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;

namespace Human80Level.Ability.Power
{
    public partial class PageAbilityPower : PhoneApplicationPage
    {
        
        public PageAbilityPower()
        {
            InitializeComponent();
            PowerManager.ExtractResult();
            PowerManager.PrepareAccel();
            UpdateTotalResult();
        }

        private void UpdateTotalResult()
        {
            try
            {
                textTotalAbs.Text = PowerManager.GetTotalAbs().ToString();
            }
            catch (Exception err)
            {
                Logger.Error("UpdateTotalResult", err.Message);
            }
        }

        private void btnStart_Checked(object sender, RoutedEventArgs e)
        {
            StartCount();
        }

        private void StartCount()
        {
            try
            {       
                PowerManager.NewAbs += UpdateCurrentResult;
                PowerManager.StartAccel();
                textCurrentAbs.Text = "0";
            }
            catch (Exception err)
            {
                Logger.Error("StartCount", err.Message);
            }            
        }

        private void UpdateCurrentResult(object sender, EventArgs e)
        {
            try
            {
                
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        textCurrentAbs.Text = PowerManager.GetCurrentAbs().ToString();
                        if ((bool)switchSound.IsChecked)
                        {
                            PlaySound();
                        }
                    });
            }
            catch (Exception err)
            {
                Logger.Error("UpdateCurrentResult", err.Message);
            }
        }

        private void btnStart_Unchecked(object sender, RoutedEventArgs e)
        {
            StopCount();
        }

        private void StopCount()
        {
            try
            {
                PowerManager.StopAccel();
                PowerManager.SaveResults();
                UpdateTotalResult();
            }
            catch (Exception err)
            {
                Logger.Error("StopCount", err.Message);
            }
        }

        private void PlaySound()
        {
            try
            {
                StreamResourceInfo sri = Application.GetResourceStream(new Uri("/Human80Level;component/Resources/beep.wav", UriKind.Relative));
                if (sri != null)
                {
                    SoundEffect effect = SoundEffect.FromStream(sri.Stream);
                    SoundEffectInstance inst = effect.CreateInstance();
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        inst.Play();
                    });

                }
            }
            catch (Exception err)
            {
                Logger.Error("PlaySound", err.Message);
            }

        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);
        }
    }
}