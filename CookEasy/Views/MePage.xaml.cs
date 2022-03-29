﻿using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookEasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MePage : ContentPage
    {
        public MePage()
        {
            InitializeComponent();
        }

        async void OnUploadImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            string act = await DisplayActionSheet("Choose or take photo?", "Cancel", null, new string[2] { "Take a photo", "Choose from device" });

            if (act == null || act == "Cancel")
                return;

            Plugin.Media.Abstractions.MediaFile file = null;

            if (act == "Take a photo")
            {
                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
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

            avatar_image.Source = ImageSource.FromFile(file.Path);
        }

        private void SignOut_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}