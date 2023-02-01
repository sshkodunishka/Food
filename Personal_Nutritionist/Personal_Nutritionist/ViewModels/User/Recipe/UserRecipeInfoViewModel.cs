using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    public class UserRecipeInfoViewModel : ViewModelBase
    {
        public Recipe _recipe;
        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                OnPropertyChanged(nameof(Recipe));
            }
        }

        public ICommand BackToRecipe { get; }

        public UserRecipeInfoViewModel(PersonalNavigationStore personalNavigationStore, Recipe selectedRecipe)
        {
            try
            {
                Recipe = selectedRecipe;
                BackToRecipe = new PersonalNavigateCommand<UserRecipeViewModel>(
                new PersonalNavigationService<UserRecipeViewModel>(personalNavigationStore,
                () =>
                {
                    return new UserRecipeViewModel(personalNavigationStore);
                }));
            }
            catch
            {
                MessageBox.Show("Can't get info about recipe");
            }
        }
    }
}
