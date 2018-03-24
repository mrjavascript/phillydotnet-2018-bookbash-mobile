using System;
using BookBash.Models;
using BookBash.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewBacklogPage : ContentPage
	{
		public ViewBacklogPage ()
		{
		    InitializeComponent();
//            BindingContext = new ViewBacklogViewModel(Navigation);
		}

	    private async void OnMore(object sender, EventArgs e)
	    {
	        if (!(sender is MenuItem menu)) return;
	        var item = menu.CommandParameter as BacklogItem;
	        if (BindingContext is ViewBacklogViewModel view) await view.OnEditItem(item);
	    }

	    private async void OnDelete(object sender, EventArgs e)
	    {
	        if (!(sender is MenuItem menu)) return;
	        var item = menu.CommandParameter as BacklogItem;
	        if (BindingContext is ViewBacklogViewModel view) await view.OnDeleteItem(item);
	    }
	}
}