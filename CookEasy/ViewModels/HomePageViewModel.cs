using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;

namespace CookEasy.ViewModels
{
    public class HomePageViewModel : BindableObject
    {
        public HomePageViewModel(INavigation navigation)
        {
            GoToRecipe = new Command(OnGoToRecipe);
            this.Navigation = navigation;
        }

        public INavigation Navigation { get; set; }

        public ICommand GoToRecipe { get; }

        string recipeText = "Cake";

        public string RecipeText
        {
            get => recipeText;
            set
            {
                if (value == recipeText)
                    return;
                recipeText = value;
                OnPropertyChanged();
            }
        }

        public async void OnGoToRecipe()
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateRecipePage()) { BarBackgroundColor = Color.White });
        }
    }
}
