﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobiusTest
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WinPage : ContentPage
	{
		public WinPage ()
		{
			InitializeComponent ();
		}
        private void RButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartPage();
        }
    }
}