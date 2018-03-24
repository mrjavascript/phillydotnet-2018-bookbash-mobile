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
    public class AccountCreateViewModel : BindableBase, INavigationAware
    {
        //
        //  instance data
        private string _errorMessage, _userName, _password, _emailAddress;
        private Color _theme;
        private bool _hasErrors;
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly IPreferenceService _preferenceService;

        //
        //  Properties
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }

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

        public ICommand RegisterButtonCommand { get; set; }
        public ICommand GoToLoginCommand { get; set; }

        public ICommand FacebookCommand { get; set; }


        //        public AccountCreateViewModel(INavigation navigation)
        public AccountCreateViewModel(INavigationService navigationService, IAccountService accountService, IPreferenceService preferenceService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _preferenceService = preferenceService;

            //
            //  Initialize
            HasErrors = false;

            //
            //  Create an account
            RegisterButtonCommand = new DelegateCommand(async () => { await OnRegister(); });

            GoToLoginCommand = new DelegateCommand(async () =>
            {
                //                await Navigation.PushAsync(new AccountLoginPage());
                await _navigationService.NavigateAsync(new Uri($"AccountLoginPage", UriKind.Relative), null, false);
            });

            FacebookCommand = new DelegateCommand(async () => await OnFacebookButtonClick());
        }

        private async Task OnFacebookButtonClick()
        {
            await _navigationService.NavigateAsync(new Uri($"FacebookPage", UriKind.Relative), null, false);
        }

        public async Task OnRegister()
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
            else if (string.IsNullOrEmpty(EmailAddress))
            {
                ErrorMessage = "Email is required";
                HasErrors = true;
            }
            else
            {
                try
                {
                    await _accountService.CreateUserAccount(UserName, Password, EmailAddress);
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