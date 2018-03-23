using System;
using System.Windows.Input;
using BookBash.ViewModels.Base;
using BookBash.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace BookBash.ViewModels
{
    public class AccountCreateViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public ICommand RegisterButtonCommand { get; set; }
        public ICommand GoToLoginCommand { get; set; }

        public AccountCreateViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            RegisterButtonCommand = new Command(() => { });
            GoToLoginCommand = new Command(async () =>
            {
                //                await Navigation.PushAsync(new AccountLoginPage());
                await _navigationService.NavigateAsync(new Uri($"AccountLoginPage", UriKind.Relative), null, false);

            });
        }
    }
}