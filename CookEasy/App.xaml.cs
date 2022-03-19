using CookEasy.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RedCorners.Forms;

namespace CookEasy
{
    public partial class App : Application2
    {

        public override void InitializeSystems()
        {
            // Because we also have an App.xaml file
            InitializeComponent();

            base.InitializeSystems();
        }

        // Tell RedCorners.Forms what our first page should be
        public override Page GetFirstPage() =>
            new Views.HomePage();
    }
}
