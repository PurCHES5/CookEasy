﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;

namespace CookEasy.ViewModels
{
    public class SearchPageViewModel : BindableObject
    {
        public SearchPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }

        public ICommand SearchRecipe => new Command<string>((string query) =>
        {
            Console.WriteLine("query" + query);
            Navigation.PushModalAsync(new SearchResultPage());
        });

        private void OnBackButtonClicked()
        {
            Navigation.PushModalAsync(new NavPage());
        }
    }
}