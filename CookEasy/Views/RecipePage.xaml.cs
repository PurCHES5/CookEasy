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
    public partial class RecipePage : ContentPage
    {
        public RecipePage(RecipeProp recipeRecomms)
        {
            InitializeComponent();
            BindingContext = new RecipePageViewModel(Navigation);
            Title.Text = recipeRecomms.Name;
            TitleLabel.TextColor = Color.FromRgba(0.0, 0.0, 0.0, 0);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;

            var TransY = e.ScrollY;

            if (TransY > 330)
            {
                double alpha = (TransY - 330) / 60;
                TitleLabel.TextColor = Color.FromRgba(0.0, 0.0, 0.0, alpha);
            }
            else
            {
                TitleLabel.TextColor = Color.FromRgba(0.0, 0.0, 0.0, 0);
            }

            if (TransY < 20)
            {
                titleBarShadow.BlurRadius = (float)TransY / 4;
            }
            else
            {
                titleBarShadow.BlurRadius = 5;
            }

            

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}