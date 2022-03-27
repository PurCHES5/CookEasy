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
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeProp> RecipeRecomms { get; set; }

        public HomePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            GoToRecipe = new Command(OnGoToRecipe);
            RefreshCommand = new AsyncCommand(Refresh);

            RecipeRecomms = new ObservableRangeCollection<RecipeProp>();
        }

        public INavigation Navigation { get; set; }
        public ICommand GoToRecipe { get; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

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

        async void OnGoToRecipe()
        {
            await Navigation.PushModalAsync(new SearchPage());
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            RecipeRecomms.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            var image = "TestRecipeImage.jpeg";
            RecipeRecomms.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image });
        }
    }
}
