using System.Windows;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageAbilityList : PhoneApplicationPage
    {
        private const string LuckUri = "/Ability/Luck/PageAbilityLuck.xaml";

        private const string PowerUri = "/Ability/Power/PageAbilityPower.xaml";

        private const string EnduranceUri = "/Ability/Endurance/PageAbilityEndurance.xaml";

        private const string IntelUri = "/Ability/Intelligence/PageAbilityIntelligence.xaml";

        private const string PhysiqueUri = "/Ability/Physique/PageAbilityPhysique.xaml";

        public PageAbilityList()
        {
            InitializeComponent();
        }

        private void btnLuck_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, LuckUri);
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, PowerUri);
        }

        private void btnEndurance_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, EnduranceUri);
        }

        private void btnIntelligence_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this, IntelUri);
        }

        private void btnPhysique_Click(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(this,PhysiqueUri);
        }
    }
}