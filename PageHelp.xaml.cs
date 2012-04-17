using System.Collections.Generic;
using System.Windows.Navigation;
using Human80Level.Utils;
using Microsoft.Phone.Controls;

namespace Human80Level
{
    public partial class PageHelp : PhoneApplicationPage
    {
        
        
        /// <summary>
        /// Help page constructor
        /// </summary>
        public PageHelp()
        {
            InitializeComponent();
            Logger.Info("PageHelp constructor", "page was initialized");           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetPivotItem();
            
        }

        private void SetPivotItem()
        {
            if (!this.NavigationContext.QueryString.ContainsKey(Navigator.FromPage))
            {
                return; 
            }
            string formPage = this.NavigationContext.QueryString[Navigator.FromPage];
            if (!string.IsNullOrEmpty(formPage) && (Navigator.pages.IndexOf(formPage)>=0))
            {
                pivot.SelectedIndex = Navigator.pages.IndexOf(formPage);
            }
        }
    }
}