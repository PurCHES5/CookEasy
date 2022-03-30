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
using Acr.UserDialogs;

namespace CookEasy.ViewModels
{
    class StartPagesViewModel : BaseViewModel
    {
        public StartPagesViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            LoginClicked = new Command(OnLoginClicked);
            ForgotPasswordClicked = new Command(OnForgotPasswordClicked);
            GoToRegisterPage = new Command(OnGoToRegisterPage);
            FacebookLoginClicked = new Command(OnFacebookLoginClicked);
            GoogleLoginClicked = new Command(OnGoogleLoginClicked);
            RegisterClicked = new Command(OnRegisterClicked);
            GoToLoginPage = new Command(OnGoToLoginPage);
            MeatLoverClicked = new Command(OnMeatLoverClicked);
            VegtarainClicked = new Command(OnVegtarainClicked);
        }

        public INavigation Navigation { get; set; }

        public Command LoginClicked { get; }
        public Command ForgotPasswordClicked { get; }
        public Command GoToRegisterPage { get; }
        public Command FacebookLoginClicked { get; }
        public Command GoogleLoginClicked { get; }
        public Command RegisterClicked { get; }
        public Command GoToLoginPage { get; }
        public Command MeatLoverClicked { get; }
        public Command VegtarainClicked { get; }

        private async void OnLoginClicked()
        {
            // verification
            await Navigation.PushAsync(new StarterPage());
        }

        private async void OnForgotPasswordClicked()
        {
            // verification
            await Navigation.PushAsync(new ForgotPswdPage());
        }

        private async void OnGoToRegisterPage()
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnFacebookLoginClicked()
        {
            // verification
        }

        private async void OnGoogleLoginClicked()
        {
            // verification
        }

        private async void OnRegisterClicked()
        {
            // sync data
            await Navigation.PushAsync(new StarterPage());
        }

        private async void OnGoToLoginPage()
        {
            await Navigation.PopAsync();
        }

        private async void OnMeatLoverClicked()
        {
            // sync data
            var dialog = UserDialogs.Instance.Loading("Loading the content...\nPlease wait", show: true, maskType: MaskType.Gradient);
            await Navigation.PushAsync(new NavPage());

            for (int i = 0; i < Navigation.NavigationStack.Count; i++)      // clear NavigationStack so that in the actual logged in pages
            {                                                               // can just call Navigation.PopToRootAsync to go back to "home page"
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            if (Navigation.NavigationStack.Count > 1)       // for some unknown reasons sometimes NavigationStack is not cleared
            {                                               // thoroughly, so just add an if condition to make sure is cleaned
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }

            dialog.Hide();
        }

        private async void OnVegtarainClicked()
        {
            // sync data
            var dialog = UserDialogs.Instance.Loading("Loading the content...\nPlease wait", show: true, maskType: MaskType.Gradient);
            await Navigation.PushAsync(new NavPage());

            for (int i = 0; i < Navigation.NavigationStack.Count; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            if (Navigation.NavigationStack.Count > 1)
            {
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }

            dialog.Hide();
        }
    }
}
