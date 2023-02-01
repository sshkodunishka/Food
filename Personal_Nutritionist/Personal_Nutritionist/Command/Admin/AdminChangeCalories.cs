using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class AdminChangeCalories : CommandBase
    {
        private readonly AdminUserInfoViewModel _viewModel;

        public AdminChangeCalories(AdminUserInfoViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {

                AdminCountingCaloriesRepository adminCaloriesRepository = new AdminCountingCaloriesRepository();
                IEnumerable<AdminCountingCalories> adminCalories = adminCaloriesRepository.Get(_viewModel.User.UserId, _viewModel.SelectedDate);

                if (adminCalories.Count() > 0)
                {
                    AdminCountingCalories adminCaloriesItem = adminCalories.First();
                    adminCaloriesItem.Calories = _viewModel.AdminCountedCalories;
                    adminCaloriesRepository.Update(adminCaloriesItem);
                    return;
                }
                else
                {
                    AdminCountingCalories adminCaloriesItem = new AdminCountingCalories(_viewModel.User.UserId, _viewModel.SelectedDate, _viewModel.AdminCountedCalories);
                    adminCaloriesRepository.Create(adminCaloriesItem);
                }
            }

            catch
            {
                MessageBox.Show("Can't change calories");
            }
        }
    }
}
