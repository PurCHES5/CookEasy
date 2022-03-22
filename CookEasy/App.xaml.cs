﻿using System;
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

            MainPage = new NavPage();
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
