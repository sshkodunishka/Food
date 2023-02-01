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
using Microsoft.EntityFrameworkCore;

namespace Personal_Nutritionist.ViewModels
{
    public class DisplayedFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }

        public DisplayedFood(int id, string name, int calories)
        {
            Id = id;
            Name = name;
            Calories = calories;
        }
    }

    public class ChangeMealViewModel : ViewModelBase
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

        public ObservableCollection<DisplayedFood> _displayedFood;
        public ObservableCollection<DisplayedFood> DisplayedFood
        {
            get => _displayedFood;
            set
            {
                _displayedFood = value;
                OnPropertyChanged(nameof(DisplayedFood));
            }
        }

        public DisplayedFood _selectedFood;
        public DisplayedFood SelectedFood
        {
            get => _selectedFood;
            set
            {
                _selectedFood = value;
                OnPropertyChanged(nameof(SelectedFood));
            }
        }



        public ICommand DeleteMeal { get; }
        public ICommand NavigateUserAddRecipeMeal { get; }
        public ICommand NavigateUserAddProductMeal { get; }

        public ChangeMealViewModel(PersonalNavigationStore personalNavigationStore, DateTime selectedDate, MealType mealType)
        {
            try
            {
                SelectedDate = selectedDate;
                SelectedType = mealType;

                MealHistoryRepository mealHistoryRepository = new MealHistoryRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                MealHistory history = mealHistoryRepository.GetMealHistory(currentUser, SelectedDate, SelectedType);

                DisplayedFood = new ObservableCollection<DisplayedFood>();
                if (history.MealFood != null)
                {
                    history.MealFood .ToList().ForEach(mealFood =>
                    {
                        DisplayedFood food;
                        if (mealFood.ProductId != null)
                        {
                            food = new DisplayedFood(mealFood.MealFoodId, mealFood.Product.Name, mealFood.Product.Calories);
                        }
                        else
                        {
                            food = new DisplayedFood(mealFood.MealFoodId, mealFood.Recipe.Name, mealFood.Recipe.Calories);
                        }
                        DisplayedFood.Add(food);
                    });
                }
               

                DeleteMeal = new DeleteMeal(this);
                NavigateUserAddRecipeMeal = new PersonalNavigateCommand<AddMealRecipe>(
                  new PersonalNavigationService<AddMealRecipe>(personalNavigationStore,
                  () =>
                  {
                      return new AddMealRecipe(personalNavigationStore, SelectedDate, SelectedType);
                  }));
                NavigateUserAddProductMeal = new PersonalNavigateCommand<AddMealProduct>(
                  new PersonalNavigationService<AddMealProduct>(personalNavigationStore,
                  () =>
                  {
                      return new AddMealProduct(personalNavigationStore, SelectedDate, SelectedType);
                  }));
            }
            catch
            {
                MessageBox.Show("Error change in change meal");
            }
        }
    }
}
