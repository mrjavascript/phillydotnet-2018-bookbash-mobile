using BookBash.Views;
using Xamarin.Forms;

namespace BookBash
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

//			MainPage = new BookBash.MainPage();
		    MainPage = new NavigationPage(new AccountCreatePage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
