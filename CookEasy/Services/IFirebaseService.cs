using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CookEasy.Services
{
    public interface IFirebaseService
    {
        void SignOut();

        Task<bool> IsLoggedIn();

        Task<string> ForgotPassword(string email);

        Task<string> RegisterAccount(string email, string password);

        Task<string> SignInEmail(string email, string password);

        Task<string> UploadToStorage(Plugin.Media.Abstractions.MediaFile file, string uploadType, string uploadTime = null);

        Task<string> GetAvatarPhoto();

        Task<object> ReadRecipe(string recipeId);

        Task<string> WriteRecipe(object recipeData, string uploadTime);

        Task<object> ReadRecipeProps(int limit);

        Task<object> ReadRecipeProps(string queryText, int limit);

        string GetEmail();
    }
}
