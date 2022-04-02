using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;
using CookEasy.Models;
using System.Collections.ObjectModel;
using CookEasy.Services;
using Acr.UserDialogs;

namespace CookEasy.ViewModels
{
    public class RecipePageViewModel : BindableObject
    {
        public RecipePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);

            MainIngredients = new ObservableCollection<RecipeIngre>();
            OtherIngredients = new ObservableCollection<RecipeIngre>();
            Steps = new ObservableCollection<RecipeIngre>();

            GetRecipeData();
        }

        public INavigation Navigation { get; set; }
        public Command BackButtonClick { get; }

        public ObservableCollection<RecipeIngre> MainIngredients { get; set; }
        public ObservableCollection<RecipeIngre> OtherIngredients { get; set; }
        public ObservableCollection<RecipeIngre> Steps { get; set; }

        private string recipeTitle;
        public string RecipeTitle {
            get => recipeTitle;
            set
            {
                recipeTitle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 0 = Easy, 1 = Advanced
        /// </summary>
        private int Difficulty { get; set; }
        /// <summary>
        /// 0 = Easy, 1 = Advanced, 2 = Both
        /// </summary>
        private int DifficultiesAvail { get; set; }
        public bool EasyAvailable       // these codes are for easier Binding in xaml
        {
            get
            {
                return DifficultiesAvail == 0 || DifficultiesAvail == 2;
            }
        }
        public bool AdvancedAvailable
        {
            get
            {
                return DifficultiesAvail >= 1;
            }
        }
        public int DifficultyEasyBorder
        {
            get
            {
                return Difficulty == 0 ? 6 : 1;
            }
        }
        public int DifficultyAdvancedBorder
        {
            get
            {
                return Difficulty == 1 ? 6 : 1;
            }
        }

        private string cookTime;
        public string CookTime
        {
            get
            {
                return cookTime;
            }
            set
            {
                cookTime = value;
                OnPropertyChanged();
            }
        }

        private string portion;
        public string Portion
        {
            get
            {
                return portion;
            }
            set
            {
                portion = value;
                OnPropertyChanged();
            }
        }

        private Uri recipeImage;
        public Uri RecipeImage
        {
            get
            {
                return recipeImage;
            }
            set
            {
                recipeImage = value;
                OnPropertyChanged();
            }
        }

        async void OnBackButtonClicked()
        {
            await Navigation.PopAsync();
        }

        private async void GetRecipeData()
        {
            var loadingDialog = UserDialogs.Instance.Loading("", show: true, maskType: MaskType.Gradient);
            RecipeDetailData data = (RecipeDetailData)await FirebaseManager.Current.ReadRecipe("000000001");

            RecipeTitle = data.RecipeTitle;
            CookTime = "Cook time: " + data.CookTime;
            Portion = "Portion: " + data.Portion;
            RecipeImage = new Uri(data.ImageUri, UriKind.Absolute);
            DifficultiesAvail = data.DifficultiesAvail;
            Difficulty = data.Difficulty;
            string[] mainIngre = data.MainIngre.Split('/');
            string[] otherIngre = data.OtherIngre.Split('/');
            string[] steps = data.Steps.Split('/');

            foreach (string ingre in mainIngre)
            {
                MainIngredients.Add(new RecipeIngre { Content = ingre });
            }

            foreach (string ingre in otherIngre)
            {
                OtherIngredients.Add(new RecipeIngre { Content = ingre });
            }

            for (int i = 0; i < steps.Length; i++)
            {
                Steps.Add(new RecipeIngre { Content = steps[i], Order = i });
            }

            await Task.Delay(1000);
            loadingDialog.Hide();
        }
    }
}
