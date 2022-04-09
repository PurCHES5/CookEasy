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

namespace CookEasy.ViewModels
{
    public class SearchPageViewModel : BindableObject
    {
        public ObservableCollection<RecipeProp> Categories { get; set; }
        public SearchPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            BackButtonClick = new Command(OnBackButtonClicked);

            Categories = new ObservableRangeCollection<RecipeProp>();

            LoadCategories();
        }

        public INavigation Navigation { get; set; }
        public ICommand BackButtonClick { get; }

        public ICommand SearchRecipe => new Xamarin.Forms.Command<string>((string query) =>
        {
            Navigation.PushAsync(new SearchResultPage(query));
        });

        private async void OnBackButtonClicked()
        {
            await Navigation.PopAsync();
        }

        void LoadCategories()
        {
            Categories.Add(new RecipeProp { Name = "Western", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2F9550569905_4743020635_b.jpg?alt=media&token=8ac5256a-fefd-4ca7-863c-88e968c56836" });
            Categories.Add(new RecipeProp { Name = "Chinese", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2Flongbao.png?alt=media&token=ce34242a-e95a-4c48-b380-1d03e1f739e9" });
            Categories.Add(new RecipeProp { Name = "Indian", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2Fprata.png?alt=media&token=fb871457-9659-4f92-8591-f953b066cfde" });
            Categories.Add(new RecipeProp { Name = "Seafood", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2Fseafood.png?alt=media&token=4e441580-2a32-470e-bdb8-eebd86271cd1" });
            Categories.Add(new RecipeProp { Name = "Easy", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2F194935630_007045128c_b.jpg?alt=media&token=ffb1b8c9-1184-4d70-af12-1cf9008a7da0" });
            Categories.Add(new RecipeProp { Name = "Baked", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2F4877968590_a3e22b2e5a_b.jpg?alt=media&token=92ff439b-4e7f-4e96-8bc3-061c28a0a900" });
            Categories.Add(new RecipeProp { Name = "Healthy", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2FA535FDBB21.jpg?alt=media&token=5f1d45ef-0eda-416e-ad94-1c014909529b" });
            Categories.Add(new RecipeProp { Name = "Kid Friendly", Image = "https://firebasestorage.googleapis.com/v0/b/cookeasy-ad909.appspot.com/o/recipes%2FApH0xNOhHxgphNoefZhzEClFOcz1%2F1990743_c9b5bab8ab_b.jpg?alt=media&token=fdbb4346-f9e5-4483-bbc2-aed7ca50ba75" });
        }
    }
}
