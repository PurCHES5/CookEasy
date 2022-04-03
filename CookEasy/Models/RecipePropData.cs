using System;
using System.Collections.Generic;
using System.Text;

namespace CookEasy.Models
{
    public class RecipePropData
    {
        public string RecipeTitle { get; set; }
        public int DifficultiesAvail { get; set; }
        public int Difficulty { get; set; }
        public string ImageUri { get; set; }
        public int Likes { get; set; }
        public string RecipeId { get; set; }
    }
}
