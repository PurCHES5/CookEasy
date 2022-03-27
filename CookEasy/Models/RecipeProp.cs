using System;
using System.Collections.Generic;
using System.Text;

namespace CookEasy.Models
{
    public class RecipeProp
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        /// <summary>
        /// 0 = Easy, 1 = Advanced
        /// </summary>
        public int Difficulty { get; set; }
        /// <summary>
        /// 0 = Easy, 1 = Advanced, 2 = Both
        /// </summary>
        public int DifficultiesAvail { get; set; } 
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
        public double DifficultyEasySize
        {
            get
            {
                return Difficulty == 0 ? 30 : 20;
            }
        }
        public double DifficultyAdvancedSize
        {
            get
            {
                return Difficulty == 1 ? 30 : 20;
            }
        }
        public double DifficultyEasyOpacity
        {
            get
            {
                return Difficulty == 0 ? 1 : 0.6;
            }
        }
        public double DifficultyAdvancedOpacity
        {
            get
            {
                return Difficulty == 1 ? 1 : 0.6;
            }
        }
    }
}
