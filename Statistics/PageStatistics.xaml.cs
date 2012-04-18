using System.Windows.Navigation;
using Human80Level.Ability.Endurance;
using Human80Level.Ability.Power;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level.Statistics
{
    public partial class PageStatistics : PhoneApplicationPage
    {
        public PageStatistics()
        {
            InitializeComponent();
            LocalizedStrings.LocalizeHelpBtn(this);
            PowerManager.ExtractResult();
            EnduranceManager.ExtractResult();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IndicatorManager.CreateIndicators();
            listIndicators.ItemsSource = IndicatorManager.GetIndicators();
            SetTotalLevel();
        }

        private void SetTotalLevel()
        {
            textTotalLevel.Text = IndicatorManager.GetTotalLevel().ToString();
            progressTotal.Value = IndicatorManager.GetTotalLevel();
        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Navigator.NavigateTo(this, Navigator.HelpUri);
        }
    }
}