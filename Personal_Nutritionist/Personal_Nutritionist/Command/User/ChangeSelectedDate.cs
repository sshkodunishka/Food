using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class ChangeSelectedDate : CommandBase
    {

        private readonly UserProfileViewModel _user_viewModel;
        private readonly AdminUserInfoViewModel _admin_viewModel;
        private bool isNextDay;

        public ChangeSelectedDate(UserProfileViewModel viewModel, bool _isNextDay)
        {
            isNextDay = _isNextDay;
            _user_viewModel = viewModel;
        }

        public ChangeSelectedDate(AdminUserInfoViewModel viewModel, bool _isNextDay)
        {
            isNextDay = _isNextDay;
            _admin_viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (_user_viewModel != null)
                {
                    DateTime newSelectedDate;
                    if (this.isNextDay)
                    {
                        newSelectedDate = this._user_viewModel.SelectedDate.AddDays(1);
                    }
                    else
                    {
                        newSelectedDate = this._user_viewModel.SelectedDate.AddDays(-1);
                    }
                    _user_viewModel.SelectedDate = newSelectedDate;
                    _user_viewModel.BreakfastFood = "";
                    _user_viewModel.LunchFood = "";
                    _user_viewModel.DinnerFood = "";
                    _user_viewModel.TotalCalories = 0;
                    _user_viewModel.CaloriesLeft = 0;
                    Context context = new Context();
                    MealHistoryRepository mealHistoryRepository = new MealHistoryRepository();

                    User user = Account.getInstance(null).CurrentUser;


                    Repository<AdminCountingCalories> adminCaloriesRepository = new Repository<AdminCountingCalories>(context);

                    _user_viewModel.AdminCountedCalories = 0;
                    IEnumerable<AdminCountingCalories> adminCalories = adminCaloriesRepository.
                        Get(x => x.UserId == user.UserId
                            && x.Date.Year == _user_viewModel.SelectedDate.Year
                            && x.Date.Month == _user_viewModel.SelectedDate.Month
                            && x.Date.Day == _user_viewModel.SelectedDate.Day);

                    if (adminCalories.Count() > 0)
                    {
                        _user_viewModel.AdminCountedCalories = adminCalories.First().Calories;
                    }

                    List<MealHistory> mealHistories = mealHistoryRepository.GetMealHistory(user, _user_viewModel.SelectedDate);

                    mealHistories.ForEach(mealHistory =>
                    {
                        if (mealHistory.MealFood == null)
                        {
                            return;
                        }
                        mealHistory.MealFood.ToList().ForEach(mealFood =>
                        {
                            if (mealFood.ProductId != null)
                            {
                                _user_viewModel.TotalCalories += mealFood.Product.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    _user_viewModel.BreakfastFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    _user_viewModel.LunchFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    _user_viewModel.DinnerFood += mealFood.Product.Name + ", ";
                                }
                            }
                            else
                            {
                                _user_viewModel.TotalCalories += mealFood.Recipe.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    _user_viewModel.BreakfastFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    _user_viewModel.LunchFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    _user_viewModel.DinnerFood += mealFood.Recipe.Name + ", ";
                                }
                            }
                        });
                    });

                    float k1 = 655.1f;
                    float k2 = 9.563f;
                    float k3 = 1.85f;
                    float k4 = 4.676f;
                    float k5 = 66.5f;
                    float k6 = 13.78f;
                    float k7 = 5.003f;
                    float k8 = 6.775f;
                    if (user.Sex == 0)
                    {
                        _user_viewModel.CaloriesLeft = (int)(k1 + k2 * user.Weight + k3 * user.Height - k4 * user.Age - (float)_user_viewModel.TotalCalories);

                    }
                    else
                    {
                        _user_viewModel.CaloriesLeft = (int)(k5 + k6 * user.Weight + k7 * user.Height - k8 * user.Age - (float)_user_viewModel.TotalCalories);

                    }
                }
                if (_admin_viewModel != null)
                {
                    DateTime newSelectedDate;
                    if (this.isNextDay)
                    {
                        newSelectedDate = this._admin_viewModel.SelectedDate.AddDays(1);
                    }
                    else
                    {
                        newSelectedDate = this._admin_viewModel.SelectedDate.AddDays(-1);
                    }
                    _admin_viewModel.SelectedDate = newSelectedDate;
                    _admin_viewModel.BreakfastFood = "";
                    _admin_viewModel.LunchFood = "";
                    _admin_viewModel.DinnerFood = "";
                    _admin_viewModel.TotalCalories = 0;
                    _admin_viewModel.CaloriesLeft = 0;
                    Context context = new Context();
                    MealHistoryRepository mealHistoryRepository = new MealHistoryRepository();

                    Repository<AdminCountingCalories> adminCaloriesRepository = new Repository<AdminCountingCalories>(context);

                    _admin_viewModel.AdminCountedCalories = 0;
                    IEnumerable<AdminCountingCalories> adminCalories = adminCaloriesRepository.
                        Get(x => x.UserId == _admin_viewModel.User.UserId
                            && x.Date.Year == _admin_viewModel.SelectedDate.Year
                            && x.Date.Month == _admin_viewModel.SelectedDate.Month
                            && x.Date.Day == _admin_viewModel.SelectedDate.Day);

                    if (adminCalories.Count() > 0)
                    {
                        _admin_viewModel.AdminCountedCalories = adminCalories.First().Calories;
                    }

                    List<MealHistory> mealHistories = mealHistoryRepository.GetMealHistory(_admin_viewModel.User, _admin_viewModel.SelectedDate);

                    mealHistories.ForEach(mealHistory =>
                    {
                        if (mealHistory.MealFood == null)
                        {
                            return;
                        }
                        mealHistory.MealFood.ToList().ForEach(mealFood =>
                        {
                            if (mealFood.ProductId != null)
                            {
                                _admin_viewModel.TotalCalories += mealFood.Product.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    _admin_viewModel.BreakfastFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    _admin_viewModel.LunchFood += mealFood.Product.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    _admin_viewModel.DinnerFood += mealFood.Product.Name + ", ";
                                }
                            }
                            else
                            {
                                _admin_viewModel.TotalCalories += mealFood.Recipe.Calories;
                                if (mealHistory.MealType == MealType.Breakfast)
                                {
                                    _admin_viewModel.BreakfastFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Lunch)
                                {
                                    _admin_viewModel.LunchFood += mealFood.Recipe.Name + ", ";
                                }
                                if (mealHistory.MealType == MealType.Dinner)
                                {
                                    _admin_viewModel.DinnerFood += mealFood.Recipe.Name + ", ";
                                }
                            }
                        });
                    });


                    float k1 = 655.1f;
                    float k2 = 9.563f;
                    float k3 = 1.85f;
                    float k4 = 4.676f;
                    float k5 = 66.5f;
                    float k6 = 13.78f;
                    float k7 = 5.003f;
                    float k8 = 6.775f;
                    if (_admin_viewModel.User.Sex == 0)
                    {
                        _admin_viewModel.CaloriesLeft = (int)(k1 + k2 * _admin_viewModel.User.Weight + k3 * _admin_viewModel.User.Height - k4 * _admin_viewModel.User.Age - (float)_admin_viewModel.TotalCalories);

                    }
                    else
                    {
                        _admin_viewModel.CaloriesLeft = (int)( k5 + k6 * _admin_viewModel.User.Weight + k7 * _admin_viewModel.User.Height - k8 * _admin_viewModel.User.Age - (float)_admin_viewModel.TotalCalories);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Can't click new Date");
            }
        }
    }
}
