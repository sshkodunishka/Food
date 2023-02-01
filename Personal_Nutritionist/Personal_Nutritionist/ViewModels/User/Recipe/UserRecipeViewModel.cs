using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{

    public class RecipeView
    {
        public Recipe Recipe { get; set; }
        public bool isFavorite { get; set; }
        public bool isNotFavorite { get; set; }
        public int? FavoriteId { get; set; }

        public RecipeView(Recipe recipe, bool isFavorite, bool isNotFavorite, int? favoriteId)
        {
            Recipe = recipe;
            this.isFavorite = isFavorite;
            this.isNotFavorite = isNotFavorite;
            FavoriteId = favoriteId;
        }
    }

    public class UserRecipeViewModel : ViewModelBase
    {
        private ObservableCollection<RecipeView> _recipes;
        public ObservableCollection<RecipeView> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public RecipeView _selectedRecipe;
        public RecipeView SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public ICommand OpenRecipe { get; }
        public ICommand NavigateUserAddRecipe { get; }
        public ICommand AddFavoriteCommand { get; }
        public ICommand DeleteFavoriteCommand { get; }

        public UserRecipeViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                RecipeRepository repositoryRecipe = new RecipeRepository();
                FavoritesRepository repositoryFavorite = new FavoritesRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                List<Recipe> recipes = repositoryRecipe.GetByUserId(currentUser.UserId);

                List<Favorites> favorites = repositoryFavorite.GetByUserId(currentUser.UserId);
                List<RecipeView> recipeViews = new List<RecipeView>();
                recipes.ForEach(recipe =>
                {
                    Favorites? favorite = favorites.Find((favorite) => favorite.RecipeId == recipe.RecipeId);
                    RecipeView recipeView = new RecipeView(recipe, favorite != null, favorite == null, favorite?.FavoritesId);
                    recipeViews.Add(recipeView);
                });

                Recipes = new ObservableCollection<RecipeView>(recipeViews);

                OpenRecipe = new PersonalNavigateCommand<UserRecipeInfoViewModel>(
                    new PersonalNavigationService<UserRecipeInfoViewModel>(personalNavigationStore,
                    () =>
                    {
                        if (SelectedRecipe == null)
                            return null;
                        return new UserRecipeInfoViewModel(personalNavigationStore, SelectedRecipe.Recipe);
                    }));

                NavigateUserAddRecipe = new PersonalNavigateCommand<UserAddRecipeViewModel>(
                   new PersonalNavigationService<UserAddRecipeViewModel>(personalNavigationStore,
                   () =>
                   {
                       return new UserAddRecipeViewModel(personalNavigationStore);
                   }));

                AddFavoriteCommand = new AddFavorite(this);
                DeleteFavoriteCommand = new DeleteFavoriteCommand(this);

            }
            catch
            {
                MessageBox.Show("Can't see all recipes");
            }
        }
    }
}
