using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class AdminRecommendation
    {
        public int AdminRecommendationId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public AdminRecommendation(int userId, int recipeId)
        {
            UserId = userId;
            RecipeId = recipeId;
        }
        public AdminRecommendation()
        {
        }
    }
}
