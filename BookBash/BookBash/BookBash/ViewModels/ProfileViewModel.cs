using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookBash.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BookBash.ViewModels
{
    public class ProfileVIewModel : BindableBase, INavigationAware
    {
        //
        //  Instance data
        private bool _hasErrors;
        private string _errors;
        private string _newPassword;
        private string _newPasswordConfirm;
        private bool _isDarkMode;
        private Color _theme;
        private readonly IAccountService _accountService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IPreferenceService _preferenceService;

        //
        //  Properties
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set => SetProperty(ref _isDarkMode, value);
        }

        public Color Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }
        public string Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }
        public string NewPasswordConfirm
        {
            get => _newPasswordConfirm;
            set => SetProperty(ref _newPasswordConfirm, value);
        }

        public ICommand LogOutButtonCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        public ProfileVIewModel(INavigationService navigationService, IAccountService accountService, IPageDialogService pageDialogService, IPreferenceService preferenceService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _pageDialogService = pageDialogService;
            _preferenceService = preferenceService;

            ChangePasswordCommand = new DelegateCommand(async () => await OnPasswordReset());
            LogOutButtonCommand = new DelegateCommand(async () => await OnLogoutPress());
        }

        private async Task OnLogoutPress()
        {
            _accountService.LogOutUser();

            // Application.Current.MainPage = new AccountCreatePage();
            await _navigationService.NavigateAsync(new Uri("/NavigationPage/AccountCreatePage", UriKind.Absolute));
        }


        public async Task OnPasswordReset()
        {
            if (string.IsNullOrEmpty(NewPassword))
            {
                HasErrors = true;
                Errors = "new password is required";
            }
            else if (string.IsNullOrEmpty(NewPasswordConfirm))
            {
                HasErrors = true;
                Errors = "new password confirm is required";
            }
            else if (NewPassword != NewPasswordConfirm)
            {
                HasErrors = true;
                Errors = "passwords dont match";
            }
            else
            {
                try
                {
                    await _accountService.ResetPassword(NewPassword);
                    HasErrors = false;
                    await _pageDialogService.DisplayAlertAsync("Success", "Use new pasword at next login", "OK");
                }

                catch (Exception e)
                {
                    HasErrors = true;
                    Errors = $"errors: {e.Message}";
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await OnThemeSet();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public async Task OnDarkModeToggle()
        {
            //
            //  Toggle the switch
            if (IsDarkMode)
            {
                await _preferenceService.SetDarkMode();
            }
            else
            {
                await _preferenceService.UnsetDarkMode();
            }

            await OnThemeSet();
        }

        public async Task OnThemeSet()
        {
            //
            //  Set theme here
            Theme = await _preferenceService.HasDarkModeSet() ? Color.SlateGray : Color.White;

            IsDarkMode = await _preferenceService.HasDarkModeSet();
        }
    }
}