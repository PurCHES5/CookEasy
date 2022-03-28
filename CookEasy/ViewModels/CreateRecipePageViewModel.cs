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
using Plugin.Media;

namespace CookEasy.ViewModels
{
    class CreateRecipePageViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeIngre> MainIngredients { get; set; }

        public CreateRecipePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            EasyClicked = new Command(OnEasyClicked);
            AdvancedClicked = new Command(OnAdvancedClicked);
            MainIngredientAdd = new Command(AddMainIngredients);
            MainIngredientDelete = new MvvmHelpers.Commands.Command<int>(DeleteMainIngredient);

            MainIngredients = new ObservableCollection<RecipeIngre>();

            AddMainIngredients();
        }

        public INavigation Navigation { get; set; }

        public Command UploadImage { get; }
        public Command SaveButtonClicked { get; }
        public Command PublishButtonClicked { get; }
        public Command EasyClicked { get; }
        public Command AdvancedClicked { get; }
        public Command MainIngredientAdd { get; }
        public MvvmHelpers.Commands.Command<int> MainIngredientDelete { get; }

        private bool isEasy = true;
        private bool isAdvanced = false;
        private int difficulty;
        public int Difficulty
        {
            get => difficulty;
            set
            {
                if (value == difficulty)
                    return;
                difficulty = value;
                OnPropertyChanged();
            }
        }
        public bool IsEasy
        {
            get => isEasy;
            set
            {
                if (value == isEasy)
                    return;
                isEasy = value;
                OnPropertyChanged();
            }
        }
        public bool IsAdvanced
        {
            get => isAdvanced;
            set
            {
                if (value == isAdvanced)
                    return;
                isAdvanced = value;
                OnPropertyChanged();
            }
        }

        private void OnEasyClicked()
        {
            Difficulty = 0;
            IsEasy = true;
            IsAdvanced = false;
        }

        private void OnAdvancedClicked()
        {
            Difficulty = 1;
            IsEasy = false;
            IsAdvanced = true;
        }

        private void AddMainIngredients()
        {
            if (MainIngredients.Count == 0)
            {
                MainIngredients.Add(new RecipeIngre { Placeholder = "100g cake flour" });
                ReorderList();
                MainIngredients.Add(new RecipeIngre { Placeholder = "4 medium sized eggs" });
                ReorderList();
                MainIngredients.Add(new RecipeIngre { Placeholder = "70g sugar" });
                for (int i = 0; i < MainIngredients.Count; i++)
                {
                    MainIngredients[i].Order = i;
                    MainIngredients[i].Placeholder = i.ToString();
                }

                return;
            }

            MainIngredients.Add(new RecipeIngre { Placeholder = "quantity/weight + ingredient" });
            ReorderList();
        }

        private void DeleteMainIngredient(int index)
        {
            MainIngredients.RemoveAt(index);
            ReorderList();
        }

        private void ReorderList()
        {
            for (int i = 0; i < MainIngredients.Count; i++)
            {
                MainIngredients[i].Order = 0;
                MainIngredients[i].Placeholder = i.ToString();
            }
        }

    }
}
