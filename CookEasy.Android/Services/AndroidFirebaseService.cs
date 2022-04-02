﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CookEasy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CookEasy.Droid.Services
{
    class AndroidFirebaseService : IFirebaseService
    {
        public void SignOut()
        {
            MainActivity mainActivity = new MainActivity();
            mainActivity.SignOut();
        }

        public async Task<string> ForgotPassword(string email)
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.ForgotPassword(email);
        }

        public async Task<bool> IsLoggedIn()
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.IsUserLoggedIn();
        }

        public async Task<string> RegisterAccount(string email, string password)
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.RegisterWithEmailPassword(email, password);
        }

        public async Task<string> SignInEmail(string email, string password)
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.LoginWithEmailPassword(email, password);
        }

        public async Task<string> UploadToStorage(Plugin.Media.Abstractions.MediaFile file, string uploadType)
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.UploadToStorage(file, uploadType);
        }

        public async Task<string> GetAvatarPhoto()
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.GetAvatarPhoto();
        }

        public async Task<object> ReadRecipe(string recipeId)
        {
            MainActivity mainActivity = new MainActivity();
            return await mainActivity.ReadRecipe(recipeId);
        }
    }
}