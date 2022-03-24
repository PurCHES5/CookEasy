using System;
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

            //SearchRecipe = new Command(OnSearchRecipe);
        }

        public INavigation Navigation { get; set; }
        //public ICommand SearchRecipe { get; }

        private string searchRecipeText;
        public string SearchRecipeText
        {
            get => searchRecipeText;
            set
            {
                if (value == searchRecipeText)
                    return;
                searchRecipeText = value;
                OnPropertyChanged();
            }
        }

        //async void OnSearchRecipe()
        //{
        //    Console.WriteLine(searchRecipeText);
        //    await Navigation.PushModalAsync(new LoginPage());
        //}

        public ICommand SearchRecipe => new Command<string>((string query) =>
        {
            Console.WriteLine("query" + query);
            //Navigation.PushModalAsync(new LoginPage());
        });
    }
}
