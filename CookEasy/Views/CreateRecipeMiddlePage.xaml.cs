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
    public partial class CreateRecipeMiddlePage : ContentPage
    {
        public CreateRecipeMiddlePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateRecipePage()) { BarBackgroundColor = Color.White });
        }
    }
}