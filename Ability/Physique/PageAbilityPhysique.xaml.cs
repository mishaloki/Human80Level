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
using System.Windows.Shapes;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Ability.Physique
{
    public partial class PageAbilityPhysique : PhoneApplicationPage
    {
        public PageAbilityPhysique()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.CalculateIdeal();
        }

        private void CalculateIdeal()
        {
            try
            {
                string textWeigth = boxWeight.Text;
                double weight = 0;
                double ideal = 0;
                Profile.Profile profile = ProfileManager.GetProfile();
                if (double.TryParse(textWeigth, out weight))
                {
                    ideal = PhysiqueManager.GetIdealWeight(weight); 
                    profile.Weight = weight;
                    ProfileManager.UpdateProfile(profile);
                    this.UpdateFigure(ideal,weight);
                }
                else
                {
                    MessageBox.Show("error weigth");
                }
            }
            catch (Exception err)
            {
                Logger.Error("CalculateIdeal", err.Message);
            }

        }

        private void UpdateFigure(double idWeight, double curWeight)
        {
            textIdealMarker.Text = idWeight.ToString();
            textRightSideMarker.Text = (idWeight*2).ToString();
            textCurrentMarker.Text = boxWeight.Text;
            double barLenght = 440;
            double onePerc = (barLenght/(idWeight));
            double margLeft = onePerc * (curWeight-idWeight);
            textCurrentMarker.Margin = new Thickness(margLeft,90,0,0);
            imgCurrentMarker.Margin = new Thickness(margLeft,30,0,0);
        }
    }
}