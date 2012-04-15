using System.Windows.Navigation;
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