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
using CookEasy.Services;

namespace CookEasy.ViewModels
{
    class StartPagesViewModel : BaseViewModel
    {
        public StartPagesViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            LoginClicked = new Command(OnLoginClicked);
            GoToForgotPasswordPage = new Command(OnGoToForgotPasswordPage);
            GoToRegisterPage = new Command(OnGoToRegisterPage);
            FacebookLoginClicked = new Command(OnFacebookLoginClicked);
            GoogleLoginClicked = new Command(OnGoogleLoginClicked);
            RegisterClicked = new Command(OnRegisterClicked);
            GoToLoginPage = new Command(OnGoToLoginPage);
            MeatLoverClicked = new Command(OnMeatLoverClicked);
            VegtarainClicked = new Command(OnVegtarainClicked);
            ForgotPasswordClicked = new Command(OnForgotPasswordClicked);
        }

        public INavigation Navigation { get; set; }

        public Command LoginClicked { get; }
        public Command GoToForgotPasswordPage { get; }
        public Command GoToRegisterPage { get; }
        public Command FacebookLoginClicked { get; }
        public Command GoogleLoginClicked { get; }
        public Command RegisterClicked { get; }
        public Command GoToLoginPage { get; }
        public Command MeatLoverClicked { get; }
        public Command VegtarainClicked { get; }
        public Command ForgotPasswordClicked { get;  }

        public bool RegisterChecked { get; set; }
        public string RegisterEmail { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterConfirmPassword { get; set; }

        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }

        public string ForgotPasswordEmail { get; set; }

        private async void OnLoginClicked()
        {
            string result = await FirebaseManager.Current.SignInEmail(LoginEmail, LoginPassword);

            if (result == "True")
            {
                await Navigation.PushAsync(new StarterPage());
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(result, "Alert", "Ok");
            }
        }

        private async void OnForgotPasswordClicked()
        {
            string result = await FirebaseManager.Current.ForgotPassword(ForgotPasswordEmail);

            if (result == "True")
            {
                await UserDialogs.Instance.AlertAsync("A password reset email has been sent to this account.", "Alert", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(result, "Alert", "OK");
            }
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
            // check if the password is same as confirm password
            if (RegisterPassword != RegisterConfirmPassword)
            {
                await UserDialogs.Instance.AlertAsync("Password is not same as your Confirm Password.", "Alert", "Try Again");
                return;
            }
            
            // check whether the accept aggrement box is ticked
            if (!RegisterChecked)
            {
                await UserDialogs.Instance.AlertAsync("Please accept our aggrements first to continue registering.", "Alert", "OK");
                return;
            }

            string result = await FirebaseManager.Current.RegisterAccount(RegisterEmail, RegisterPassword);

            if (result == "True")
            {
                await Navigation.PushAsync(new StarterPage());

                for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)      // clear NavigationStack so that in the actual logged in pages
                {                                                               // can just call Navigation.PopToRootAsync to go back to "home page"
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
                if (Navigation.NavigationStack.Count > 1)       // for some unknown reasons sometimes NavigationStack is not cleared
                {                                               // thoroughly, so just add an if condition to make sure is cleaned
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(result, "Alert", "OK");
            }
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

            for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)      // clear NavigationStack so that in the actual logged in pages
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

            for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)      // clear NavigationStack so that in the actual logged in pages
            {                                                               // can just call Navigation.PopToRootAsync to go back to "home page"
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            if (Navigation.NavigationStack.Count > 1)       // for some unknown reasons sometimes NavigationStack is not cleared
            {                                               // thoroughly, so just add an if condition to make sure is cleaned
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }

            dialog.Hide();
        }

        private async void OnGoToForgotPasswordPage()
        {
            await Navigation.PushAsync(new ForgotPswdPage());
        }
    }
}
