using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<AdminRecommendation> AdminRecommendations { get; set; }
        public ICollection<MealFood> MealFoods { get; set; }

        public Recipe() { }

        public Recipe(string name, int calories, string description, int userId)
        {
            Name = name;
            Calories = calories;
            Description = description;
            UserId = userId;
        }
    }
}
