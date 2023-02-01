using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
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
    public class AdminUserInfoViewModel : ViewModelBase
    {
        public User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

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

        public int _totalCalories;
        public int TotalCalories
        {
            get => _totalCalories;
            set
            {
                _totalCalories = value;
                OnPropertyChanged(nameof(TotalCalories));
            }
        }

        public float _caloriesLeft;
        public float CaloriesLeft
        {
            get => _caloriesLeft;
            set
            {
                _caloriesLeft = value;
                OnPropertyChanged(nameof(CaloriesLeft));
            }
        }

        public int _adminCountedCalories;
        public int AdminCountedCalories
        {
            get => _adminCountedCalories;
            set
            {
                _adminCountedCalories = value;
                OnPropertyChanged(nameof(AdminCountedCalories));
            }
        }



        public string _breakfastFood;
        public string BreakfastFood
        {
            get => _breakfastFood;
            set
            {
                _breakfastFood = value;
                OnPropertyChanged(nameof(BreakfastFood));
            }
        }

        public string _lunchFood;
        public string LunchFood
        {
            get => _lunchFood;
            set
            {
                _lunchFood = value;
                OnPropertyChanged(nameof(LunchFood));
            }
        }

        public string _dinnerFood;
        public string DinnerFood
        {
            get => _dinnerFood;
            set
            {
                _dinnerFood = value;
                OnPropertyChanged(nameof(DinnerFood));
            }
        }

        public ICommand SetPrevDate { get; }
        public ICommand SetNextDate { get; }
        public ICommand OpenBreakfast { get; }
        public ICommand OpenLunch { get; }
        public ICommand OpenDinner { get; }
        public ICommand ChangeAdminCalories { get; }
        public ICommand NavigateAdminRecommendation { get; }

        public AdminUserInfoViewModel(PersonalNavigationStore personalNavigationStore, User selectedUser)
        {
            try
            {

                SelectedDate = DateTime.Now;

                SetPrevDate = new ChangeSelectedDate(this, false);
                SetNextDate = new ChangeSelectedDate(this, true);

                TotalCalories = 0;
                CaloriesLeft = 0;
                User = selectedUser;
                MealHistoryRepository mealHistoryRepository = new MealHistoryRepository();
                AdminCountingCaloriesRepository adminCaloriesRepository = new AdminCountingCaloriesRepository();

                AdminCountedCalories = 0;
                IEnumerable<AdminCountingCalories> adminCalories = adminCaloriesRepository.Get(selectedUser.UserId, SelectedDate);

                if (adminCalories.Count() > 0)
                {
                    AdminCountedCalories = adminCalories.First().Calories;
                }


                List<MealHistory> mealHistories = mealHistoryRepository.GetMealHistory(User, SelectedDate);

                mealHistories.ForEach(mealHistory =>
                {
                    if (mealHistory.MealFood != null)
                    {
                        mealHistory.MealFood.ToList().ForEach(mealFood =>
                        {
                            if (mealFood.ProductId != null)
                            {
                                TotalCalories += mealFood.Product.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    BreakfastFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    LunchFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    DinnerFood += mealFood.Product.Name + ", ";
                                }
                            }
                            else
                            {
                                TotalCalories += mealFood.Recipe.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    BreakfastFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    LunchFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    DinnerFood += mealFood.Recipe.Name + ", ";
                                }
                            }
                        });
                    }
                });
                float k1 = 655.1f;
                float k2 = 9.563f;
                float k3 = 1.85f;
                float k4 = 4.676f;
                float k5 = 66.5f;
                float k6 = 13.78f;
                float k7 = 5.003f;
                float k8 = 6.775f;

                if (User.Sex == 0)
                {
                    CaloriesLeft = (int)(k1 + k2 * User.Weight + k3 * User.Height - k4 * User.Age - (float)TotalCalories);

                }
                else
                {
                    CaloriesLeft = (int)(k5 + k6 * User.Weight + k7 * User.Height - k8 * User.Age - (float)TotalCalories);

                }

                OpenBreakfast = new PersonalNavigateCommand<AdminUserMealHistoty>(
                   new PersonalNavigationService<AdminUserMealHistoty>(personalNavigationStore,
                   () =>
                   {
                       return new AdminUserMealHistoty(personalNavigationStore, SelectedDate, MealType.Breakfast, User);
                   }));

                OpenLunch = new PersonalNavigateCommand<AdminUserMealHistoty>(
                  new PersonalNavigationService<AdminUserMealHistoty>(personalNavigationStore,
                  () =>
                  {
                      return new AdminUserMealHistoty(personalNavigationStore, SelectedDate, MealType.Lunch, User);
                  }));

                OpenDinner = new PersonalNavigateCommand<AdminUserMealHistoty>(
                  new PersonalNavigationService<AdminUserMealHistoty>(personalNavigationStore,
                  () =>
                  {
                      return new AdminUserMealHistoty(personalNavigationStore, SelectedDate, MealType.Dinner, User);
                  }));

                ChangeAdminCalories = new AdminChangeCalories(this);
                NavigateAdminRecommendation = new PersonalNavigateCommand<AdminRecommendationViewModel>(
                  new PersonalNavigationService<AdminRecommendationViewModel>(personalNavigationStore,
                  () =>
                  {
                      return new AdminRecommendationViewModel(personalNavigationStore, User);
                  }));
            }
            catch
            {
                MessageBox.Show("Can't load user ptofile");
            }
        }
    }
}
