using System;
using BookBash.ViewModels;
using BookBash.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

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
            //  xamarin
            containerRegistry.RegisterSingleton<TabbedPage>();
            containerRegistry.RegisterSingleton<NavigationPage>();

            //
            //  view models
            containerRegistry.RegisterForNavigation<AccountCreatePage, AccountCreateViewModel>();
	        containerRegistry.RegisterForNavigation<AccountLoginPage, AccountLoginViewModel>();
	        containerRegistry.RegisterForNavigation<ViewBacklogPage, ViewBacklogViewModel>();
	        containerRegistry.RegisterForNavigation<AddEditBacklogPage, AddEditBacklogViewModel>();
	        containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>();
        }

	    protected override void OnInitialized()
	    {
	        NavigationService.NavigateAsync(new Uri("/NavigationPage/AccountCreatePage", UriKind.Absolute));
	    }
	}
}
