using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : TabbedPage, INavigationAware
    {
        public DashboardPage ()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                PageUtilities.OnNavigatedTo(child, parameters);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}