using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Acr.UserDialogs;
using Firebase.Auth;
using Firebase;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using CookEasy.Services;
using CookEasy.Droid.Services;
using Android.Gms.Auth.Api;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Common;
using Firebase.Storage;
using Firebase.Database;
using System.IO;
using Android.Gms.Extensions;
using CookEasy.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Java.Util;
using GoogleGson;

namespace CookEasy.Droid
{
    [Activity(Label = "CookEasy", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity MainActivityInstance { get; private set; }
        private static FirebaseAuth mAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Forms.SetFlags("UseLegacyRenderers");
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: false);
            UserDialogs.Init(this);
            MainActivityInstance = this;
            FirebaseManager.Current = new AndroidFirebaseService();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            mAuth = GetFirebaseAuth();
        }

        private FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(this);
            FirebaseAuth firebaseAuth;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("cookeasy-ad909")
                    .SetApplicationId("cookeasy-ad909")
                    .SetApiKey("AIzaSyACG0_GwU1z8CHP8bUx9K-JmEOPjgJGwMA")
                    .SetDatabaseUrl("https://cookeasy-ad909-default-rtdb.asia-southeast1.firebasedatabase.app")
                    .SetStorageBucket("cookeasy-ad909.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(this, options);
                firebaseAuth = FirebaseAuth.Instance;
            }
            else
            {
                firebaseAuth = FirebaseAuth.Instance;
            }
            return firebaseAuth;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task<string> RegisterWithEmailPassword(string email, string password)
        {
            try
            {
                await mAuth.CreateUserWithEmailAndPasswordAsync(email, password);
                return "True";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                await mAuth.SignInWithEmailAndPasswordAsync(email, password);
                return "True";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> IsUserLoggedIn()
        {
            for (int i = 0; i < 10; i++)        // Attempt several times as Firebase Initialise time is unknown
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (mAuth.CurrentUser != null)
                {
                    return true;
                }
            }
            
            return false;       // Timed out or the user actually haven't login
        }

        public void SignOut()
        {
            mAuth.SignOut();
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                await mAuth.SendPasswordResetEmailAsync(email);
                return "True";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> UploadToStorage(Plugin.Media.Abstractions.MediaFile file, string uploadType, string uploadTime = null)
        {
            FirebaseStorage storage = FirebaseStorage.Instance;
            StorageReference storageRef = storage.Reference;
            StorageReference contentRef = null;

            if (uploadType == "avatar")
            {
                contentRef = storageRef.Child($"users/{mAuth.CurrentUser.Uid}/avatar/image.jpg");
            }
            else if (uploadType == "recipe")
            {
                contentRef = storageRef.Child($"recipes/{mAuth.CurrentUser.Uid}/{uploadTime}.jpg");
            }

            var stream = file.GetStream();
            try
            {
                await contentRef.PutStream(stream);
                var uri = await contentRef.GetDownloadUrlAsync();
                return uri.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetAvatarPhoto()
        {
            FirebaseStorage storage = FirebaseStorage.Instance;
            StorageReference storageRef = storage.Reference;
            StorageReference contentRef = storageRef.Child($"users/{mAuth.CurrentUser.Uid}/avatar/image.jpg");

            try
            {
                var uri = await contentRef.GetDownloadUrlAsync();
                return uri.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<object> ReadRecipe(string recipeId)
        {
            DatabaseReference database = FirebaseDatabase.Instance.Reference;

            var data = (DataSnapshot)await database.Child("recipes").Child(recipeId).Get();

            HashMap hashMap = data.Value.JavaCast<HashMap>();
            Gson gson = new GsonBuilder().Create();
            string json = gson.ToJson(hashMap);
            try
            {
                RecipeDetailData result = JsonConvert.DeserializeObject<RecipeDetailData>(json);

                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> WriteRecipe(object recipeData, string uploadTime)
        {
            RecipeDetailData data = (RecipeDetailData)recipeData;
            RecipePropData propData = new RecipePropData { 
                RecipeTitle = data.RecipeTitle, Likes = data.Likes, ImageUri = data.ImageUri, DifficultiesAvail = data.DifficultiesAvail,
            Difficulty = data.Difficulty, RecipeId = uploadTime};

            DatabaseReference database = FirebaseDatabase.Instance.Reference;

            string json = JsonConvert.SerializeObject(data);
            Gson gson = new GsonBuilder().Create();
            HashMap hashMap = gson.FromJson(json, Java.Lang.Class.FromType(typeof(HashMap))).JavaCast<HashMap>();

            string json2 = JsonConvert.SerializeObject(propData);
            Gson gson2 = new GsonBuilder().Create();
            HashMap hashMap2 = gson2.FromJson(json2, Java.Lang.Class.FromType(typeof(HashMap))).JavaCast<HashMap>();

            try
            {
                await database.Child("recipes").Child(uploadTime).SetValueAsync(hashMap);
                await database.Child("recipeProps").Child(uploadTime).SetValueAsync(hashMap2);
                return "True";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<object> ReadRecipeProp(int limit)
        {
            DatabaseReference database = FirebaseDatabase.Instance.Reference;

            var data = (DataSnapshot)await database.Child("recipeProps").OrderByChild("Likes").LimitToFirst(limit).Get();
            var list = data.Children;
            int count = (int)data.ChildrenCount;

            System.Collections.Generic.List<RecipePropData> recipePropDatas = new System.Collections.Generic.List<RecipePropData>();

            for (IIterator iterator = list.Iterator(); iterator.HasNext;)
            {
                DataSnapshot lastDataSnapshot = (DataSnapshot)iterator.Next();

                HashMap hashMap = lastDataSnapshot.Value.JavaCast<HashMap>();
                Gson gson = new GsonBuilder().Create();
                string json = gson.ToJson(hashMap);
                recipePropDatas.Add(JsonConvert.DeserializeObject<RecipePropData>(json));
            }

            try
            {
                return recipePropDatas;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<object> ReadRecipeProp(string queryText, int limit)
        {
            DatabaseReference database = FirebaseDatabase.Instance.Reference;

            var data = (DataSnapshot)await database.Child("recipeProps")
                .OrderByChild("RecipeTitle")
                .StartAt(queryText)
                .EndAt(queryText + "\uf8ff")
                .LimitToFirst(limit).Get();
            var list = data.Children;

            System.Collections.Generic.List<RecipePropData> recipePropDatas = new System.Collections.Generic.List<RecipePropData>();

            for (IIterator iterator = list.Iterator(); iterator.HasNext;)
            {
                DataSnapshot lastDataSnapshot = (DataSnapshot)iterator.Next();

                HashMap hashMap = lastDataSnapshot.Value.JavaCast<HashMap>();
                Gson gson = new GsonBuilder().Create();
                string json = gson.ToJson(hashMap);
                recipePropDatas.Add(JsonConvert.DeserializeObject<RecipePropData>(json));
            }

            try
            {
                return recipePropDatas;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetEmail()
        {
            return mAuth.CurrentUser.Email;
        }
    }
}