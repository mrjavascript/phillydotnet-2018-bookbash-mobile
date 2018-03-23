﻿using System;
using BookBash.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountCreatePage : ContentPage
	{
		public AccountCreatePage ()
		{
			InitializeComponent ();
		    BindingContext = new AccountCreateViewModel(Navigation);
		}
	}
}