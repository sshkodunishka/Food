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
    public class AdminRecipeViewModel : ViewModelBase
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

        public ICommand OpenRecipe { get; }
        public ICommand NavigateAdminAddRecipe { get; }
        public ICommand AdminDeleteRecipe { get; }

        public AdminRecipeViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                RecipeRepository repositoryRecipe = new RecipeRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                List<Recipe> recipes = repositoryRecipe.GetAll();
                Recipes = new ObservableCollection<Recipe>(recipes);

                OpenRecipe = new PersonalNavigateCommand<RecipeInfoViewModel>(
                    new PersonalNavigationService<RecipeInfoViewModel>(personalNavigationStore,
                    () =>
                    {
                        if (SelectedRecipe == null)
                            return null;
                        return new RecipeInfoViewModel(personalNavigationStore, SelectedRecipe);
                    }));

                NavigateAdminAddRecipe = new PersonalNavigateCommand<AddRecipeViewModel>(
                   new PersonalNavigationService<AddRecipeViewModel>(personalNavigationStore,
                   () =>
                   {
                       return new AddRecipeViewModel(personalNavigationStore);
                   }));

                AdminDeleteRecipe = new AdminDeleteRecipe(this);


            }
            catch
            {
                MessageBox.Show("Can't see all recipes");
            }
        }
    }
}
