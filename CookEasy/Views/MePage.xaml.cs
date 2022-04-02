using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookEasy.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace CookEasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MePage : ContentPage
    {
        public MePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string photoUri = await FirebaseManager.Current.GetAvatarPhoto();

            if (photoUri.StartsWith("https://firebasestorage.googleapis.com/"))
            {
                avatar_image.Source = ImageSource.FromUri(new Uri(photoUri, UriKind.Absolute));
            }
        }

        async void OnUploadImage(object sender, EventArgs e)
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
                    Name = "avatar_photo.jpg",
                    SaveToAlbum = true,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    CompressionQuality = 90
                });
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    CompressionQuality = 90
                });
            }

            if (file == null)
                return;

            var stream = file.GetStream();

            var loadingDialog = UserDialogs.Instance.Loading("Uploading Photo...", () =>
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.AlertAsync("Upload Cancelled", "Error in uploading recipe photo", "OK");
                avatar_image.Source = "upload_image.png";       // reset the image in case the user thought they already uploaded the photo
                return;
            }, "Cancel", true, MaskType.Gradient);

            string result = await FirebaseManager.Current.UploadToStorage(file, "avatar");

            loadingDialog.Hide();

            if (result.StartsWith("https://firebasestorage.googleapis.com/"))
            {
                // provide positive feedback
                await UserDialogs.Instance.AlertAsync("Profile picture successfully uploaded to cloud!", "Upload Image", "Great!");
                avatar_image.Source = ImageSource.FromUri(new Uri(result, UriKind.Absolute));
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(result, "Error", "OK");
            }
        }

        private void SignOut_Tapped(object sender, EventArgs e)
        {
            FirebaseManager.Current.SignOut();
            for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)      // clear NavigationStack so that in the actual logged in pages
            {                                                               // can just call Navigation.PopToRootAsync to go back to "home page"
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            if (Navigation.NavigationStack.Count > 1)       // for some unknown reasons sometimes NavigationStack is not cleared
            {                                               // thoroughly, so just add an if condition to make sure is cleaned
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            Navigation.PushAsync(new LoginPage());
        }
    }
}