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
    public class AdminUserMealHistoty : ViewModelBase
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

        public ICommand BackToUser { get; }

        public AdminUserMealHistoty(PersonalNavigationStore personalNavigationStore, DateTime selectedDate, MealType mealType, User user)
        {
            try
            {
                SelectedDate = selectedDate;
                SelectedType = mealType;

                MealHistoryRepository mealHistoryRepository = new MealHistoryRepository();


                MealHistory history = mealHistoryRepository.GetMealHistory(user, SelectedDate, SelectedType);

                DisplayedFood = new ObservableCollection<DisplayedFood>();
                if (history.MealFood != null)
                {
                    history.MealFood.ToList().ForEach(mealFood =>
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


                BackToUser = new PersonalNavigateCommand<AdminUserInfoViewModel>(
                  new PersonalNavigationService<AdminUserInfoViewModel>(personalNavigationStore,
                  () =>
                  {
                      return new AdminUserInfoViewModel(personalNavigationStore, user);
                  }));
            }
            catch
            {
                MessageBox.Show("Error change in change meal");
            }
        }
    }
}
