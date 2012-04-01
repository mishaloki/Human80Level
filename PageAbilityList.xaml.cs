﻿using System;
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
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageAbilityList : PhoneApplicationPage
    {
        public PageAbilityList()
        {
            InitializeComponent();
        }

        private void btnLuck_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Ability/Luck/PageAbilityLuck.xaml", UriKind.Relative));
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Ability/Power/PageAbilityPower.xaml", UriKind.Relative));
        }

        private void btnEndurance_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Ability/Endurance/PageAbilityEndurance.xaml", UriKind.Relative));
        }

        private void btnIntelligence_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Ability/Intelligence/PageAbilityIntelligence.xaml", UriKind.Relative));
        }

        private void btnPhysique_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Ability/Physique/PageAbilityPhysique.xaml", UriKind.Relative));
        }
    }
}