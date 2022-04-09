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
using CookEasy.Services;

namespace CookEasy.ViewModels
{
    public class SearchResultPageViewModel : BaseViewModel
    {
        public static string queryText;
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

        private async void LoadCategoryRecomms()
        {
            var result = await FirebaseManager.Current.ReadRecipeProps(queryText, RecipeRusult.Count + 4);

            try
            {
                List<RecipePropData> datas = (List<RecipePropData>)result;

                if (((List<RecipePropData>)result).Count <= RecipeRusult.Count)
                    return;

                if (((List<RecipePropData>)result).Count > RecipeRusult.Count + 4)
                {
                    for (int i = 0; i < RecipeRusult.Count + 4; i++)
                    {
                        datas.RemoveRange(0, RecipeRusult.Count + 4);
                    }
                }

                foreach (var data in datas)
                {
                    RecipeRusult.Add(new RecipeProp { Name = data.RecipeTitle, Image = data.ImageUri, Difficulty = data.Difficulty, DifficultiesAvail = data.DifficultiesAvail, RecipeId = data.RecipeId, Likes = data.Likes });
                }
            }
            catch
            {
                return;
            }
        }
    }
}
