using System;
using System.Collections.Generic;
using System.Text;

namespace CookEasy.Models
{
    public class RecipeProp
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int DifficultiesAvail { get; set; }
        public int Likes { get; set; }
    }
}
