﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBash.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookBash.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditBacklogPage : ContentPage
	{
		public AddEditBacklogPage ()
		{
		    InitializeComponent();
//            BindingContext = new AddEditBacklogViewModel(Navigation);
        }
	}
}