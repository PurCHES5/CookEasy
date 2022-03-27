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
<<<<<<< HEAD
            this.Navigation = navigation;

            GoToRecipe = new Command(OnGoToRecipe);
            RefreshCommand = new AsyncCommand(Refresh);

            RecipeRecomms = new ObservableRangeCollection<RecipeProp>();
=======
            GoToRecipe = new Command(OnGoToRecipe);
<<<<<<< Updated upstream
            this.Navigation = navigation;
=======
            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new Command(LoadMore);

            RecipeRecomms = new ObservableRangeCollection<RecipeProp>();

            LoadMore();
>>>>>>> Stashed changes
>>>>>>> merger
        }

        public INavigation Navigation { get; set; }
        public ICommand GoToRecipe { get; }

<<<<<<< HEAD
=======
<<<<<<< Updated upstream
=======
>>>>>>> merger
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
<<<<<<< HEAD
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

=======
        public Command ClearCommand { get; }

        public string[] cardColors = new string[9] {
            "#faced0", "#c74a6d", "#88697c", "#6da6bf", "#cee0f4", "#99c0c3", "#8cb173", "#cfce94", "#ffde99" };

>>>>>>> Stashed changes
>>>>>>> merger
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
<<<<<<< HEAD
            var image = "TestRecipeImage.jpeg";
            RecipeRecomms.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image });
=======
<<<<<<< Updated upstream
            // refresh page content, call this method from code behind
=======
            if (RecipeRecomms.Count >= 8)
                return;

            var image = "TestRecipeImage.jpeg";
            RecipeRecomms.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image, Difficulty = 1, DifficultiesAvail = 2, Likes = 129, CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 0, Likes = 82, CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 2, Likes = 14, CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image, Difficulty = 1, DifficultiesAvail = 1, CardColor = RandomColorPicker() });
        }

        string RandomColorPicker()
        {
            Random random = new Random();
            return cardColors[random.Next(0, cardColors.Length)];
>>>>>>> Stashed changes
>>>>>>> merger
        }
    }
}
