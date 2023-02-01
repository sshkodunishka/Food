using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.ViewModels
{
    public class RecipeInfoViewModel : ViewModelBase
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

        public RecipeInfoViewModel(PersonalNavigationStore personalNavigationStore, Recipe selectedRecipe)
        {
            try
            {
                Recipe = selectedRecipe;
            }
            catch
            {
                MessageBox.Show("Can't get info about recipe");
            }
        }
    }
}
