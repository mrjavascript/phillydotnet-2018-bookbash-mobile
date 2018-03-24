using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBash.Services;
using BookBash.Services.Impl;
using BookBash.ViewModels;
using BookBash.Views;
using CommonServiceLocator;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services;
using Prism.Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;
using DependencyService = Xamarin.Forms.DependencyService;

namespace BookBash
{
	public partial class App : PrismApplication
    {
        public static string AppName => "Book Bash";

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //
            //  Xamarin
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            //
            //  Services
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();
            containerRegistry.RegisterSingleton<IBacklogService, BacklogService>();
            containerRegistry.RegisterSingleton<IPreferenceService, PreferenceService>();

            //
            //  View Models
            containerRegistry.RegisterForNavigation<DashboardPage>();
            containerRegistry.RegisterForNavigation<AccountCreatePage, AccountCreateViewModel>();
            containerRegistry.RegisterForNavigation<AccountLoginPage, AccountLoginViewModel>();
            containerRegistry.RegisterForNavigation<ViewBacklogPage, ViewBacklogViewModel>();
            containerRegistry.RegisterForNavigation<AddEditBacklogPage, AddEditBacklogViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfileVIewModel>();
            containerRegistry.RegisterForNavigation<FacebookPage, FacebookViewModel>();
        }

        protected override void OnInitialized()
        {
            var credentialService = DependencyService.Get<ICredentialsService>();
            NavigationService.NavigateAsync(credentialService.DoCredentialsExist()
                ? new Uri("/NavigationPage/DashboardPage", UriKind.Absolute)
                : new Uri("/NavigationPage/AccountCreatePage", UriKind.Absolute));
        }

        public async void SuccessfulFacebookLogin(string email)
        {
            var accountService = Container.Resolve<IAccountService>();
            try
            {
                await accountService.FacebookAccountLogin(email);
                await NavigationService.NavigateAsync(new Uri("/NavigationPage/DashboardPage", UriKind.Absolute));
            }
            catch (Exception ex)
            {
                var pageDialogService = Container.Resolve<IPageDialogService>();
                await pageDialogService.DisplayAlertAsync("Error", $"Errors: {ex.Message}", "OK");
                CancelFacebookLogin();
            }
        }

        public async void CancelFacebookLogin()
        {
            //
            //  Go back to account creation page
            await NavigationService.GoBackAsync(); // back to LoginModalPage
            await NavigationService.GoBackAsync(); // back to the last page before LoginModalPage
        }

        public async Task HandleWidgetAction()
        {
            var credentialService = DependencyService.Get<ICredentialsService>();

            //
            //  unable to resume activity ???   prism error

//            await NavigationService.NavigateAsync(credentialService.DoCredentialsExist()
//                ? new Uri("/NavigationPage/DashboardPage", UriKind.Absolute)
//                : new Uri("/NavigationPage/AccountCreatePage", UriKind.Absolute));
            if (credentialService.DoCredentialsExist())
            {
                await Current.MainPage.Navigation.PushAsync(new DashboardPage());
            }
            else
            {
                await Current.MainPage.Navigation.PushAsync(new AccountCreatePage());
            }
        }
    }
}
