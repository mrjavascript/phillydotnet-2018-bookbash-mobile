using System.Windows.Input;
using BookBash.ViewModels.Base;
using BookBash.Views;
using Xamarin.Forms;

namespace BookBash.ViewModels
{
    public class AccountCreateViewModel : ViewModelBase
    {
        public ICommand RegisterButtonCommand { get; set; }
        public ICommand GoToLoginCommand { get; set; }

        public AccountCreateViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RegisterButtonCommand = new Command(() => { });
            GoToLoginCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new AccountLoginPage());
            });
        }
    }
}