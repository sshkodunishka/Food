using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class AdminDeleteRecipe : CommandBase
    {
        private readonly AdminRecipeViewModel _viewModel;

        public AdminDeleteRecipe(AdminRecipeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                RecipeRepository recipeRepository = new RecipeRepository();
                User user = Account.getInstance(null).CurrentUser;
                MealFoodRepository mealFoodRepository = new MealFoodRepository();
                FavoritesRepository favRepo = new FavoritesRepository();
                AdminRecommendationRepository adminRecRepo = new AdminRecommendationRepository();
                
                var id = _viewModel.SelectedRecipe.RecipeId;
                _viewModel.Recipes = new ObservableCollection<Recipe>(
                    _viewModel.Recipes.Where(f => f.RecipeId != _viewModel.SelectedRecipe.RecipeId));
                var food = RecipeRepository.Get(id);
                
                var mealFoods = mealFoodRepository.GetRecipeId(food.RecipeId);
                mealFoods.ForEach(meal =>
                {
                    mealFoodRepository.RemoveByRecipeId(food.RecipeId);
                });

                var favorites = favRepo.GetByRecipeId(food.RecipeId);
                favorites.ForEach(meal =>
                {
                    favRepo.Remove(meal.FavoritesId);
                });
                
                var adminReccomendations = adminRecRepo.GetByRecipe(food.RecipeId);
                adminReccomendations.ForEach(meal =>
                {
                    adminRecRepo.Remove(meal.AdminRecommendationId);
                });
                
                recipeRepository.Remove(food.RecipeId);

            }

            catch
            {
                MessageBox.Show("Can't delete selected recipe");
            }
        }
    }
}
