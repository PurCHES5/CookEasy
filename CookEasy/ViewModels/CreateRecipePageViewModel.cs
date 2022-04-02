﻿using System;
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
using CookEasy.Services;
using Acr.UserDialogs;

namespace CookEasy.ViewModels
{
    class CreateRecipePageViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeIngre> MainIngredients { get; set; }
        public ObservableCollection<RecipeIngre> OtherIngredients { get; set; }
        public ObservableCollection<RecipeIngre> Steps { get; set; }

        public CreateRecipePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            EasyClicked = new Command(OnEasyClicked);
            AdvancedClicked = new Command(OnAdvancedClicked);
            MainIngredientAdd = new Command(AddMainIngredients);
            MainIngredientDelete = new MvvmHelpers.Commands.Command<int>(DeleteMainIngredient);
            OtherIngredientAdd = new Command(AddOtherIngredients);
            OtherIngredientDelete = new MvvmHelpers.Commands.Command<int>(DeleteOtherIngredient);
            StepAdd = new Command(AddSteps);
            StepDelete = new MvvmHelpers.Commands.Command<int>(DeleteStep);
            UploadImage = new Command(ChooseRecipePhoto);

            MainIngredients = new ObservableCollection<RecipeIngre>();
            OtherIngredients = new ObservableCollection<RecipeIngre>();
            Steps = new ObservableCollection<RecipeIngre>();

            recipeImage = "upload_image.png";

            AddMainIngredients();
            AddOtherIngredients();
            AddSteps();
        }

        public INavigation Navigation { get; set; }

        public Command UploadImage { get; }
        public Command SaveButtonClicked { get; }
        public Command PublishButtonClicked { get; }
        public Command EasyClicked { get; }
        public Command AdvancedClicked { get; }
        public Command MainIngredientAdd { get; }
        public MvvmHelpers.Commands.Command<int> MainIngredientDelete { get; }
        public Command OtherIngredientAdd { get; }
        public MvvmHelpers.Commands.Command<int> OtherIngredientDelete { get; }
        public Command StepAdd { get; }
        public MvvmHelpers.Commands.Command<int> StepDelete { get; }

        private ImageSource recipeImage;
        public ImageSource RecipeImage { 
            get => recipeImage; 
            set 
            {
                if (value == recipeImage)
                    return;
                recipeImage = value;
                OnPropertyChanged(); 
            } 
        }
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
                MainIngredients.Add(new RecipeIngre { Placeholder = "4 medium sized eggs" });
                MainIngredients.Add(new RecipeIngre { Placeholder = "70g sugar" });
                ReorderList();

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

        private void AddOtherIngredients()
        {
            if (OtherIngredients.Count == 0)
            {
                OtherIngredients.Add(new RecipeIngre { Placeholder = "2g baking powder" });
                OtherIngredients.Add(new RecipeIngre { Placeholder = "1ml vanilla extract" });
                ReorderList();

                return;
            }

            OtherIngredients.Add(new RecipeIngre { Placeholder = "quantity/weight + ingredient" });
            ReorderList();
        }

        private void DeleteOtherIngredient(int index)
        {
            OtherIngredients.RemoveAt(index);
            ReorderList();
        }

        private void AddSteps()
        {
            if (Steps.Count == 0)
            {
                Steps.Add(new RecipeIngre { Placeholder = "Crack the eggs and separate the egg white and yolk. You may want to use a tool to do so. Put them in separated, dry and clean bowls.\n" });
                Steps.Add(new RecipeIngre { Placeholder = "Take 50g sugar from the 70g sugar. Split them into 3 equal portions and start whipping the egg white. Add one portion of sugar after every 30 seconds of whipping.\n" });
                Steps.Add(new RecipeIngre { Placeholder = "Now the whipped yolk should be very creamy. Put the rest 20g of sugar into egg yolk and whip it for 30 seconds. Then gently pour the egg yolk into the whipped egg white.\n" });
                ReorderList();

                return;
            }

            Steps.Add(new RecipeIngre { Placeholder = "Detailed explaination for each step" });
            ReorderList();
        }

        private void DeleteStep(int index)
        {
            Steps.RemoveAt(index);
            ReorderList();
        }

        private void ReorderList()
        {
            for (int i = 0; i < MainIngredients.Count; i++)
            {
                MainIngredients[i] = new RecipeIngre { Placeholder = MainIngredients[i].Placeholder, Content = MainIngredients[i].Content, Order = i };
            }

            for (int i = 0; i < OtherIngredients.Count; i++)
            {
                OtherIngredients[i] = new RecipeIngre { Placeholder = OtherIngredients[i].Placeholder, Content = OtherIngredients[i].Content, Order = i };
            }

            for (int i = 0; i < Steps.Count; i++)
            {
                Steps[i] = new RecipeIngre { Placeholder = Steps[i].Placeholder, Content = Steps[i].Content, Order = i };
            }
        }

        private async void ChooseRecipePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await UserDialogs.Instance.AlertAsync("No Camera", ":( No camera available.", "OK");
                return;
            }

            string act = await UserDialogs.Instance.ActionSheetAsync("Choose or take photo?", "Cancel", null, null, new string[2] { "Take a photo", "Choose from device" });

            if (act == null || act == "Cancel")
                return;

            Plugin.Media.Abstractions.MediaFile file = null;

            if (act == "Take a photo")
            {
                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Name = "recipe_photo.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Large,
                    CompressionQuality = 90
                });
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Large,
                    CompressionQuality = 90
                });
            }

            if (file == null)
                return;

            var stream = file.GetStream();

            RecipeImage = ImageSource.FromStream(() =>
            {
                return stream;
            });

            var loadingDialog = UserDialogs.Instance.Loading("Uploading Photo...", () =>
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.AlertAsync("Upload Cancelled", "Error in uploading recipe photo", "OK");
                RecipeImage = "upload_image.png";       // reset the image in case the user thought they already uploaded the photo
                return;
            }, "Cancel", true, MaskType.Gradient);

            string result = await FirebaseManager.Current.UploadToStorage(file, "recipe");

            loadingDialog.Hide();

            if (!result.StartsWith("https://firebasestorage.googleapis.com/"))
            {
                await UserDialogs.Instance.AlertAsync(result, "Error in uploading recipe photo", "OK");
                RecipeImage = "upload_image.png";       // reset the image in case the user thought they already uploaded the photo
            }
            else
            {
                // provide positive feedback
                await UserDialogs.Instance.AlertAsync("Your recipe photo has been successfully uploaded to cloud!", "Upload Image", "Great!");
            }
        }

    }
}