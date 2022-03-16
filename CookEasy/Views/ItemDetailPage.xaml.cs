using CookEasy.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CookEasy.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}