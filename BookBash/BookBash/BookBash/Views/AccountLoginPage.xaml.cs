using BookBash.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountLoginPage : ContentPage
	{
		public AccountLoginPage ()
		{
		    InitializeComponent();
//            BindingContext = new AccountLoginViewModel(Navigation);
        }
	}
}