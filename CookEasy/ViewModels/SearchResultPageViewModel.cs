using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;

namespace CookEasy.ViewModels
{
    public class SearchResultPageViewModel : BindableObject
    {
        public SearchResultPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);
            GoToRecipe = new Command(OnGoToRecipe);
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }
        public ICommand GoToRecipe { get; }

        async void OnBackButtonClicked()
        {
            await Navigation.PushModalAsync(new SearchPage());
        }

        async void OnGoToRecipe()
        {
            await Navigation.PushModalAsync(new SearchPage());
        }
    }
}
