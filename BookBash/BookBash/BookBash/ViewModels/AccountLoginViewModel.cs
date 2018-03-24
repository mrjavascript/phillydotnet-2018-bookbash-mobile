using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookBash.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace BookBash.ViewModels
{
    public class AccountLoginViewModel : BindableBase, INavigationAware
    {
        //
        //  instance data
        private string _errorMessage, _userName, _password;
        private Color _theme;
        private bool _hasErrors;
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly IPreferenceService _preferenceService;

        public ICommand ReturnToHomeCommand { get; set; }
        public ICommand LoginButtonClickCommand { get; set; }

        public Color Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        public AccountLoginViewModel(INavigationService navigationService, IAccountService accountService, IPreferenceService preferenceService)  // INavigation navigation)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _preferenceService = preferenceService;
//            Navigation = navigation;
            ReturnToHomeCommand = new DelegateCommand(async () =>
            {
//                await Navigation.PopAsync();
                await navigationService.GoBackAsync();
            });
            LoginButtonClickCommand = new DelegateCommand(async () => { await OnLogin(); });
        }

        public async Task OnLogin()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                ErrorMessage = "User name is required";
                HasErrors = true;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Password is required";
                HasErrors = true;
            }
            else
            {
                try
                {
                    await _accountService.AccountLogin(UserName, Password);
                    HasErrors = false;
                    await _navigationService.NavigateAsync(new Uri($"DashboardPage", UriKind.Relative), null, false);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    ErrorMessage = ex.Message;
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            //
            //  Set theme here
            Theme = await _preferenceService.HasDarkModeSet() ? Color.SlateGray : Color.White;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}