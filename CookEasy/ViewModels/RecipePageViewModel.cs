using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CookEasy.Views;
using System.Threading.Tasks;
using CookEasy.Models;
using System.Collections.ObjectModel;

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

            Difficulty = 1;
            DifficultiesAvail = 2;
            cookTime = "Cook time: 1 hr 10 mins";
            portion = "Portion: 2 people";

            MainIngredients.Add(new RecipeIngre { Content = "Cream" });

            OtherIngredients.Add(new RecipeIngre { Content = "Sugar" });

            Steps.Add(new RecipeIngre { Content = "Crack the eggs and separate the egg white and yolk. You may want to use a tool to do so. Put them in separated, dry and clean bowls." });
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }

        public ObservableCollection<RecipeIngre> MainIngredients { get; set; }
        public ObservableCollection<RecipeIngre> OtherIngredients { get; set; }
        public ObservableCollection<RecipeIngre> Steps { get; set; }

        /// <summary>
        /// 0 = Easy, 1 = Advanced
        /// </summary>
        public int Difficulty { get; set; }
        /// <summary>
        /// 0 = Easy, 1 = Advanced, 2 = Both
        /// </summary>
        public int DifficultiesAvail { get; set; }
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

        public string cookTime { get; set; }
        public string CookTime
        {
            get
            {
                return cookTime;
            }
        }

        public string portion { get; set; }
        public string Portion
        {
            get
            {
                return portion;
            }
        }

        async void OnGoToRecipe()
        {
            await Navigation.PushAsync(new SearchPage());
        }

        async void OnBackButtonClicked()
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
