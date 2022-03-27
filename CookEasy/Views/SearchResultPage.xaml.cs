﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookEasy.ViewModels;
using CookEasy.Models;

namespace CookEasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        public SearchResultPage()
        {
            InitializeComponent();
            BindingContext = new SearchResultPageViewModel(Navigation);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;

            var TransY = e.ScrollY;

            if (TransY < 20)
            {
                titleBarShadow.BlurRadius = (float)TransY / 4;
            }
            else
            {
                titleBarShadow.BlurRadius = 5;
            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            RecipeProp recipeProp = new RecipeProp { Name = "Garlic bread" };
            Navigation.PushModalAsync(new RecipePage(recipeProp));
        }

        private async void Back_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavPage());
        }
    }
}