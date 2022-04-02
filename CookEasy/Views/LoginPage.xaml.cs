using CookEasy.Services;
using CookEasy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookEasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new StartPagesViewModel(Navigation);

            IsLoggedIn();
        }


        private async void IsLoggedIn()
        {
            var dialog = UserDialogs.Instance.Loading("", maskType: MaskType.Black);

            if (await FirebaseManager.Current.IsLoggedIn())
            {
                await Navigation.PushAsync(new NavPage());

                for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
                if (Navigation.NavigationStack.Count > 1)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[0]);
                }
            }

            dialog.Hide();
        }

        protected override bool OnBackButtonPressed()
        {
            for (int i = 0; i < Navigation.NavigationStack.Count - 1; i++)      // clear NavigationStack so that in the actual logged in pages
            {                                                               // can just call Navigation.PopToRootAsync to go back to "home page"
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            if (Navigation.NavigationStack.Count > 1)       // for some unknown reasons sometimes NavigationStack is not cleared
            {                                               // thoroughly, so just add an if condition to make sure is cleaned
                Navigation.RemovePage(Navigation.NavigationStack[0]);
            }
            return false;
        }
    }
}