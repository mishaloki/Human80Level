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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Human80Level.Statistics
{
    public partial class PageStatistics : PhoneApplicationPage
    {
        public PageStatistics()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IndicatorManager.CreateIndicators();
            listIndicators.ItemsSource = IndicatorManager.GetIndicators();
            //todo add extracting endurance totaldistance
        }
    }
}