using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookEasy.ViewModels;

namespace CookEasy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        public SearchResultPage()
        {
            InitializeComponent();
            BindingContext = new SearchResultPageViewModel(Navigation);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;

            var TransY = e.ScrollY;

            if (TransY < 20)
            {
                titleBarShadow.BlurRadius = (float)TransY / 4;
            }
            else
            {
                titleBarShadow.BlurRadius = 5;
            }

        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new NavPage());
            return true;
        }
    }
}