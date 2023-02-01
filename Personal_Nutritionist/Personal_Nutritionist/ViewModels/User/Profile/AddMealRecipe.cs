using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    public class AddMealRecipe : ViewModelBase
    {
        public DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public MealType _selectedType;
        public MealType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public ObservableCollection<Recipe> _recipes;
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

        public ICommand AddMealRecipeCommand { get; }
        public ICommand BackToChangeMeal { get; }

        public AddMealRecipe(PersonalNavigationStore personalNavigationStore, DateTime selectedDate, MealType mealType)
        {
            try
            {
                SelectedDate = selectedDate;
                SelectedType = mealType;
                Context context = new Context();
                Repository<Recipe> repositoryRecipe = new Repository<Recipe>(context);
                User user = Account.getInstance(null).CurrentUser;
                List<Recipe> recipes = repositoryRecipe
                    .GetWithInclude(x => x.UserId == user.UserId || x.User.Role.RoleName == RoleType.Admin, y => y.User, y => y.User.Role).ToList();
                Recipes = new ObservableCollection<Recipe>(recipes);

                AddMealRecipeCommand = new Command.AddMealRecipe(this, personalNavigationStore);
                BackToChangeMeal = new PersonalNavigateCommand<ChangeMealViewModel>(
                  new PersonalNavigationService<ChangeMealViewModel>(personalNavigationStore,
                  () =>
                  {
                      return new ChangeMealViewModel(personalNavigationStore, selectedDate, mealType);
                  }));
            }
            catch
            {
                MessageBox.Show("Error in add meal recipe");
            }
        }
    }
}
