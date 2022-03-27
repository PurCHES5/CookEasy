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
<<<<<<< Updated upstream
            this.Navigation = navigation;
=======
            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new Command(LoadMore);

            RecipeRecomms = new ObservableRangeCollection<RecipeProp>();

            LoadMore();
>>>>>>> Stashed changes
        }

        public INavigation Navigation { get; set; }
        public ICommand GoToRecipe { get; }

<<<<<<< Updated upstream
=======
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public string[] cardColors = new string[9] {
            "#faced0", "#c74a6d", "#88697c", "#6da6bf", "#cee0f4", "#99c0c3", "#8cb173", "#cfce94", "#ffde99" };

>>>>>>> Stashed changes
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
        }
    }
}
