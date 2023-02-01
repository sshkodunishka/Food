using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class DeleteMeal : CommandBase
    {
        private readonly ChangeMealViewModel _viewModel;

        public DeleteMeal(ChangeMealViewModel viewModel)
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

                MealFoodRepository mealFoodRepository = new MealFoodRepository();
                User user = Account.getInstance(null).CurrentUser;
                var id = _viewModel.SelectedFood.Id;
                _viewModel.DisplayedFood = new ObservableCollection<DisplayedFood>(
                    _viewModel.DisplayedFood.Where(f => f.Id != _viewModel.SelectedFood.Id));
                mealFoodRepository.Remove(id);

            }

            catch
            {
                MessageBox.Show("Can't delete selected meal");
            }
        }
    }
}
