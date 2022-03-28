using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;
using Command = MvvmHelpers.Commands.Command;
using MvvmHelpers;
using MvvmHelpers.Commands;
using CookEasy.Models;
using System.Collections.ObjectModel;

namespace CookEasy.ViewModels
{
    public class SearchPageViewModel : BindableObject
    {
        public ObservableCollection<RecipeProp> Categories { get; set; }
        public SearchPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);

            Categories = new ObservableRangeCollection<RecipeProp>();

            LoadCategories();
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }

        public ICommand SearchRecipe => new Xamarin.Forms.Command<string>((string query) =>
        {
            Navigation.PushModalAsync(new SearchResultPage(query));
        });

        private void OnBackButtonClicked()
        {
            Navigation.PushModalAsync(new NavPage());
        }

        void LoadCategories()
        {
            var image = "TestRecipeImage.jpeg";
            Categories.Add(new RecipeProp { Name = "Western", Image = image });
            Categories.Add(new RecipeProp { Name = "Chinese", Image = image });
            Categories.Add(new RecipeProp { Name = "Indian", Image = image });
            Categories.Add(new RecipeProp { Name = "Seafood", Image = image });
            Categories.Add(new RecipeProp { Name = "Super Easy", Image = image });
            Categories.Add(new RecipeProp { Name = "Baking", Image = image });
            Categories.Add(new RecipeProp { Name = "Seasonal", Image = image });
            Categories.Add(new RecipeProp { Name = "Kid Friendly", Image = image });
        }
    }
}
