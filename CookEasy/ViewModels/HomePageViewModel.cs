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
using Acr.UserDialogs;

namespace CookEasy.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeProp> RecipeRecomms { get; set; }
        public ObservableCollection<RecipeProp> CategoryRecommsA { get; set; }
        public ObservableCollection<RecipeProp> CategoryRecommsB { get; set; }
        public ObservableCollection<RecipeProp> CategoryRecommsC { get; set; }

        public HomePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            GoToRecipe = new Command(OnGoToRecipe);
            RefreshCommand = new AsyncCommand(Refresh);

            RecipeRecomms = new ObservableRangeCollection<RecipeProp>();
            CategoryRecommsA = new ObservableCollection<RecipeProp>();
            CategoryRecommsB = new ObservableCollection<RecipeProp>();
            CategoryRecommsC = new ObservableCollection<RecipeProp>();
            GoToRecipe = new Command(OnGoToRecipe);
            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new Command(LoadMoreRecipeRecomms);

            LoadMoreRecipeRecomms();
            LoadCategoryRecomms(CategoryRecommsA);
            LoadCategoryRecomms(CategoryRecommsB);
            LoadCategoryRecomms(CategoryRecommsC);
        }

        public INavigation Navigation { get; set; }
        public ICommand GoToRecipe { get; }

        public AsyncCommand RefreshCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command ClearCommand { get; }
        
        /// <summary>
        /// a set of colors to be randomly assigned to the top recommendation cards
        /// </summary>
        public string[] cardColors = new string[9] {
            "#faced0", "#c74a6d", "#88697c", "#6da6bf", "#cee0f4", "#99c0c3", "#8cb173", "#cfce94", "#ffde99" };

        async void OnGoToRecipe()
        {
            var dialog = UserDialogs.Instance.Loading("", maskType: MaskType.Clear);
            await Navigation.PushAsync(new SearchPage());
            dialog.Hide();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            RecipeRecomms.Clear();
            CategoryRecommsA.Clear();
            CategoryRecommsB.Clear();
            CategoryRecommsC.Clear();
            LoadMoreRecipeRecomms();
            LoadCategoryRecomms(CategoryRecommsA);
            LoadCategoryRecomms(CategoryRecommsB);
            LoadCategoryRecomms(CategoryRecommsC);

            IsBusy = false;
        }

        void LoadMoreRecipeRecomms()
        {
            if (RecipeRecomms.Count >= 8)
                return;

            var image = "TestRecipeImage.jpeg";
            RecipeRecomms.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image, Difficulty = 1, DifficultiesAvail = 2, CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 0, CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 2,  CardColor = RandomColorPicker() });
            RecipeRecomms.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image, Difficulty = 1, DifficultiesAvail = 1, CardColor = RandomColorPicker() });
        }

        void LoadCategoryRecomms(ObservableCollection<RecipeProp> collection)
        {
            var image = "TestRecipeImage.jpeg";
            collection.Add(new RecipeProp { Name = "Sip of Sunshine", Image = image, Difficulty = 1, DifficultiesAvail = 2, Likes = 129 });
            collection.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 0, Likes = 82 });
            collection.Add(new RecipeProp { Name = "Potent Potable", Image = image, Difficulty = 0, DifficultiesAvail = 2, Likes = 14 });
            collection.Add(new RecipeProp { Name = "Kenya Kiambu Handege", Image = image, Difficulty = 1, DifficultiesAvail = 1 });
        }

        string RandomColorPicker()
        {
            Random random = new Random();
            return cardColors[random.Next(0, cardColors.Length)];
        }
    }
}
