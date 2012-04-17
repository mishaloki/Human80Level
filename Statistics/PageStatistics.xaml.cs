using System.Windows.Navigation;
using Human80Level.Utils;
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

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);
        }
    }
}