using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;

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

        public bool isRefreshingPage;

        async void OnGoToRecipe()
        {
            await Navigation.PushModalAsync(new SearchPage());
        }

        public async void OnRefreshingPage()
        {
            // refresh page content, call this method from code behind
        }
    }
}
