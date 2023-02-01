using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class MealFood
    {
        public int MealFoodId { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public int? RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int MealHistoryId { get; set; }
        
        public MealHistory MealHistory { get; set;  }

        public MealFood() { }

        public MealFood(int foodId, int mealHistoryId, bool isRecipe)
        {
            if (isRecipe)
            {
                RecipeId = foodId;
            }
            else
            {
                ProductId = foodId;
            }
            MealHistoryId = mealHistoryId;
        }
    }
}
