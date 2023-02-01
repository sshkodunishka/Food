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

    public class AdminAddNewRecommendation : ViewModelBase
    {
        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ICommand AddRecipeRecomendation { get; }
        public ICommand BackToRecommendation { get; }

        public AdminAddNewRecommendation(PersonalNavigationStore personalNavigationStore, User selectedUser)
        {
            try
            {
                RecipeRepository repositoryRecipe = new RecipeRepository();
                User adminUser = Account.getInstance(null).CurrentUser;


                List<Recipe> recipes = repositoryRecipe.GetRecipeForRecomendations(selectedUser.UserId);
                Recipes = new ObservableCollection<Recipe>(recipes);
                SelectedUser = selectedUser;
                AddRecipeRecomendation = new AddNewRecommendation(this, personalNavigationStore);

                BackToRecommendation = new PersonalNavigateCommand<AdminRecommendationViewModel>(
                 new PersonalNavigationService<AdminRecommendationViewModel>(personalNavigationStore,
                 () =>
                 {
                     return new AdminRecommendationViewModel(personalNavigationStore, selectedUser);
                 }));

            }
            catch
            {
                MessageBox.Show("Can't see all recipes");
            }
        }
    }
}
