using CookEasy.ViewModels;
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
    public partial class ForgotPswdPage : ContentPage
    {
        public ForgotPswdPage()
        {
            InitializeComponent();

            BindingContext = new StartPagesViewModel(Navigation);
        }
    }
}