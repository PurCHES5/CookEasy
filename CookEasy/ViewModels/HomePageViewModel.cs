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
using CookEasy.Services;

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
            BakedMore = new Command(OnBakedMore);
            AsianMore = new Command(OnAsianMore);
            EasyMore = new Command(OnEasyMore);

            LoadMoreRecipeRecomms();
            LoadCategoryRecomms(CategoryRecommsA, "Bake");
            LoadCategoryRecomms(CategoryRecommsB, "Asian");
            LoadCategoryRecomms(CategoryRecommsC, "Easy");
        }

        public INavigation Navigation { get; set; }
        public ICommand GoToRecipe { get; }

        public AsyncCommand RefreshCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command ClearCommand { get; }
        public Command BakedMore { get; }
        public Command AsianMore { get; }
        public Command EasyMore { get; }
        
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
            LoadCategoryRecomms(CategoryRecommsA, "Bake");
            LoadCategoryRecomms(CategoryRecommsB, "Asian");
            LoadCategoryRecomms(CategoryRecommsC, "Easy");

            IsBusy = false;
        }

        async void LoadMoreRecipeRecomms()
        {
            if (RecipeRecomms.Count >= 8)
                return;

            var result = await FirebaseManager.Current.ReadRecipeProps(RecipeRecomms.Count + 4);

            try
            {
                if (RecipeRecomms.Count >= 8)
                    return;

                List<RecipePropData> datas = (List<RecipePropData>)result;

                if (((List<RecipePropData>)result).Count == 8)
                {
                     datas.RemoveRange(0, 4);
                }

                foreach (var data in datas)
                {
                    RecipeRecomms.Add(new RecipeProp { Name = data.RecipeTitle, Image = data.ImageUri, Difficulty = data.Difficulty, DifficultiesAvail = data.DifficultiesAvail, RecipeId = data.RecipeId, CardColor = RandomColorPicker() });
                }
            }
            catch
            {
                return;
            }
        }

        async void LoadCategoryRecomms(ObservableCollection<RecipeProp> collection, string queryText)
        {
            var result = await FirebaseManager.Current.ReadRecipeProps(queryText, 4);

            try
            {
                List<RecipePropData> datas = (List<RecipePropData>)result;
                foreach (var data in datas)
                {
                    collection.Add(new RecipeProp { Name = data.RecipeTitle, Image = data.ImageUri, Difficulty = data.Difficulty, DifficultiesAvail = data.DifficultiesAvail, RecipeId = data.RecipeId, Likes = data.Likes });
                }
            }
            catch
            {
                return;
            }
        }

        string RandomColorPicker()
        {
            Random random = new Random();
            return cardColors[random.Next(0, cardColors.Length)];
        }

        void OnBakedMore()
        {
            Navigation.PushAsync(new SearchResultPage("Baked"));
        }

        void OnAsianMore()
        {
            Navigation.PushAsync(new SearchResultPage("Asian"));
        }

        void OnEasyMore()
        {
            Navigation.PushAsync(new SearchResultPage("Easy"));
        }
    }
}
