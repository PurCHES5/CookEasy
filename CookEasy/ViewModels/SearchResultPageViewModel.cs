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
    public class SearchResultPageViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeProp> RecipeRusult { get; set; }

        public SearchResultPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);
            GoToRecipe = new Command(OnGoToRecipe);
            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new Command(LoadCategoryRecomms);

            RecipeRusult = new ObservableCollection<RecipeProp>();

            LoadCategoryRecomms();
        }

        public INavigation Navigation { get; set; }
        public Command BackButtonClick { get; }
        public Command GoToRecipe { get; }

        public AsyncCommand RefreshCommand { get; }

        public Command LoadMoreCommand { get; }

        async void OnBackButtonClicked()
        {
            await Navigation.PopToRootAsync();
        }

        async void OnGoToRecipe()
        {
            await Navigation.PopAsync();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            RecipeRusult.Clear();
            LoadCategoryRecomms();

            IsBusy = false;
        }

        private void LoadCategoryRecomms()
        {
            var image = "mashedpotato.jpeg";
            RecipeRusult.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image, Difficulty = 1, DifficultiesAvail = 2, Likes = 129 });
            RecipeRusult.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 0, Likes = 82 });
            RecipeRusult.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 2, Likes = 14 });
            RecipeRusult.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image, Difficulty = 1, DifficultiesAvail = 1 });
            RecipeRusult.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image, Difficulty = 1, DifficultiesAvail = 2, Likes = 129 });
            RecipeRusult.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 0, Likes = 82 });
            RecipeRusult.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 2, Likes = 14 });
            RecipeRusult.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image, Difficulty = 1, DifficultiesAvail = 1 });
        }
    }
}
