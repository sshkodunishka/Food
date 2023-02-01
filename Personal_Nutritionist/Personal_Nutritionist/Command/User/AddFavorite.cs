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
    public class AddFavorite : CommandBase
    {
        private readonly UserRecipeViewModel _viewModel;

        public AddFavorite(UserRecipeViewModel viewModel)
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
                FavoritesRepository favoriteRepository = new FavoritesRepository();
                User user = Account.getInstance(null).CurrentUser;

                var fav = new Favorites(user.UserId, _viewModel.SelectedRecipe.Recipe.RecipeId);
                

                favoriteRepository.Create(fav);
                fav = favoriteRepository.GetByRecipeId(fav.RecipeId, fav.UserId);
                List<RecipeView> newRecipeViews = _viewModel.Recipes.Select((recieVIew) =>
                {
                    if (recieVIew.Recipe.RecipeId == _viewModel.SelectedRecipe.Recipe.RecipeId)
                    {
                        _viewModel.SelectedRecipe.isFavorite = true;
                        _viewModel.SelectedRecipe.isNotFavorite = false;
                        _viewModel.SelectedRecipe.FavoriteId = fav.FavoritesId;
                        return _viewModel.SelectedRecipe;
                    };
                    return recieVIew;
                }).ToList();
                _viewModel.Recipes = new ObservableCollection<RecipeView>(newRecipeViews);

            }

            catch
            {
                MessageBox.Show("Error while adding favorites meal");
            }
        }
    }
}
