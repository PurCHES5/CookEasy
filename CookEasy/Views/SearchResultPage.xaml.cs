using System;
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
        public SearchResultPage(string query)
        {
            SearchResultPageViewModel.queryText = query;
            InitializeComponent();
            BindingContext = new SearchResultPageViewModel(Navigation);
            result_Label.Text = $"Results for \"{query}\"";
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            RecipeProp recipeProp = new RecipeProp { Name = "Garlic bread" };
            Navigation.PushAsync(new RecipePage(recipeProp.RecipeId));
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var recipeId = (e.CurrentSelection.FirstOrDefault() as RecipeProp).RecipeId;
            if (recipeId == null)
                return;

            await Navigation.PushAsync(new RecipePage(recipeId));
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
    }
}