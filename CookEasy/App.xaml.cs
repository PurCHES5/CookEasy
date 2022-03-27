using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookEasy.Views;

namespace CookEasy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

<<<<<<< Updated upstream
            MainPage = new RegisterPage();
=======
            MainPage = new NavPage();
>>>>>>> Stashed changes
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
