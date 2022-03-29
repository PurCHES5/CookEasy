using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;

namespace CookEasy.ViewModels
{
    public class RecipePageViewModel : BindableObject
    {
        public RecipePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }

        async void OnGoToRecipe()
        {
            await Navigation.PushAsync(new SearchPage());
        }

        async void OnBackButtonClicked()
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
