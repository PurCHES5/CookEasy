<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;

=======
﻿
>>>>>>> merger
namespace CookEasy.Models
{
    public class RecipeProp
    {
        public string Name { get; set; }
        public string Image { get; set; }
<<<<<<< HEAD
        public int Difficulty { get; set; }
        public int DifficultiesAvail { get; set; }
        public int Likes { get; set; }
=======
        /// <summary>
        /// 0 = Easy, 1 = Advanced
        /// </summary>
        public int Difficulty { get; set; }
        /// <summary>
        /// 0 = Easy, 1 = Advanced, 2 = Both
        /// </summary>
        public int DifficultiesAvail { get; set; } 
        public int Likes { get; set; }
        public string CardColor { get; set; }

        public bool EasyAvailable       // these codes are for easier Binding in xaml
        {
            get
            {
                return DifficultiesAvail == 0 || DifficultiesAvail == 2;
            } 
        }
        public bool AdvancedAvailable
        {
            get 
            {
                return DifficultiesAvail >= 1;
            }
        }
        public int DifficultyEasyBorder
        {
            get
            {
                return Difficulty == 0 ? 5 : 0;
            }
        }
        public int DifficultyAdvancedBorder
        {
            get
            {
                return Difficulty == 1 ? 5 : 0;
            }
        }
>>>>>>> merger
    }
}
