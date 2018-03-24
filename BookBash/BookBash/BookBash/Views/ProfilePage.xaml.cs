using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBash.ViewModels;
using Prism;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
		    InitializeComponent();
//            BindingContext = new ProfileVIewModel(Navigation);
        }

        //
        //  behaviors???????  commands!
	    private async void OnDarkModeToggle(object sender, ToggledEventArgs e)
	    {
	        if (!(sender is Switch switchElement)) return;
	        if (BindingContext is ProfileVIewModel view) await view.OnDarkModeToggle();
        }
	}
}