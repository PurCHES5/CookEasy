using System;
using System.Collections.Generic;
using System.Text;

namespace CookEasy.Models
{
    public class RecipeDetailData
    {
        public string RecipeTitle { get; set; }
        public string AuthorUid { get; set; }
        public int DifficultiesAvail { get; set; }
        public int Difficulty { get; set; }
        public string ImageUri { get; set; }
        public int Likes { get; set; }
        public string CookTime { get; set; }
        public string Portion { get; set; }
        public string MainIngre { get; set; }
        public string OtherIngre { get; set; }
        public string Steps { get; set; }
        /*
        public RecipeDetailData()
        {

        }

        public RecipeDetailData(string recipeTitle, string authorUid, int availableLevels, string imageUri, int likes)
        {
            RecipeTitle = recipeTitle;
            AuthorUid = authorUid;
            AvailableLevels = availableLevels;
            ImageUri = imageUri;
            Likes = likes;
        }
        */
    }
}
