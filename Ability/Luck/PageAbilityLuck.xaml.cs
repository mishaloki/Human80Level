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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Human80Level.Ability.Luck;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageAbilityLuck : PhoneApplicationPage
    {
        private const string CloverImageUrl = "/Images/Ability/Luck/clover.png";

        private const string TrashImageUrl = "/Images/Ability/Luck/trash.png";

        private const string DefaultEventMessage = "";

        private bool isLuck;
        
        public PageAbilityLuck()
        {
            InitializeComponent();
        }

        private void imgLeft_Hold(object sender, GestureEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(GetRandomImageUrl(),UriKind.Relative));
            Image image = sender as Image;
            image.Source = bitmapImage;
            
        }

        private string GetRandomImageUrl ()
        {
            Random random = new Random();
            int value = random.Next(0, 2);           
            isLuck = (value == 0) ? true : false;
            string url = (isLuck) ? CloverImageUrl : TrashImageUrl;
            return url;
        }
    }
}